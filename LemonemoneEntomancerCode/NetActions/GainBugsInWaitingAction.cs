using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.NetActions;

public class GainBugsInWaitingAction(Player player, int amount) : GameAction
{
    private readonly Player _player = player;
    private readonly int _amount = amount;
    
    public override ulong OwnerId => _player.NetId;
    public override GameActionType ActionType => GameActionType.Combat;
    
    public override INetAction ToNetAction() => new GainBugsInWaitingNetAction
    {
        Amount = _amount
    };
    
    public override string ToString() => $"GainBugsInWaitingAction for player {OwnerId} with {_amount} Bugs-in-Waiting";

    protected override Task ExecuteAction()
    {
        return BugsSingleton.GainBugsInWaiting(_player, _amount);
    }
}