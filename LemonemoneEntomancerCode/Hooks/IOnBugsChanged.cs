using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IOnBugsChanged
{
    public Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta, CardModel? card, List<Creature>? targets);
}