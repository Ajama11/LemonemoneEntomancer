using MegaCrit.Sts2.Core.Entities.Cards;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IHasDifferentFlyDestination
{
    public PileType FlyDestination { get; set; }
}