using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public interface IOnBugAttackHit
{
    public Task OnBugAttackHit(PlayerChoiceContext choiceContext, Creature dealer,
        DamageResult result, ValueProp props, Creature target, CardModel? cardSource);
}