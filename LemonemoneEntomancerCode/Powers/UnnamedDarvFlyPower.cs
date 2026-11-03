using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class UnnamedDarvFlyPower() : LemonemoneEntomancerPower, IAfterCardDiscardedAfterFlyCheck
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..new FlyBundle().HoverTips
    ];

    public Task AfterCardDiscardedAfterFlyCheck(PlayerChoiceContext choiceContext, CardModel card)
    {
        if (!card.Keywords.Contains(MyEnums.Fly))
            card.AddKeyword(MyEnums.Fly);
        
        return Task.CompletedTask;
    }
}