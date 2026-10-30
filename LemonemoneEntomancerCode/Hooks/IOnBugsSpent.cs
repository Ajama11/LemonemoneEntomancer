using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IOnBugsSpent
{
    public Task OnBugsSpent(CardModel card, PlayerChoiceContext choiceContext, int amount);
}