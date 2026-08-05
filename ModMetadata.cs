using JetBrains.Annotations;
using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;
using Version = SemanticVersioning.Version;

namespace WTTArtem;

[UsedImplicitly]
public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.crackbone.artem-wtt";
    public string Name { get; init; } = "Artem";
    public string Author { get; init; } = "Crackbone";
    public List<string>? Contributors { get; init; } = ["Crackbone", "The WTT Team"];
    public Version Version { get; init; } = new("3.0.0");
    public Range SptVersion { get; init; } = new("~4.0.0");
    public bool HasPrepatcher { get; init; }
    public List<string>? Incompatibilities { get; init; } = [];
    public Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~3.0.0") }
    };
    public string? Url { get; init; } = "https://github.com/WelcomeToThursday/WTT-Artem";
    public string License { get; init; } = "MIT";
}