using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

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
        {
            var previewStyle = amount > 5 ?
                CardPreviewStyle.MessyLayout :
                CardPreviewStyle.HorizontalLayout;
            CardCmd.PreviewCardPileAdd(results, previewTime, previewStyle);
        }

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

    public static async Task GainBugs(CardModel sourceCard, bool playAudio = true)
    {
        await GainBugs(sourceCard.Owner, sourceCard.DynamicVars.Bugs.IntValue, playAudio: playAudio);
    }
    
    public static async Task GainBugs(Player player, int amount, bool playAudio = true)
    {
        await BugsSingleton.GainBugs(player, amount, playAudio: playAudio);
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

        Func<List<CardModel>, List<CardModel>> defaultModifyCards = list =>
        {
            if (sourceCard.DynamicVars.Swarm.Upgraded)
                foreach (var card in list) CardCmd.Upgrade(card);
            return list;
        };

        if (modifyCardsBeforePreview != null)
        {
            var parameterFunc = modifyCardsBeforePreview;
            modifyCardsBeforePreview = list => defaultModifyCards(parameterFunc(list));
        }
        else
        {
            modifyCardsBeforePreview = defaultModifyCards;
        }

        await CreateCards(ModelDb.Card<Swarm>(),
            sourceCard.DynamicVars.Swarm.IntValue, sourceCard,
            pile, positionToUse, preview, previewTime, modifyCardsBeforePreview);
    }
    
    public static async Task CreateSwarm(
        int amount, Player owner, ICombatState combatState,
        PileType pile = PileType.Draw, CardPilePosition? position = null,
        bool preview = true, float previewTime = 1.2f,
        Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null,
        bool upgrade = false)
    {
        var positionToUse =
            position ?? 
            (pile == PileType.Draw ?
                CardPilePosition.Random :
                CardPilePosition.Bottom);
        
        Func<List<CardModel>, List<CardModel>> defaultModifyCards = list =>
        {
            if (upgrade)
                foreach (var card in list) CardCmd.Upgrade(card);
            return list;
        };

        if (modifyCardsBeforePreview != null)
        {
            var parameterFunc = modifyCardsBeforePreview;
            modifyCardsBeforePreview = list => defaultModifyCards(parameterFunc(list));
        }
        else
        {
            modifyCardsBeforePreview = defaultModifyCards;
        }
        
        await CreateCards(ModelDb.Card<Swarm>(),
            amount, owner, combatState,
            pile, positionToUse, preview, previewTime, modifyCardsBeforePreview);
    }

    public static async Task<PowerModel?> Buzzed(PlayerChoiceContext choiceContext, Creature? target, DynamicVarSource dynVarSource)
    {
        if (target == null) return null;
        return await CommonActions.Apply<BuzzedPower>(choiceContext, target, dynVarSource);
    }
    
    public static async Task<IReadOnlyList<BuzzedPower>> Buzzed(PlayerChoiceContext choiceContext, IEnumerable<Creature> targets, DynamicVarSource dynVarSource)
    {
        return await CommonActions.Apply<BuzzedPower>(choiceContext, targets, dynVarSource);
    }

    public static async Task<List<CardModel>> DiscardSelection(PlayerChoiceContext choiceContext, CardModel sourceCard, MyEnums.DiscardSelectionType selectionType = MyEnums.DiscardSelectionType.Forced, int amountOverride = -1)
    {
        int amount =
            amountOverride != -1 ? amountOverride :
            selectionType == MyEnums.DiscardSelectionType.Any ? Really.bigNumber :
            sourceCard.DynamicVars.Discard.IntValue;

        CardSelectorPrefs prefs = selectionType switch
        {
            MyEnums.DiscardSelectionType.Forced =>
                new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt,
                amount),
            MyEnums.DiscardSelectionType.UpTo =>
                new CardSelectorPrefs(MySelectionPrompts.DiscardUpTo, 0, amount),
            MyEnums.DiscardSelectionType.Any =>
                new CardSelectorPrefs(MySelectionPrompts.DiscardAny, 0,
                amount),
            _ => throw new ArgumentOutOfRangeException(nameof(selectionType), selectionType, null)
        };

        List<CardModel> discards =
            (await CardSelectCmd.FromHandForDiscard(choiceContext, sourceCard.Owner, prefs, null, sourceCard))
            .ToList();

        foreach (var card in discards)
        {
            await CardCmd.Discard(choiceContext, card);
        }

        return discards;
    }

    // The reversed order annoys me otherwise...
    public static async Task<IEnumerable<CardModel>> Draw(PlayerChoiceContext context, CardModel card)
    {
        return await CommonActions.Draw(card, context);
    }
}