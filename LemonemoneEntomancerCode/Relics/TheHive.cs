using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Relics;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Relics;

public class TheHive() : LemonemoneEntomancerRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    private const string Combat = "Combat";
    private const string Turn = "Turn";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BugsVar(Combat, 1),
        new BugsVar(Turn, 1)
    ];

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom) return;

        await MyActions.GainBugs(Owner, DynamicVars[Combat].IntValue + DynamicVars[Turn].IntValue, false);
    }

    public override async Task AfterEnergyReset(Player player)
    {
        if (player != Owner || player.PlayerCombatState!.TurnNumber == 1) return;
        
        await MyActions.GainBugs(Owner, DynamicVars[Turn].IntValue);
    }
}