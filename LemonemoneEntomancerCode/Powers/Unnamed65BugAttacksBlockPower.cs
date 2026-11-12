using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed65BugAttacksBlockPower()
    : LemonemoneEntomancerPower, IOnBugAttackHit
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(MyEnums.Bugs),
        ..new BuzzedBundle().HoverTips
    ];

    public async Task OnBugAttackHit(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (dealer != Owner) return;
        if (!target.IsEnemy) return;
        if (target.GetPowerAmount<BuzzedPower>() <= 0) return;

        await CreatureCmd.GainBlock(Owner, Amount, BlockProps.nonCardUnpowered, null, true);
    }
}