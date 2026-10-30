using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;

public static class MyHooks
{
    public static Task OnBugsChanged(Player player, int delta)
    {
        return HookUtils.DispatchWithContext<IOnBugsChanged>(player,
            (listener, context) => listener.OnBugsChanged(player, context, delta));
    }
}