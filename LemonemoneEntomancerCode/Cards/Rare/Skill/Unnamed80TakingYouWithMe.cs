using BaseLib.Patches.Features;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Skill;

public class Unnamed80TakingYouWithMe() : LemonemoneEntomancerCard(
    2, CardType.Skill, CardRarity.Rare,
    CustomTargetType.Everyone)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BuzzedVar(30)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        MyEnums.Fly,
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await MyActions.Buzzed(choiceContext, [Owner.Creature, ..this.AllEnemies], this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}