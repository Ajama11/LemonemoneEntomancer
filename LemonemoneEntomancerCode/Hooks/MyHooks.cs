using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public static class MyHooks
{
    public static Task OnBugsChanged(Player player, int delta, CardModel? card)
    {
        return HookUtils.DispatchWithContext<IOnBugsChanged>(player,
            (listener, context) => listener.OnBugsChanged(player, context, delta, card));
    }
    
    public static Task OnBugAttackHit(Player player, Creature dealer, DamageResult result, ValueProp props,
        Creature target, CardModel? cardSource)
    {
        return HookUtils.DispatchWithContext<IOnBugAttackHit>(player,
            (listener, context) => listener.OnBugAttackHit(context, dealer, result, props, target, cardSource));
    }

    public static Task AfterCardDiscardedAfterFlyCheck(Player player, CardModel card)
    {
        return HookUtils.DispatchWithContext<IAfterCardDiscardedAfterFlyCheck>(player,
            (listener, context) => listener.AfterCardDiscardedAfterFlyCheck(context, card));
    }
}