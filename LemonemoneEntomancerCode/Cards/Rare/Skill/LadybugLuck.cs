using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Skill;

public class LadybugLuck() : LemonemoneEntomancerCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromCard<Swarm>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<Swarm> swarms = Owner.Hand.OfType<Swarm>().ToList();

        foreach (var swarm in swarms)
        {
            swarm.DiscardDestinationPile = PileType.Draw;
            swarm.DiscardDestinationPosition = CardPilePosition.Random;
        }

        await CardCmd.Discard(choiceContext, Owner.Hand);

        // await CardPileCmd.Add(swarms, PileType.Draw, CardPilePosition.Random);
        foreach (var swarm in swarms)
        {
            swarm.DiscardDestinationPile = PileType.Discard;
            swarm.DiscardDestinationPosition = CardPilePosition.Bottom;
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}