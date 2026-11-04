using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class BugStrengthPower() : LemonemoneEntomancerPower, IModifyBugDamageAdditive
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(MyEnums.Bugs)
    ];

    public int ModifyBugDamageAdditive(Creature? target, int amount, Creature dealer)
    {
        return dealer == Owner ? Amount : 0;
    }
}