using BaseLib.Abstracts;
using BaseLib.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed63BuzzedEnergyPower() : LemonemoneEntomancerPower, IHasSecondAmount
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private const string BuzzedAmount = "BuzzedAmount";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar(BuzzedAmount, 0)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..new BuzzedBundle().HoverTips
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner) return;
        
        Flash();

        await PowerCmd.Apply<BuzzedPower>(choiceContext,
            Owner, DynamicVars[BuzzedAmount].BaseValue,
            Owner, null);
    }

    public override decimal ModifyMaxEnergy(Player player, decimal amount)
    {
        return player != Owner.Player ? amount : amount + Amount;
    }

    public void IncreaseBuzzed(int amount)
    {
        AssertMutable();
        DynamicVars[BuzzedAmount].BaseValue += amount;
        this.InvokeSecondAmountChanged();
    }

    public string GetSecondAmount()
    {
        return DynamicVars[BuzzedAmount].IntValue.ToString();
    }
}