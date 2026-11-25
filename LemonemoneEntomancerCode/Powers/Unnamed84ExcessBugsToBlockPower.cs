using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed84ExcessBugsToBlockPower()
    : LemonemoneEntomancerPower, IHasSecondAmount
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DisplayVar<Unnamed84ExcessBugsToBlockPower>("Display", power => $"{power.Owner.GetPowerAmount<BugsInWaitingPower>() * power.Amount}")
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<BugsInWaitingPower>(),
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.Static(MyEnums.Bugs)
    ];

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner)) return;

        var power = Owner.GetPower<BugsInWaitingPower>();
        if (power == null) return;

        int bugs = power.Amount;
        
        await power.Spend(choiceContext, bugs);

        await CreatureCmd.GainBlock(Owner, bugs * Amount, BlockProps.nonCardUnpowered, null);
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is not (BugsInWaitingPower or Unnamed84ExcessBugsToBlockPower) ||
            power.Owner != Owner)
        {
            return Task.CompletedTask;
        }

        this.InvokeSecondAmountChanged();
        
        return Task.CompletedTask;
    }

    public string GetSecondAmount()
    {
        return $"{Math.Max(0, Owner.GetPowerAmount<BugsInWaitingPower>()) * Amount}";
    }
}