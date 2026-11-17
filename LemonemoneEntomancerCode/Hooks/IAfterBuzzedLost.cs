using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IAfterBuzzedLost
{
    public Task AfterBuzzedLost(PlayerChoiceContext choiceContext, PowerModel buzzed, decimal amount);
}