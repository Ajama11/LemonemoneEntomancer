using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IAfterCardDiscardedAfterFlyCheck
{
    public Task AfterCardDiscardedAfterFlyCheck(PlayerChoiceContext choiceContext, CardModel card);
}