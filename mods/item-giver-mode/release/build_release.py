#!/usr/bin/env python3
"""Build Item Giver Mode v0.3.1 as Deltamod-native G3M patches.

v0.3.1 fixes the broken v0.3.0 pseudo Controls row. It patches DELTARUNE's
real Controls Step/Draw handlers and moves the Item Giver runtime into a
uniquely named script. obj_time Draw GUI End receives only one helper call.
"""
from __future__ import annotations

from pathlib import Path
import argparse
import json
import shutil
import subprocess
import tempfile
import zipfile

ROOT = Path(__file__).resolve().parent
CHAPTERS = range(1, 6)
EXPECTED_CODE_CHANGED = {
    "gml_Object_obj_darkcontroller_Step_0",
    "gml_Object_obj_darkcontroller_Draw_0",
    "gml_Object_obj_time_Draw_75",
}
EXPECTED_CODE_NEW = {"gml_Script_scr_gg_itemgiver_runtime"}
EXPECTED_SCRIPT_NEW = {"scr_gg_itemgiver_runtime"}

CONTROLS_STEP_APPEND = r'''
// Item Giver v0.3.1: genuine Controls row 9 / keyboard rebinding.
if (global.menuno == 3 && global.submenu == 35 && !global.is_console)
{
    if (!variable_global_exists("ig_itemgiver_key"))
        global.ig_itemgiver_key = ord("I");
    if (!variable_instance_exists(id, "ig_bind_debounce"))
        ig_bind_debounce = 0;
    if (!variable_instance_exists(id, "ig_bind_message"))
        ig_bind_message = "";
    if (!variable_instance_exists(id, "ig_bind_message_timer"))
        ig_bind_message_timer = 0;

    if (ig_bind_message_timer > 0)
        ig_bind_message_timer -= 1;

    if (control_select_con == 0 && global.submenucoord[35] == 7 && button1_p())
    {
        global.ig_itemgiver_key = ord("I");
        ig_bind_message = "RESET TO I";
        ig_bind_message_timer = 60;
        scr_gg_itemgiver_runtime(1);
    }

    if (control_select_con == 0 && global.submenucoord[35] == 9 && button1_p())
    {
        control_select_con = 3;
        keyboard_lastkey = -1;
        ig_bind_debounce = 2;
        ig_bind_message = "PRESS A KEY";
        ig_bind_message_timer = 0;
        selectnoise = 1;
    }
    else if (control_select_con == 3)
    {
        if (ig_bind_debounce > 0)
            ig_bind_debounce -= 1;
        else if (keyboard_check_pressed(vk_escape))
        {
            control_select_con = 0;
            ig_bind_message = "CANCELLED";
            ig_bind_message_timer = 45;
            onebuffer = 2;
            twobuffer = 2;
        }
        else if (keyboard_check_pressed(vk_anykey))
        {
            ig_bind_candidate = keyboard_lastkey;
            ig_bind_reserved = false;

            if (ig_bind_candidate < 0 || ig_bind_candidate >= array_length_1d(global.asc_def))
                ig_bind_reserved = true;
            if (ig_bind_candidate == vk_enter || ig_bind_candidate == vk_shift || ig_bind_candidate == vk_control || ig_bind_candidate == vk_escape)
                ig_bind_reserved = true;

            for (ig_bind_index = 0; ig_bind_index < 7; ig_bind_index += 1)
            {
                if (global.input_k[ig_bind_index] == ig_bind_candidate)
                    ig_bind_reserved = true;
            }

            if (ig_bind_reserved)
            {
                ig_bind_message = "KEY IN USE";
                ig_bind_message_timer = 60;
            }
            else
            {
                global.ig_itemgiver_key = ig_bind_candidate;
                control_select_con = 0;
                ig_bind_message = "SAVED";
                ig_bind_message_timer = 60;
                onebuffer = 2;
                twobuffer = 2;
                selectnoise = 1;
                scr_gg_itemgiver_runtime(1);
            }
        }
    }
}
'''.strip()

