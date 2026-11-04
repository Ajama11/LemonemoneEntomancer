using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;

public class BugStrengthBundle() : BundledHoverTip(
    nameof(BugStrengthBundle),
    [
        HoverTipFactory.FromPower<BugStrengthPower>(),
        HoverTipFactory.Static(MyEnums.Bugs)
    ]
)
{ }