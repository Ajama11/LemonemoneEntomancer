using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class SweetestNectarPower : CustomTemporaryPowerModelWrapper<SweetNectarPower, BugStrengthPower>
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override LocString Title => new ("powers", $"{Id.Entry}.title");
    
    // public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    // public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}