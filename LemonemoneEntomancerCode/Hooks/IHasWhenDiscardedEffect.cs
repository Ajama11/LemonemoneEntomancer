using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IHasWhenDiscardedEffect
{
    public Task WhenDiscarded(PlayerChoiceContext choiceContext);
    
    public bool ShouldDoWhenDiscarded { get; }
}