CONTROLS_DRAW_APPEND = r'''
// Item Giver v0.3.1: draw genuine Controls row 9 after DELTARUNE's native rows.
if (global.menuno == 3 && global.submenu == 35 && !global.is_console)
{
    if (!variable_global_exists("ig_itemgiver_key"))
        global.ig_itemgiver_key = ord("I");

    draw_set_color(c_white);
    if (global.submenucoord[35] == 9)
        draw_set_color(c_aqua);
    if (global.submenucoord[35] == 9 && control_select_con == 3)
        draw_set_color(c_red);

    if (global.gamepad_type == "Sony DualShock 4" || global.gamepad_type == "DualSense Wireless Controller")
        draw_text(xx + 105, ((global.lang == "en") ? (yy + 137) : (yy + 136)) + (9 * (29 + ((global.lang == "ja") ? 1 : 0))) + ((global.lang == "en") ? 0 : -4), "ITEM GIVER");
    else
        draw_text(xx + 105, yy + 140 + (9 * (28 + ((global.lang == "ja") ? 1 : 0))) + ((global.lang == "en") ? 0 : -4), "ITEM GIVER");

    draw_text(xx + 325, yy + langopt(0, -8) + 140 + (9 * langopt(28, 30)), (control_select_con == 3) ? "PRESS A KEY" : string(global.asc_def[global.ig_itemgiver_key]));

    if (variable_instance_exists(id, "ig_bind_message_timer") && ig_bind_message_timer > 0)
        draw_text(xx + 430, yy + langopt(0, -8) + 140 + (9 * langopt(28, 30)), ig_bind_message);
}
'''.strip()


def run(cmd: list[Path | str]) -> None:
    print("+", *map(str, cmd))
    subprocess.run([str(x) for x in cmd], check=True, stdin=subprocess.DEVNULL)


def csharp_verbatim(text: str) -> str:
    return text.replace('"', '""')


def find_clean(game_root: Path, chapter: int) -> Path:
    candidates = [
        game_root / f"chapter{chapter}_windows" / "data.win",
        game_root / f"chapter{chapter}_windows" / f"chapter{chapter}_windows" / "data.win",
        game_root / f"chapter{chapter}_windows" / f"chapter{chapter}_windows" / f"chapter{chapter}_windows" / "data.win",
    ]
    for path in candidates:
        if path.is_file():
            return path
    raise FileNotFoundError(f"Could not find clean Chapter {chapter} data.win under {game_root}")


