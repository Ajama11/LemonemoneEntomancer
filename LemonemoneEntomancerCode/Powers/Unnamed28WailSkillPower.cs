using BaseLib.Abstracts;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Common.Skill;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed28WailSkillPower : CustomTemporaryPowerModelWrapper<Unnamed28WailSkill, StrengthPower>
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override bool InvertInternalPowerAmount => true;

    // public override LocString Title => new ("powers", $"{Id.Entry}.title");

    // public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    // public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}