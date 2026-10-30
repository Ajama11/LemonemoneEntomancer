using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using MegaCrit.Sts2.Core.Entities.Players;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Systems;

public static class Bugs
{
    public static readonly SpireField<PlayerCombatState, int> BugAmount = new(() => 0);
    public static readonly SpireField<PlayerCombatState, int> BugSlots = new(() => 7);

    public static async Task GainBugs(Player player, int amount)
    {
        await ChangeBugs(player, amount);
    }
    
    public static async Task SpendBugs(Player player, int amount, bool silentlyLose = false)
    {
        await ChangeBugs(player, -amount, silentlyLose);
    }

    public static void GainBugSlots(Player player, int amount)
    {
        PlayerCombatState? combatState = player.PlayerCombatState;
        if (combatState == null) return;

        BugSlots[combatState] += amount;
    }

    public static async Task ChangeBugs(Player player, int delta, bool silentlyLose = false)
    {
        PlayerCombatState? combatState = player.PlayerCombatState;
        if (combatState == null) return;

        int previousBugAmount = BugAmount[combatState];

        BugAmount[combatState] = Math.Clamp(BugAmount[combatState] + delta, 0, BugSlots[combatState]);
        int actualDelta = BugAmount[combatState] - previousBugAmount;

        if (!silentlyLose && actualDelta != 0) await MyHooks.OnBugsChanged(player, actualDelta);
    }
}