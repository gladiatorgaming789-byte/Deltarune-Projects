using System;
using UndertaleModLib.Models;
using UndertaleModLib.Compiler;

EnsureDataLoaded();

UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, null, Data.ToolInfo.DecompilerSettings)
{
    ThrowOnNoOpFindReplace = true,
    MainThreadAction = MainThreadAction
};

int CountOccurrences(string source, string value)
{
    if (String.IsNullOrEmpty(value)) return 0;
    int count = 0, index = 0;
    while ((index = source.IndexOf(value, index, StringComparison.Ordinal)) >= 0)
    {
        count++;
        index += value.Length;
    }
    return count;
}

void PatchCode(string codeName, params (string OldText, string NewText, string Label)[] replacements)
{
    UndertaleCode code = Data.Code.ByName(codeName);
    if (code is null) throw new Exception($"SBC v0.4.2: missing {codeName}");
    string source = GetDecompiledText(code);
    string patched = source;
    foreach (var r in replacements)
    {
        int n = CountOccurrences(patched, r.OldText);
        if (n != 1)
            throw new Exception($"SBC v0.4.2: {r.Label} expected 1 match, found {n}");
        patched = patched.Replace(r.OldText, r.NewText, StringComparison.Ordinal);
    }
    if (!String.Equals(source, patched, StringComparison.Ordinal))
        importGroup.QueueReplace(code, patched);
}

if (Data.Code.ByName("gml_Object_obj_pink_enemy_Create_0") is null)
    throw new Exception("SBC v0.4.2 targets DELTARUNE Chapter 5");

PatchCode("gml_GlobalScript_scr_spellinfo",
    (@"        case 14:
            spellname = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name"");
            spellnameb = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name_b"");
            spelldescb = stringsetloc(""Shield#ally"", ""secret_boss_challenge_shield_spell_desc_b"");
            spelldesc = stringsetloc(""Reduces damage to one party member by about 75%#until the enemy attack ends."", ""secret_boss_challenge_shield_spell_desc"");
            spelltarget = 1;
            cost = 162.5;
            spellusable = 0;
            break;",
     @"        case 14:
            spellname = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name"");
            spellnameb = stringsetloc(""Shield"", ""secret_boss_challenge_shield_spell_name_b"");
            spelldescb = stringsetloc(""Shield#party"", ""secret_boss_challenge_shield_spell_desc_b"");
            spelldesc = stringsetloc(""Reduces damage to the whole party by about 75%#until the enemy attack ends."", ""secret_boss_challenge_shield_spell_desc"");
            spelltarget = 0;
            cost = 187.5;
            spellusable = 0;
            break;",
     "whole-party Shield info and 75 TP cost")
);

PatchCode("gml_GlobalScript_scr_spell",
    (@"        case 14:
            global.secret_boss_challenge_shield_active = 1;
            global.secret_boss_challenge_shield_target = star;
            with (global.charinstance[star])
            {
                var __sbc_shield_anim = instance_create(x, y, obj_healanim);
                __sbc_shield_anim.target = id;
                __sbc_shield_anim.image_blend = make_color_rgb(255, 128, 210);
                tu += 1;
            }
            global.spelldelay = 20;
            break;",
     @"        case 14:
            global.secret_boss_challenge_shield_active = 1;
            global.secret_boss_challenge_shield_target = -1;
            for (var __sbc_shield_i = 0; __sbc_shield_i < 3; __sbc_shield_i++)
            {
                if (global.char[__sbc_shield_i] != 0 && instance_exists(global.charinstance[__sbc_shield_i]))
                {
                    with (global.charinstance[__sbc_shield_i])
                    {
                        var __sbc_shield_anim = instance_create(x, y, obj_healanim);
                        __sbc_shield_anim.target = id;
                        __sbc_shield_anim.image_blend = make_color_rgb(255, 128, 210);
                        tu += 1;
                    }
                }
            }
            global.spelldelay = 20;
            break;",
     "whole-party Shield execution")
);

PatchCode("gml_GlobalScript_scr_spelltext",
    (@"        case 14:
            global.msg[0] = stringsetsubloc(""* ~1 cast SHIELD on ~2!/%"", global.charname[global.char[caster]], global.charname[global.char[star]], ""secret_boss_challenge_shield_cast"");
            break;",
     @"        case 14:
            global.msg[0] = stringsetsubloc(""* ~1 cast SHIELD on the party!/%"", global.charname[global.char[caster]], ""secret_boss_challenge_shield_cast"");
            break;",
     "whole-party Shield battle text")
);

PatchCode("gml_GlobalScript_scr_damage",
    (@"            if (variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1 && global.secret_boss_challenge_shield_target == target)
            {
                tdamage = max(1, ceil(tdamage * 0.25));
            }",
     @"            if (variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1)
            {
                tdamage = max(1, ceil(tdamage * 0.25));
            }",
     "whole-party Shield single-target damage"),
    (@"                    if (variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1 && global.secret_boss_challenge_shield_target == hpi)
                    {
                        tdamage = max(1, ceil(tdamage * 0.25));
                    }",
     @"                    if (variable_global_exists(""secret_boss_challenge_shield_active"") && global.secret_boss_challenge_shield_active == 1)
                    {
                        tdamage = max(1, ceil(tdamage * 0.25));
                    }",
     "whole-party Shield party damage")
);

PatchCode("gml_Object_obj_grazebox_Create_0",
    (@"    grazetpfactor += 0.1;
    grazesizefactor += 0.1;",
     @"    grazetpfactor += 0.1;
    grazesizefactor += 0.25;",
     "Pink Scarf +25% graze area")
);

PatchCode("gml_GlobalScript_scr_weaponinfo",
    (@"            weapongrazeamttemp = 0.1;
            weapongrazesizetemp = 0.1;",
     @"            weapongrazeamttemp = 0.1;
            weapongrazesizetemp = 0.25;",
     "Pink Scarf graze metadata"),
    (@"            weaponabilitytemp = ""Shield / Graze +10%"";",
     @"            weaponabilitytemp = ""Shield / Area +25%, TP +10%"";",
     "Pink Scarf ability text")
);

importGroup.Import();
