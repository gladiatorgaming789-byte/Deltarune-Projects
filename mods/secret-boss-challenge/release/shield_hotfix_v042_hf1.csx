using System;
using UndertaleModLib.Models;
using UndertaleModLib.Compiler;
using UndertaleModLib.Decompiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

GlobalDecompileContext globalDecompileContext = new(Data);
IDecompileSettings decompilerSettings = Data.ToolInfo.DecompilerSettings;

string helperName = "gml_Script_scr_gg_sbc_shield_apply";
string helperGml = @"
global.secret_boss_challenge_shield_active = 1;
global.secret_boss_challenge_shield_target = -1;

for (var __sbc_hf_i = 0; __sbc_hf_i < 3; __sbc_hf_i++)
{
    if (global.char[__sbc_hf_i] != 0 && instance_exists(global.charinstance[__sbc_hf_i]))
    {
        var __sbc_hf_target = global.charinstance[__sbc_hf_i];
        var __sbc_hf_anim = instance_create(__sbc_hf_target.x, __sbc_hf_target.y, obj_healanim);
        __sbc_hf_anim.target = __sbc_hf_target;
        __sbc_hf_anim.image_blend = make_color_rgb(255, 128, 210);
        __sbc_hf_target.tu += 1;
    }
}

global.spelldelay = 20;
";

var helper = Data.Code.ByName(helperName);
if (helper == null)
{
    CodeImportGroup helperGroup = new(Data, globalDecompileContext, decompilerSettings)
    {
        ThrowOnNoOpFindReplace = true,
        MainThreadAction = MainThreadAction
    };
    helperGroup.QueueReplace(helperName, helperGml);
    helperGroup.Import();
    helper = Data.Code.ByName(helperName);
}
if (helper == null)
    throw new Exception("SBC Shield hotfix: failed to create helper CodeEntry");

if (Data.Scripts.ByName("scr_gg_sbc_shield_apply") == null)
    Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("scr_gg_sbc_shield_apply"), Code = helper });
Data.Functions.EnsureDefined("scr_gg_sbc_shield_apply", Data.Strings);

var heroStep = Data.Code.ByName("gml_Object_obj_heroparent_Step_0");
if (heroStep == null)
    throw new Exception("SBC Shield hotfix: missing obj_heroparent Step");

string source = GetDecompiledText(heroStep);
string oldCall = "            scr_spell(global.charspecial[myself], myself);";
string newCall = @"            // SBC v0.4.2 Shield crash hotfix: case 14 bypasses scr_spell's merged local table.
            if (global.charspecial[myself] == 14)
                scr_gg_sbc_shield_apply();
            else
                scr_spell(global.charspecial[myself], myself);";
int count = source.Split(new[] { oldCall }, StringSplitOptions.None).Length - 1;
if (count != 1)
    throw new Exception("SBC Shield hotfix: expected one scr_spell caller, found " + count);
source = source.Replace(oldCall, newCall);

CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true,
    MainThreadAction = MainThreadAction
};
importGroup.QueueReplace(heroStep, source);
importGroup.Import();

ScriptMessage("Secret Boss Challenge Shield hotfix installed: spell 14 bypasses scr_spell.");
