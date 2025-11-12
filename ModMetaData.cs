using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;
using Version = SemanticVersioning.Version;


namespace Artem;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.crackbone.artem-wtt";
    public override string Name { get; init; } = "Artem";
    public override string Author { get; init; } = "Crackbone";
    public override List<string>? Contributors { get; init; } = ["Crackbone", "WTT Team"];
    public override Version Version { get; init; } = new("3.0.0");
    public override Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; } = [""];
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; } = "https://github.com/sp-tarkov/server-mod-examples";
    public override bool? IsBundleMod { get; init; } = true;
    public override string? License { get; init; } = "MIT";
}