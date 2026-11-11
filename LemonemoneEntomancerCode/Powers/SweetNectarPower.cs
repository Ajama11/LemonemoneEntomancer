using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class SweetNectarPower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..new BugStrengthBundle().HoverTips
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player || AmountOnTurnStart == 0) return;

        await PowerCmd.Apply<SweetestNectarPower>(choiceContext,
            Owner, Amount,
            Owner, null);
        
        await PowerCmd.Remove(this);
    }
}