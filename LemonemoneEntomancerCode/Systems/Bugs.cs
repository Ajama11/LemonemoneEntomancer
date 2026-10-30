using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

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

    public static async Task ChangeBugs(Player player, int delta, bool silentlyLose = false)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;

        int previousBugAmount = BugAmount[combatState];

        BugAmount[combatState] = Math.Clamp(BugAmount[combatState] + delta, 0, BugSlots[combatState]);
        int actualDelta = BugAmount[combatState] - previousBugAmount;

        if (!silentlyLose && actualDelta != 0) await MyHooks.OnBugsChanged(player, actualDelta);
        
        MainFile.Logger.Warn($"Bugs changed by {actualDelta} ({delta}). Current Bugs: {BugAmount[combatState]} / {BugSlots[combatState]}");
    }
    
    public static void GainBugSlots(Player player, int amount)
    {
        ChangeBugSlots(player, amount);
    }

    public static void ChangeBugSlots(Player player, int delta)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;

        BugSlots[combatState] = Math.Max(0, BugSlots[combatState] + delta);
        if (delta < 0) BugAmount[combatState] = Math.Min(BugAmount[combatState], BugSlots[combatState]);
        
        MainFile.Logger.Warn($"Bug Slots changed by {delta}. Current Bugs: {BugAmount[combatState]} / {BugSlots[combatState]}");
    }

    public static async Task GainBugsInWaiting(Player player, int amount)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;
        var creature = player.Creature;
        
        int preexistingAmount = creature.GetPowerAmount<BugsInWaitingPower>();
        int amountToUse = Math.Min(amount, BugAmount[combatState]) - preexistingAmount;

        await PowerCmd.Apply<BugsInWaitingPower>(new ThrowingPlayerChoiceContext(),
            creature, amountToUse,
            creature, null);
    }

    public static async Task LoseBugsInWaiting(Player player, int amount)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;
        var creature = player.Creature;

        var power = creature.GetPower<BugsInWaitingPower>();
        if (power == null) return;

        await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(),
            power, -amount, creature, null);
    }
}