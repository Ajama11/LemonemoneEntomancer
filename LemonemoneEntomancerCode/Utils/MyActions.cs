using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;

public class MyActions
{
    #region Core

    public static async Task<IEnumerable<CardModel>> CreateCards(
        CardModel canonicalCard, int amount,
        CardModel sourceCard,
        PileType pile = PileType.Hand,
        CardPilePosition position = CardPilePosition.Bottom,
        bool preview = true, float previewTime = 1.2f,
        Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        return await CreateCards(canonicalCard, amount,
            sourceCard.Owner, sourceCard.CombatState!,
            pile, position, preview, previewTime, modifyCardsBeforePreview);
    }
    
    public static async Task<IEnumerable<CardModel>> CreateCards(
        CardModel canonicalCard, int amount,
        Player owner, ICombatState combatState,
        PileType pile = PileType.Hand,
        CardPilePosition position = CardPilePosition.Bottom,
        bool preview = true, float previewTime = 1.2f,
        Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        if (amount == 0 || CombatManager.Instance.IsOverOrEnding) return [];

        List<CardModel> cards = [];

        for (int i = 0; i < amount; i++)
        {
            cards.Add(combatState.CreateCard(canonicalCard, owner));
        }

        if (modifyCardsBeforePreview != null) cards = modifyCardsBeforePreview(cards);

        IReadOnlyList<CardPileAddResult> results =
            await CardPileCmd.AddGeneratedCardsToCombat(cards, pile, owner, position);

        if (pile != PileType.Hand && preview)
            CardCmd.PreviewCardPileAdd(results, previewTime);

        return cards;
    }
    
    public static List<CardModel> GetRandomCards(CardModel sourceCard,
        PileType from, Func<CardModel, bool> filter, int amount = 1)
    {
        return GetRandomCards(sourceCard.Owner, from, filter, amount);
    }
    
    public static List<CardModel> GetRandomCards(Player player,
        PileType from, Func<CardModel, bool> filter, int amount = 1)
    {
        List<CardModel> cards = from.GetPile(player).Cards
            .Where(filter)
            .TakeRandom(amount, player.RunState.Rng.CombatCardSelection)
            .ToList();
        
        return cards;
    }
    
    public static List<CardModel> GetRandomCards(Player player,
        List<CardModel> cards, Func<CardModel, bool> filter, int amount = 1)
    {
        List<CardModel> selectedCards = cards
            .Where(filter)
            .TakeRandom(amount, player.RunState.Rng.CombatCardSelection)
            .ToList();
        
        return selectedCards;
    }
    
    #endregion

    public static async Task GainBugs(CardModel sourceCard)
    {
        await GainBugs(sourceCard.Owner, sourceCard.DynamicVars.Bugs.IntValue);
    }
    
    public static async Task GainBugs(Player player, int amount)
    {
        await BugsSingleton.GainBugs(player, amount);
    }

    public static async Task CreateSwarm(
        CardModel sourceCard,
        PileType pile = PileType.Draw, CardPilePosition? position = null,
        bool preview = true, float previewTime = 1.2f,
        Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        var positionToUse =
            position ?? 
            (pile == PileType.Draw ?
                CardPilePosition.Random :
                CardPilePosition.Bottom);

        await CreateCards(ModelDb.Card<Swarm>(),
            sourceCard.DynamicVars.Swarm.IntValue, sourceCard,
            pile, positionToUse, preview, previewTime, modifyCardsBeforePreview);
    }
    
    public static async Task CreateSwarm(
        int amount, Player owner, ICombatState combatState,
        PileType pile = PileType.Draw, CardPilePosition? position = null,
        bool preview = true, float previewTime = 1.2f,
        Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        var positionToUse =
            position ?? 
            (pile == PileType.Draw ?
                CardPilePosition.Random :
                CardPilePosition.Bottom);
        
        await CreateCards(ModelDb.Card<Swarm>(),
            amount, owner, combatState,
            pile, positionToUse, preview, previewTime, modifyCardsBeforePreview);
    }
}