def make_csx(chapter: int, runtime: str, step_append: str, draw_append: str) -> str:
    runtime_cs = csharp_verbatim(runtime)
    step_cs = csharp_verbatim(step_append)
    draw_cs = csharp_verbatim(draw_append)
    return f'''using System;
using System.Linq;
using UndertaleModLib.Models;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Compiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{{
    ScriptError("Item Giver requires the DELTARUNE full release.");
    return;
}}

string displayName = Data?.GeneralInfo?.DisplayName?.Content?.ToLowerInvariant() ?? "";
if (!displayName.Contains("chapter {chapter}") && !displayName.Contains("chapitre {chapter}"))
{{
    ScriptError("This Item Giver source patch is for Chapter {chapter} only.");
    return;
}}

GlobalDecompileContext globalDecompileContext = new(Data);
IDecompileSettings decompilerSettings = new DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{{
    ThrowOnNoOpFindReplace = true
}};

var runtimeName = "gml_Script_scr_gg_itemgiver_runtime";
var runtimeCode = Data.Code.ByName(runtimeName);
if (runtimeCode == null)
{{
    UndertaleModLib.Compiler.CodeImportGroup runtimeGroup = new(Data, globalDecompileContext, decompilerSettings);
    runtimeGroup.QueueReplace(runtimeName, @"{runtime_cs}");
    runtimeGroup.Import();
    runtimeCode = Data.Code.ByName(runtimeName);
    if (runtimeCode == null)
    {{
        ScriptError("Item Giver failed to create its runtime code entry.");
        return;
    }}
}}
if (Data.Scripts.ByName("scr_gg_itemgiver_runtime") == null)
    Data.Scripts.Add(new UndertaleScript() {{ Name = Data.Strings.MakeString("scr_gg_itemgiver_runtime"), Code = runtimeCode }});
Data.Functions.EnsureDefined("scr_gg_itemgiver_runtime", Data.Strings);

var draw75 = Data.Code.ByName("gml_Object_obj_time_Draw_75");
var step0 = Data.Code.ByName("gml_Object_obj_darkcontroller_Step_0");
var draw0 = Data.Code.ByName("gml_Object_obj_darkcontroller_Draw_0");
if (draw75 == null || step0 == null || draw0 == null)
{{
    ScriptError("Item Giver could not find DELTARUNE's required controller code.");
    return;
}}

var timeText = GetDecompiledText(draw75);
if (!timeText.Contains("scr_gg_itemgiver_runtime"))
    importGroup.QueueAppend(draw75, "scr_gg_itemgiver_runtime();");

var stepText = GetDecompiledText(step0);
string navOld = "if (global.submenucoord[35] < 8)";
string navNew = "if (global.submenucoord[35] < (global.is_console ? 8 : 9))";
if (!stepText.Contains(navNew))
{{
    int navCount = stepText.Split(new[] {{ navOld }}, StringSplitOptions.None).Length - 1;
    if (navCount != 1)
    {{
        ScriptError("Item Giver expected exactly one Controls navigation anchor, found " + navCount + ".");
        return;
    }}
    stepText = stepText.Replace(navOld, navNew);
}}
if (!stepText.Contains("Item Giver v0.3.1: genuine Controls row 9"))
    stepText += @"\n{step_cs}\n";
importGroup.QueueReplace(step0, stepText);

var drawText = GetDecompiledText(draw0);
if (!drawText.Contains("Item Giver v0.3.1: draw genuine Controls row 9"))
    drawText += @"\n{draw_cs}\n";
importGroup.QueueReplace(draw0, drawText);

importGroup.Import();
ScriptMessage("Item Giver v0.3.1 installed for Chapter {chapter}. Controls row: ITEM GIVER; default key: I.");
'''


def resource_names(group: dict, key: str) -> set[str]:
    return {item["name"] for item in group.get(key, [])}


