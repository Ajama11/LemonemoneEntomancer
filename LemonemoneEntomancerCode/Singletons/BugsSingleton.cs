using BaseLib.Abstracts;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;

public class BugsSingleton() : CustomSingletonModel(HookType.Combat)
{
    public static readonly SpireField<PlayerCombatState, int> BugAmount = new(() => 0);
    public static readonly SpireField<PlayerCombatState, int> BugSlots = new(() => 7);
    
    public static event Action? LocalBugAmountChanged;
    public static event Action? LocalBugSlotsChanged;
    public static event Action? LocalBugsInWaitingChanged;
    
    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is not BugsInWaitingPower) return Task.CompletedTask;
        
        if (LocalContext.IsMe(power.Owner)) LocalBugsInWaitingChanged?.Invoke();
        
        return Task.CompletedTask;
    }

    public static async Task GainBugs(Player player, int amount, CardModel? card = null, bool playAudio = true)
    {
        await ChangeBugs(player, amount, card);
    }
    
    /// If silently losing Bugs NOT through the widget, call ValidateBugsInWaiting()!
    public static async Task SpendBugs(Player player, int amount, CardModel? card = null, List<Creature>? targets = null, bool silentlyLose = false)
    {
        await ChangeBugs(player, -amount, card, targets, silentlyLose);
    }
    
    /// If silently losing Bugs NOT through the widget, call ValidateBugsInWaiting()!
    public static async Task ChangeBugs(Player player, int delta, CardModel? card = null, List<Creature>? targets = null, bool silentlyLose = false)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;

        int previousBugAmount = BugAmount[combatState];

        BugAmount[combatState] = Math.Clamp(BugAmount[combatState] + delta, 0, BugSlots[combatState]);
        int actualDelta = BugAmount[combatState] - previousBugAmount;

        if (!silentlyLose && actualDelta != 0) await MyHooks.OnBugsChanged(player, actualDelta, card, targets);
        
        if (LocalContext.IsMe(player)) LocalBugAmountChanged?.Invoke();
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
        
        if (LocalContext.IsMe(player)) LocalBugSlotsChanged?.Invoke();
    }

    public static async Task GainBugsInWaiting(Player player, int amount)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;
        var creature = player.Creature;
        
        int preexistingAmount = creature.GetPowerAmount<BugsInWaitingPower>();
        int amountToUse = Math.Min(amount, BugAmount[combatState] - preexistingAmount);

        await PowerCmd.Apply<BugsInWaitingPower>(new ThrowingPlayerChoiceContext(),
            creature, amountToUse,
            null, null);
    }

    public static async Task LoseBugsInWaiting(Player player, int amount)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;
        var creature = player.Creature;

        var power = creature.GetPower<BugsInWaitingPower>();
        if (power == null) return;

        await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(),
            power, -amount, null, null);
    }
    
    /// If Bugs are lost via another effect, this makes sure the amount of Bugs-in-Waiting doesn't exceed the amount of Bugs that could be spent.
    public static async Task ValidateBugsInWaiting(Player player)
    {
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;
        var creature = player.Creature;
        
        var power = creature.GetPower<BugsInWaitingPower>();
        if (power == null) return;
        
        if (power.Amount <= BugAmount[combatState]) return;

        await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(),
            power, BugAmount[combatState] - power.Amount,
            null, null);
    }
}