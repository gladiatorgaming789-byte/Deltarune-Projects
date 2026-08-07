using System;
using System.Linq;
using UndertaleModLib.Models;
using UndertaleModLib.Decompiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

if (!Data.IsVersionAtLeast(2023, 6))
{
    ScriptError("Item Giver requires the DELTARUNE full release.");
    return;
}

string displayName = Data?.GeneralInfo?.DisplayName?.Content?.ToLowerInvariant() ?? "";
if (!displayName.Contains("chapter __CHAPTER__") && !displayName.Contains("chapitre __CHAPTER__"))
{
    ScriptError("This Item Giver source patch is for Chapter __CHAPTER__ only.");
    return;
}

GlobalDecompileContext globalDecompileContext = new(Data);
IDecompileSettings decompilerSettings = new DecompileSettings();
UndertaleModLib.Compiler.CodeImportGroup importGroup = new(Data, globalDecompileContext, decompilerSettings)
{
    ThrowOnNoOpFindReplace = true
};

UndertaleGameObject objTime = Data.GameObjects.ByName("obj_time");
if (objTime == null)
{
    ScriptError("Item Giver could not find DELTARUNE's persistent obj_time controller.");
    return;
}

var drawCode = objTime.EventHandlerFor(EventType.Draw, (uint)75, Data);
var existing = GetDecompiledText(drawCode);
if (existing.Contains("ig_itemgiver_version"))
{
    ScriptMessage("Item Giver v0.2.0 is already installed for Chapter __CHAPTER__.");
    return;
}

importGroup.QueueAppend(drawCode,
@"__GML_BODY__");

importGroup.Import();
ScriptMessage("Item Giver v0.2.0 source patch installed for Chapter __CHAPTER__. Press F7 outside battle.");
