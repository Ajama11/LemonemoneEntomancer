using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class ChildrenPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player || AmountOnTurnStart == 0) return;

        await PowerCmd.Apply<BuzzedPower>(choiceContext,
            this.AllEnemies, Amount,
            Owner, null);
        
        await PowerCmd.Remove(this);
    }
}