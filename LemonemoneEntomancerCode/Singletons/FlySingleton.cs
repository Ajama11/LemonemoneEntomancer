using BaseLib.Abstracts;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;

public class FlySingleton() : CustomSingletonModel(HookType.Combat)
{
    public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
    {
        if (card.Keywords.Contains(MyEnums.Fly))
        {
            var target = card.Rng.CombatTargets.NextItem(card.AllEnemies);
            if (target == null) return;

            await CardPileCmd.Add(card, PileType.Play);
            
            await Cmd.CustomScaledWait(0.3f, 0.6f);

            await PowerCmd.Apply<BuzzedPower>(choiceContext,
                target, 3,
                card.Owner.Creature, null);
            
            await CardPileCmd.Add(card, PileType.Discard);
        }

        await MyHooks.AfterCardDiscardedAfterFlyCheck(card.Owner, card);
    }
}