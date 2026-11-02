using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class UnnamedBugAttacksApplyBuzzedPower() : LemonemoneEntomancerPower, IOnBugAttackHit
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public async Task OnBugAttackHit(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        if (dealer != Owner) return;
        
        // To be replaced with Buzzed once Buzzed is implemented
        await PowerCmd.Apply<DisintegrationPower>(choiceContext,
            target, Amount,
            dealer, null);
    }
}