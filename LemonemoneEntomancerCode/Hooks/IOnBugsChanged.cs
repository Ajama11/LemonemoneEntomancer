using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IOnBugsChanged
{
    public Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta);
}