using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Character;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Potions;

[Pool(typeof(LemonemoneEntomancerPotionPool))]
public abstract class LemonemoneEntomancerPotion : CustomPotionModel
{
    public override string? CustomPackedImagePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    public override string? CustomPackedOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}