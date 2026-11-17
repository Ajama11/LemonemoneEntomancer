using BaseLib.Abstracts;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.CombatHistoryEntries;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed66RetribugtionPower() : LemonemoneEntomancerPower, IOnBugsChanged, IAfterBuzzedLost, IHasSecondAmount
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    private const string ExtraBuzzed = "ExtraBuzzed";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar(ExtraBuzzed, 0)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(MyEnums.Bugs),
        ..new BuzzedBundle().HoverTips
    ];

    public async Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta, CardModel? card, List<Creature>? targets)
    {
        if (player != Owner.Player) return;
        if (delta <= 0) return;

        await PowerCmd.Apply<BuzzedPower>(choiceContext,
            Owner, Amount,
            Owner, null);
    }
    
    // This sadly needs a special hook for timing reasons. If Buzzed is after this power, as it usually will be due to it being consumed and then reapplied constantly, then this AfterPowerAmountChanged would've ran before Buzzed's AfterPowerAmountChanged could add the BuzzedLostEntry
    public async Task AfterBuzzedLost(PlayerChoiceContext choiceContext, PowerModel buzzed, decimal amount)
    {
        if (buzzed.Owner != Owner) return;

        int timesLostThisTurn = CombatManager.Instance.History.Entries.OfType<BuzzedLostEntry>()
            .Count(entry =>
                entry.HappenedThisTurn(CombatState) &&
                entry.Actor == Owner);
        if (timesLostThisTurn > 1) return;
        
        Flash();

        Creature? randomEnemy = this.RandomEnemy;
        if (randomEnemy == null) return;

        decimal amountToApply = (Math.Abs(amount) * Amount) + DynamicVars[ExtraBuzzed].BaseValue;
        
        await PowerCmd.Apply<BuzzedPower>(choiceContext,
            randomEnemy, amountToApply,
            Owner, null);
    }
    
    public void IncreaseExtraBuzzed(int amount)
    {
        AssertMutable();
        DynamicVars[ExtraBuzzed].BaseValue += amount;
        this.InvokeSecondAmountChanged();
    }

    public string GetSecondAmount()
    {
        return DynamicVars[ExtraBuzzed].IntValue > 0 ?
            $"+{DynamicVars[ExtraBuzzed].IntValue}" :
            "";
    }
}