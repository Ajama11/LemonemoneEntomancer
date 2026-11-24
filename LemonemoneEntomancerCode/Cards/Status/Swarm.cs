using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Character;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;

[Pool(typeof(StatusEntomancerCardPool))]
public class Swarm() : LemonemoneEntomancerCard(-1,
    CardType.Status, CardRarity.Status,
    TargetType.Self),
    IHasWhenDiscardedEffect, IHasDifferentDiscardDestination
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        new BugsVar(1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Ethereal,
        CardKeyword.Unplayable
    ];
    
    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs)
    ];

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card != this) return;

        await MyActions.Draw(choiceContext, this);

        await MyActions.GainBugs(this);
    }

    public bool ShouldDoWhenDiscarded => IsUpgraded;
    public async Task WhenDiscarded(PlayerChoiceContext choiceContext)
    {
        await MyActions.GainBugs(this);
    }

    public PileType DiscardDestinationPile { get; set; } = PileType.Discard;
    public CardPilePosition DiscardDestinationPosition { get; set; } = CardPilePosition.Bottom;
}