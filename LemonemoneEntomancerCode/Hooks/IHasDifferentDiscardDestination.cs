using MegaCrit.Sts2.Core.Entities.Cards;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IHasDifferentDiscardDestination
{
    public PileType DiscardDestinationPile { get; set; }
    public CardPilePosition DiscardDestinationPosition { get; set; }
}