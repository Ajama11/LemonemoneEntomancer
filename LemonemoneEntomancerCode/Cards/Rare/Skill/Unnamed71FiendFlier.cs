using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Token;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Skill;

public class Unnamed71FiendFlier() : LemonemoneEntomancerCard(
    0, CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        MyEnums.Fly,
        CardKeyword.Exhaust
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromCard<UnnamedLilGuy>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await MyActions.Draw(choiceContext, this);
        
        int xValue = ResolveEnergyXValue();
        if (IsUpgraded) xValue++;

        await MyActions.CreateCards(ModelDb.Card<UnnamedLilGuy>(), xValue, this);
    }
}