using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Spt.Config;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Servers;
using SPTarkov.Server.Core.Utils;
using System.Reflection;
using Path = System.IO.Path;


namespace Artem;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class WTTArtem(
    ModHelper modHelper,
    ImageRouter imageRouter,
    ConfigServer configServer,
    TimeUtil timeUtil,
    WTTArtemHelper wttArtemHelper,
    WTTServerCommonLib.WTTServerCommonLib wttCommon
)
    : IOnLoad
{
    private readonly TraderConfig _traderConfig = configServer.GetConfig<TraderConfig>();
    private readonly RagfairConfig _ragfairConfig = configServer.GetConfig<RagfairConfig>();


    public  async Task OnLoad()
    {
        
        var pathToMod = modHelper.GetAbsolutePathToModFolder(Assembly.GetExecutingAssembly());
        
        Assembly assembly = Assembly.GetExecutingAssembly();
        
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        await wttCommon.CustomQuestZoneService.CreateCustomQuestZones(assembly);
        await wttCommon.CustomLocaleService.CreateCustomLocales(assembly);
        
        var traderImagePath = Path.Combine(pathToMod, "res/66bf757f27d0b097db0acea5.jpg");
        var traderBase = modHelper.GetJsonDataFromFile<TraderBase>(pathToMod, "db/base.json");
        
        imageRouter.AddRoute(traderBase.Avatar.Replace(".jpg", ""), traderImagePath);
        wttArtemHelper.SetTraderUpdateTime(_traderConfig, traderBase, timeUtil.GetHoursAsSeconds(1), timeUtil.GetHoursAsSeconds(2));
        
        _ragfairConfig.Traders.TryAdd(traderBase.Id, true);
        
        wttArtemHelper.AddTraderWithEmptyAssortToDb(traderBase);
        
        wttArtemHelper.AddTraderToLocales(traderBase, "Artem", "[REDACTED]");
        
        await wttCommon.CustomQuestService.CreateCustomQuests(assembly);
        await wttCommon.CustomClothingService.CreateCustomClothing(assembly);
        
        var assort = modHelper.GetJsonDataFromFile<TraderAssort>(pathToMod, "db/assort.json");

        
        wttArtemHelper.OverwriteTraderAssort(traderBase.Id, assort);

        
        await Task.CompletedTask;
    }
}