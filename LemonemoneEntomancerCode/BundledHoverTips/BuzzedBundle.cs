using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;

public class BuzzedBundle() : BundledHoverTip(
    nameof(BuzzedBundle),
    [
        HoverTipFactory.FromPower<BuzzedPower>(),
        HoverTipFactory.FromCard<Swarm>()
    ]
)
{ }