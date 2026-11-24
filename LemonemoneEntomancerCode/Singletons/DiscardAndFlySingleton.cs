using BaseLib.Abstracts;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;

public class DiscardAndFlySingleton() : CustomSingletonModel(HookType.Combat)
{
    public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
    {
        if (card is IHasWhenDiscardedEffect { ShouldDoWhenDiscarded: true } || 
            card.Keywords.Contains(MyEnums.Fly))
        {
            CardPile destinationPile = card.Pile ?? PileType.None.GetPile(card.Owner);
            CardPilePosition destinationPosition = CardPilePosition.Bottom;
            
            if (card is IHasDifferentDiscardDestination specialCard)
            {
                destinationPile = specialCard.DiscardDestinationPile.GetPile(card.Owner);
                destinationPosition = specialCard.DiscardDestinationPosition;
            }

            await CardPileCmd.Add(card, PileType.Play);
            
            await Cmd.CustomScaledWait(0.3f, 0.6f);

            if (card is IHasWhenDiscardedEffect { ShouldDoWhenDiscarded: true } whenDiscarded)
            {
                await whenDiscarded.WhenDiscarded(choiceContext);
            }

            if (card.Keywords.Contains(MyEnums.Fly))
            {
                var target = card.Rng.CombatTargets.NextItem(card.AllEnemies);
                if (target == null) return;

                await PowerCmd.Apply<BuzzedPower>(choiceContext,
                    target, 3,
                    card.Owner.Creature, null);
            }
            
            await CardPileCmd.Add(card, destinationPile, destinationPosition);
        }
        else if (card is Swarm { DiscardDestinationPile: PileType.Draw } swarm)
        {
            await CardPileCmd.Add(swarm, swarm.DiscardDestinationPile, swarm.DiscardDestinationPosition);
        }

        await MyHooks.AfterCardDiscardedAfterFlyCheck(card.Owner, card);
    }
}