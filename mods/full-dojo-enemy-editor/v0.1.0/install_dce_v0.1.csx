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
    "DCE_BattleIntegration.gml",
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

var managerCreate = manager.EventHandlerFor(EventType.Create, Data);
var managerStep = manager.EventHandlerFor(EventType.Step, Data);
var managerDraw = manager.EventHandlerFor(EventType.Draw, Data);

var battleStep = Data.Code.ByName("gml_Object_obj_battlecontroller_Step_0");
var nativeControllerStep = Data.Code.ByName("gml_Object_obj_dbulletcontroller_Step_0");
var enemyDataGlobal = Data.Code.ByName("gml_GlobalScript_scr_dojo_enemydata");
var bulletSpawnerGlobal = Data.Code.ByName("gml_GlobalScript_scr_bulletspawner");

if (managerCreate is null || managerStep is null || managerDraw is null)
{
    ScriptError("obj_dojomanage Create/Step/Draw could not be resolved.");
    return;
}

if (battleStep is null || nativeControllerStep is null || enemyDataGlobal is null || bulletSpawnerGlobal is null)
{
    ScriptError("One or more Full Dojo battle integration code entries are missing.");
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

// Replace the original native spawner global script in-place so existing
// callers continue to resolve scr_bulletspawner through the DCE bridge.
group.QueueReplace(bulletSpawnerGlobal, File.ReadAllText(Path.Combine(gml, "DCE_BulletSpawner.gml")));

// Load addon data before the manager UI is opened so custom enemies are selectable immediately.
group.QueueAppend(managerCreate, @"
DCE_Load();
DCE_RegisterAllEnemies();
");

// Open the editor with F10. The UI consumes input only while open.
group.QueueAppend(managerStep, @"
if (!global.dce_editor_open && keyboard_check_pressed(vk_f10)) {
    DCE_UI_Open();
}

DCE_UI_HandleStep();
");

group.QueueAppend(managerDraw, @"
DCE_UI_Draw();
");

// Register custom IDs when the Dojo manager is created. This avoids modifying
// Full Dojo's nested anonymous setup function, which UTMT cannot safely append to.

// Return complete custom stats when Full Dojo asks for a custom enemy by ID.
// The replace is performed against the root global script so UTMT can preserve
// the existing anonymous-function code entries.
string dceGenerateSearch = "function scr_dm_generate_enemy(arg0 = 5)
{";
string dceGenerateReplacement = @"function scr_dm_generate_enemy(arg0 = 5)
{
    var __dce_generated = DCE_FindEnemy(argument0);
    if (!is_undefined(__dce_generated)) {
        var __dce_result = {
            type: __dce_generated.id,
            hp: __dce_generated.hp,
            at: __dce_generated.at,
            df: __dce_generated.df
        };
        if (variable_struct_exists(__dce_generated, ""sp"")) {
            __dce_result.sp = __dce_generated.sp;
        }
        return __dce_result;
    }
";
group.QueueFindReplace(enemyDataGlobal, dceGenerateSearch, dceGenerateReplacement, true);

// The existing enemy objects already call scr_bulletspawner. The replacement
// wrapper routes custom enemies into the addon timeline and leaves vanilla
// callers on the original controller path.
group.QueueAppend(battleStep, @"
DCE_BattleStep();
");

// The enemy object writes its normal spawntype onto the returned controller.
// Proxy controllers are reset before native pattern dispatch so they stay inert.
group.QueuePrepend(nativeControllerStep, @"
if (variable_instance_exists(id, ""dce_proxy"") && dce_proxy) {
    type = 999999;
}
");

group.Import();

string[] requiredEntries = new[] {
    "gml_GlobalScript_DCE_Init",
    "gml_GlobalScript_DCE_CreateEnemy",
    "gml_GlobalScript_DCE_CreateAttack",
    "gml_GlobalScript_DCE_AddStep",
    "gml_GlobalScript_DCE_AddBuiltinStep",
    "gml_GlobalScript_DCE_FindEnemy",
    "gml_GlobalScript_DCE_FindAttack",
    "gml_GlobalScript_DCE_RegisterEnemy",
    "gml_GlobalScript_DCE_SaveLoad",
    "gml_GlobalScript_DCE_AttackRuntime",
    "gml_GlobalScript_DCE_BattleIntegration",
    "gml_GlobalScript_DCE_EditorModel",
    "gml_GlobalScript_DCE_Validation",
    "gml_GlobalScript_DCE_UI"
};

foreach (string name in requiredEntries)
{
    if (Data.Code.FirstOrDefault(c => c.Name.Content == name) is null)
    {
        ScriptError("Missing installed CodeEntry: " + name);
        return;
    }
}

ScriptMessage("Full Dojo Enemy Editor v0.1.0 runtime bridge installed. Press F10 inside the Dojo manager.");