def strip_serialization_noise(source: Path, destination: Path) -> None:
    """Remove UTMT-only Sound diffs and reject every other unexpected resource."""
    with zipfile.ZipFile(source, "r") as zin:
        manifest = json.loads(zin.read("g3mpatch.json"))
        resources = manifest.get("resources", {})

        unexpected_types = set(resources) - {"Scripts", "CodeEntries", "Sounds"}
        if unexpected_types:
            raise RuntimeError(f"Unexpected G3M resource types: {sorted(unexpected_types)}")

        scripts = resources.get("Scripts", {})
        code = resources.get("CodeEntries", {})
        if resource_names(scripts, "new") != EXPECTED_SCRIPT_NEW:
            raise RuntimeError("Unexpected new Script resource set")
        if resource_names(scripts, "changed") or resource_names(scripts, "deleted"):
            raise RuntimeError("Unexpected changed/deleted Scripts")
        if resource_names(code, "changed") != EXPECTED_CODE_CHANGED:
            raise RuntimeError("Unexpected changed CodeEntry set")
        if resource_names(code, "new") != EXPECTED_CODE_NEW:
            raise RuntimeError("Unexpected new CodeEntry set")
        if resource_names(code, "deleted"):
            raise RuntimeError("Unexpected deleted CodeEntries")

        resources.pop("Sounds", None)
        manifest["resources"] = resources

        changed = sum(len(v.get("changed", [])) for v in resources.values())
        new = sum(len(v.get("new", [])) for v in resources.values())
        deleted = sum(len(v.get("deleted", [])) for v in resources.values())
        changed_files = sum(sum(len(x.get("files", {})) for x in v.get("changed", [])) for v in resources.values())
        new_files = sum(sum(len(x.get("files", {})) for x in v.get("new", [])) for v in resources.values())
        manifest["statistics"] = {
            "totalChanged": changed,
            "totalNew": new,
            "totalDeleted": deleted,
            "totalChangedFiles": changed_files,
            "totalNewFiles": new_files,
        }
        manifest["applyPlan"] = {
            "mode": "standard",
            "requiresCodePipeline": True,
            "requiresTexturePipeline": False,
            "requiresAssetReorder": True,
            "requiresHeavyFinalize": True,
            "supportsDirectResourceApply": False,
            "simpleResourceTypes": [],
            "heavyResourceTypes": ["CodeEntries", "Scripts"],
        }

        with zipfile.ZipFile(destination, "w", zipfile.ZIP_DEFLATED, compresslevel=9) as zout:
            for info in zin.infolist():
                if info.filename == "g3mpatch.json" or info.filename.startswith("Sounds/"):
                    continue
                zout.writestr(info, zin.read(info.filename))
            zout.writestr("g3mpatch.json", json.dumps(manifest, indent=2).encode("utf-8"))


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--game-root", type=Path, required=True,
                        help="Folder containing chapter1_windows ... chapter5_windows")
    parser.add_argument("--utmt", type=Path, required=True, help="UndertaleModCli executable")
    parser.add_argument("--g3mtool", type=Path, required=True, help="G3MTool 1.2.1-compatible executable")
    parser.add_argument("--output", type=Path, default=ROOT / "Item_Giver_Mode_v0.3.1_Deltamod.zip")
    args = parser.parse_args()

    # Historical filename retained so older repository links keep working.
    # In v0.3.1 it contains the isolated helper-script body, not obj_time's full Draw body.
    runtime = (ROOT / "ItemGiver_Draw75.gml").read_text(encoding="utf-8")
    step_append = CONTROLS_STEP_APPEND
    draw_append = CONTROLS_DRAW_APPEND

    with tempfile.TemporaryDirectory(prefix="item_giver_v031_") as tmp_name:
        tmp = Path(tmp_name)
        stage = tmp / "package"
        patches = stage / "patches"
        patches.mkdir(parents=True)
        for name in ("meta.json", "modding.xml", "README.txt", "LICENSE"):
            shutil.copy2(ROOT / name, stage / name)

        for chapter in CHAPTERS:
            clean = find_clean(args.game_root, chapter)
            modified = tmp / f"ch{chapter}_modified.win"
            csx = tmp / f"ItemGiver_ch{chapter}_v031.csx"
            raw_patch = tmp / f"ItemGiver_ch{chapter}_raw.g3mpatch"
            final_patch = patches / f"ItemGiver_ch{chapter}.g3mpatch"
            csx.write_text(make_csx(chapter, runtime, step_append, draw_append), encoding="utf-8", newline="\n")

            run([args.utmt, "load", clean, "--output", modified, "--scripts", csx])
            run([args.g3mtool, "patch", "create", clean, modified, raw_patch])
            strip_serialization_noise(raw_patch, final_patch)
            run([args.g3mtool, "patch", "validate", final_patch, "--data", clean])

        args.output.parent.mkdir(parents=True, exist_ok=True)
        with zipfile.ZipFile(args.output, "w", zipfile.ZIP_DEFLATED, compresslevel=9) as outzip:
            for path in sorted(stage.rglob("*")):
                if path.is_file():
                    outzip.write(path, path.relative_to(stage).as_posix())
        with zipfile.ZipFile(args.output) as check:
            bad = check.testzip()
            if bad:
                raise RuntimeError(f"ZIP integrity failure: {bad}")
        print("Wrote", args.output)


if __name__ == "__main__":
    main()
