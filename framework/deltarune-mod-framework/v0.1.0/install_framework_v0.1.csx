// DELTARUNE Mod Framework v0.1.0 installer.
// Run with UTMT CLI 0.9.1.2:
//   UndertaleModCli load data.win -s install_framework_v0.1.csx -o framework_v0.1.0.data.win
// The script expects a sibling `gml/` directory containing the framework sources.

using System;
using System.IO;
using System.Linq;
using UndertaleModLib.Compiler;
using Underanalyzer.Decompiler;

EnsureDataLoaded();

if (Data.IsYYC())
{
    ScriptError("DELTARUNE Mod Framework requires a non-YYC data file because it adds GML CodeEntries.");
    return;
}

string root = Path.GetDirectoryName(ScriptPath);
string gml = Path.Combine(root, "gml");
string[] files = new[]
{
    "DMF_Init.gml",
    "DMF_Mod_Register.gml",
    "DMF_Mod_Query.gml",
    "DMF_Capability.gml",
    "DMF_Event.gml"
};

foreach (string file in files)
{
    if (!File.Exists(Path.Combine(gml, file)))
    {
        ScriptError($"Missing framework source: {file}");
        return;
    }
}

GlobalDecompileContext decompileContext = new(Data);
IDecompileSettings settings = Data.ToolInfo.DecompilerSettings;
CodeImportGroup group = new(Data, decompileContext, settings)
{
    AutoCreateAssets = true,
    MainThreadAction = MainThreadAction
};

foreach (string file in files)
{
    string source = File.ReadAllText(Path.Combine(gml, file));
    string[] lines = source.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
    string code = source;
    string name = Path.GetFileNameWithoutExtension(file);
    group.QueueReplace("gml_GlobalScript_" + name, code);
}

group.Import();

string[] expected = files
    .Select(f => "gml_GlobalScript_" + Path.GetFileNameWithoutExtension(f))
    .ToArray();

foreach (string name in expected)
{
    if (Data.Code.FirstOrDefault(c => c.Name.Content == name) is null)
    {
        ScriptError("Framework installation did not create expected CodeEntry: " + name);
        return;
    }
}

ScriptMessage("DELTARUNE Mod Framework v0.1.0 installed: Mod Registry, Capability Registry, Event Bus.");
