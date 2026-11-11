using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed42ReBuzzedPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..new BuzzedBundle().HoverTips
    ];

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is not BuzzedPower) return;
        if (amount >= 0) return;
        if (power.Owner != Owner) return;

        await PowerCmd.Apply<BuzzedPower>(choiceContext,
        Owner, -amount,
        Applier, null);

        await PowerCmd.Decrement(this);
    }
}