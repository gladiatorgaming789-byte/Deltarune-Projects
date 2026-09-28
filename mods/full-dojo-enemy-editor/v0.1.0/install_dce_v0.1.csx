// Full Dojo Enemy Editor v0.1.0 installer
// Target: Full Dojo Customizer v0.14 / DELTARUNE Chapter 4
// Usage:
// UndertaleModCli load dataPATCHED.win -s install_dce_v0.1.csx -o dce.data.win

using System;
using System.IO;
using System.Linq;
using UndertaleModLib.Compiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

string root = Path.GetDirectoryName(ScriptPath);
string gml = Path.Combine(root, "gml");

string[] files = new[] {
    "DCE_Init.gml",
    "DCE_CreateEnemy.gml",
    "DCE_CreateAttack.gml",
    "DCE_AddStep.gml",
    "DCE_AddBuiltinStep.gml",
    "DCE_FindEnemy.gml",
    "DCE_FindAttack.gml",
    "DCE_RegisterEnemy.gml",
    "DCE_SaveLoad.gml",
    "DCE_AttackRuntime.gml",
    "DCE_EditorModel.gml",
    "DCE_Validation.gml",
    "DCE_UI.gml"
};

foreach (string file in files)
{
    if (!File.Exists(Path.Combine(gml, file)))
    {
        ScriptError("Missing source: " + file);
        return;
    }
}

var manager = Data.GameObjects.ByName("obj_dojomanage");
if (manager is null)
{
    ScriptError("Full Dojo Customizer v0.14 was not detected: obj_dojomanage is missing.");
    return;
}

var step = manager.EventHandlerFor(EventType.Step, Data);
var draw = manager.EventHandlerFor(EventType.Draw, Data);

if (step is null || draw is null)
{
    ScriptError("obj_dojomanage Step/Draw events could not be resolved.");
    return;
}

GlobalDecompileContext context = new(Data);
IDecompileSettings settings = Data.ToolInfo.DecompilerSettings;

CodeImportGroup group = new(Data, context, settings)
{
    AutoCreateAssets = true,
    MainThreadAction = MainThreadAction
};

foreach (string file in files)
{
    string source = File.ReadAllText(Path.Combine(gml, file));
    group.QueueReplace("gml_GlobalScript_" + Path.GetFileNameWithoutExtension(file), source);
}

// Open the editor with F10. The UI consumes input only while open.
group.QueueAppend(step, @"
if (!global.dce_editor_open && keyboard_check_pressed(vk_f10)) {
    DCE_UI_Open();
}

DCE_UI_HandleStep();
");

group.QueueAppend(draw, @"
DCE_UI_Draw();
");

group.Import();

foreach (string file in files)
{
    string name = "gml_GlobalScript_" + Path.GetFileNameWithoutExtension(file);
    if (Data.Code.FirstOrDefault(c => c.Name.Content == name) is null)
    {
        ScriptError("Missing installed CodeEntry: " + name);
        return;
    }
}

ScriptMessage("Full Dojo Enemy Editor v0.1.0 installed. Press F10 inside the Dojo manager to open it.");
