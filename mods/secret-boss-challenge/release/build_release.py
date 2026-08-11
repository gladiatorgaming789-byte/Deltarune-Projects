#!/usr/bin/env python3
"""Build Secret Boss Challenge v0.4.3 as native Deltamod G3M patches."""
from pathlib import Path
import argparse, json, shutil, subprocess, tempfile, zipfile

ROOT = Path(__file__).resolve().parent
PROJECT = ROOT.parent
CHAPTERS = (1, 2, 5)
EXPECTED = {
    1: {"gml_GlobalScript_scr_monstersetup", "gml_Object_obj_jokerbattleevent_Step_0", "gml_Object_obj_treasure_room_Create_0", "gml_Object_obj_treasure_room_Other_10", "gml_Object_obj_darkcontroller_Create_0", "gml_Object_obj_darkcontroller_Step_0", "gml_Object_obj_darkcontroller_Draw_0", "gml_Object_obj_joker_Create_0", "gml_Object_obj_joker_Other_15"},
    2: {"gml_GlobalScript_scr_monstersetup", "gml_Object_obj_treasure_room_Create_0", "gml_Object_obj_treasure_room_Other_10", "gml_Object_obj_darkcontroller_Create_0", "gml_Object_obj_darkcontroller_Step_0", "gml_Object_obj_darkcontroller_Draw_0", "gml_Object_obj_spamton_neo_enemy_Create_0", "gml_Object_obj_ch2_sceneex2a_Step_0"},
    5: {"gml_GlobalScript_scr_damage", "gml_GlobalScript_scr_mnendturn", "gml_GlobalScript_scr_spellinfo", "gml_GlobalScript_scr_spelltext", "gml_GlobalScript_scr_weaponinfo", "gml_GlobalScript_scr_spellmenu_setup", "gml_Object_obj_dw_pink_encounter_Step_0", "gml_Object_obj_darkcontroller_Create_0", "gml_Object_obj_darkcontroller_Step_0", "gml_Object_obj_darkcontroller_Draw_0", "gml_Object_obj_battlecontroller_Create_0", "gml_Object_obj_grazebox_Create_0", "gml_Object_obj_heroparent_Step_0", "gml_Object_obj_dw_fcastle_pinkshop_Create_0", "gml_Object_obj_dw_fcastle_pinkshop_Step_0", "gml_Object_obj_dw_fcastle_pinkshop_Other_11"},
}

def run(cmd):
    print('+', *map(str, cmd))
    subprocess.run([str(x) for x in cmd], check=True, stdin=subprocess.DEVNULL)

def find_clean(root, ch):
    for p in (root/f"chapter{ch}_windows"/"data.win", root/f"ch{ch}"/"data.win", root/f"chapter{ch}_windows"/f"chapter{ch}_windows"/"data.win"):
        if p.is_file(): return p
    raise FileNotFoundError(f"Chapter {ch} data.win not found under {root}")

def names(group, key):
    return {x['name'] for x in group.get(key, [])}

def clean_patch(src, dst, ch):
    with zipfile.ZipFile(src) as zin:
        m=json.loads(zin.read('g3mpatch.json'))
        r=m.get('resources', {})
        unexpected=set(r)-{'CodeEntries','Scripts','Sounds'}
        if unexpected: raise RuntimeError(f"Chapter {ch}: unexpected resource types {sorted(unexpected)}")
        code=r.get('CodeEntries', {})
        if names(code,'changed') != EXPECTED[ch]: raise RuntimeError(f"Chapter {ch}: unexpected changed CodeEntries")
        if names(code,'deleted'): raise RuntimeError(f"Chapter {ch}: deleted CodeEntries are not expected")
        scripts=r.get('Scripts', {})
        if ch == 5:
            expected_new_code={'gml_Script_scr_gg_sbc_rewards','gml_Script_scr_gg_sbc_shield_apply'}
            expected_new_scripts={'scr_gg_sbc_rewards','scr_gg_sbc_shield_apply'}
            if names(code,'new') != expected_new_code or names(scripts,'new') != expected_new_scripts:
                raise RuntimeError('Chapter 5: unexpected helper resources')
        elif names(code,'new') or names(scripts,'new'):
            raise RuntimeError(f"Chapter {ch}: unexpected new resources")
        r.pop('Sounds', None)  # UTMT serialization noise; SBC never edits audio.
        m['resources']=r
        changed=sum(len(v.get('changed',[])) for v in r.values())
        new=sum(len(v.get('new',[])) for v in r.values())
        deleted=sum(len(v.get('deleted',[])) for v in r.values())
        cf=sum(sum(len(x.get('files',{})) for x in v.get('changed',[])) for v in r.values())
        nf=sum(sum(len(x.get('files',{})) for x in v.get('new',[])) for v in r.values())
        m['statistics']={'totalChanged':changed,'totalNew':new,'totalDeleted':deleted,'totalChangedFiles':cf,'totalNewFiles':nf}
        m['applyPlan']={'mode':'standard','requiresCodePipeline':True,'requiresTexturePipeline':False,'requiresAssetReorder':True,'requiresHeavyFinalize':True,'supportsDirectResourceApply':False,'simpleResourceTypes':[],'heavyResourceTypes':['CodeEntries','Scripts']}
        with zipfile.ZipFile(dst,'w',zipfile.ZIP_DEFLATED,compresslevel=9) as zout:
            for info in zin.infolist():
                if info.filename=='g3mpatch.json' or info.filename.startswith('Sounds/'): continue
                zout.writestr(info, zin.read(info.filename))
            zout.writestr('g3mpatch.json', json.dumps(m, indent=2).encode())

def main():
    p=argparse.ArgumentParser()
    p.add_argument('--game-root', type=Path, required=True)
    p.add_argument('--utmt', type=Path, required=True)
    p.add_argument('--g3mtool', type=Path, required=True)
    p.add_argument('--output', type=Path, default=ROOT/'Secret_Boss_Challenge_v0.4.3_Deltamod.zip')
    a=p.parse_args()
    with tempfile.TemporaryDirectory(prefix='sbc_v043_') as td:
        t=Path(td); stage=t/'package'; patches=stage/'patches'; patches.mkdir(parents=True)
        for name in ('meta.json','modding.xml','README.txt'):
            shutil.copy2(ROOT/name, stage/name)
        license_path=(PROJECT/'LICENSE') if (PROJECT/'LICENSE').is_file() else (ROOT/'LICENSE')
        if license_path.is_file(): shutil.copy2(license_path, stage/'LICENSE')
        for ch in CHAPTERS:
            clean=find_clean(a.game_root,ch)
            modified=t/f'ch{ch}_modified.win'
            raw=t/f'ch{ch}_raw.g3mpatch'
            final=patches/f'SecretBossChallenge_ch{ch}.g3mpatch'
            run([a.utmt,'load',clean,'--output',modified,'--scripts',ROOT/f'SecretBossChallenge_ch{ch}.csx'])
            run([a.g3mtool,'patch','create',clean,modified,raw])
            clean_patch(raw,final,ch)
            run([a.g3mtool,'patch','validate',final,'--data',clean])
        a.output.parent.mkdir(parents=True,exist_ok=True)
        with zipfile.ZipFile(a.output,'w',zipfile.ZIP_DEFLATED,compresslevel=9) as z:
            for f in sorted(stage.rglob('*')):
                if f.is_file(): z.write(f,f.relative_to(stage).as_posix())
        with zipfile.ZipFile(a.output) as z:
            bad=z.testzip()
            if bad: raise RuntimeError(f'ZIP integrity failure: {bad}')
        print('Wrote',a.output)
if __name__=='__main__': main()
