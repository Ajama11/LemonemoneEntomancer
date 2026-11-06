using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;

public class FlyBundle() : BundledHoverTip(
    nameof(FlyBundle),
    [
        HoverTipFactory.FromKeyword(MyEnums.Fly),
        HoverTipFactory.FromPower<BuzzedPower>(),
        HoverTipFactory.FromCard<Swarm>()
    ],
    BundledHoverTipManager.Category.End
)
{ }