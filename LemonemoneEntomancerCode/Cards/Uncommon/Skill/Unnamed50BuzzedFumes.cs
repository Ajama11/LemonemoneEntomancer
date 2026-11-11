using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Skill;

public class Unnamed50BuzzedFumes() : LemonemoneEntomancerCard(
    0, CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override bool HasEnergyCostX => true;

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        MyEnums.Fly
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        new BuzzedBundle()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int xValue = ResolveEnergyXValue();
        if (IsUpgraded) xValue++;

        await PowerCmd.Apply<Unnamed50BuzzedFumesPower>(choiceContext,
            play.Target!, xValue,
            Owner.Creature, this);
    }
}