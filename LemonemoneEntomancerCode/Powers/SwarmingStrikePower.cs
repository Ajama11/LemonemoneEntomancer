using BaseLib.Abstracts;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Common.Attack;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class SwarmingStrikePower : CustomTemporaryPowerModelWrapper<SwarmingStrike, BugStrengthPower>
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    // public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    // public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}