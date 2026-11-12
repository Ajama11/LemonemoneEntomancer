using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public abstract class Unnamed60CallHomeBasePower() : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    private const string ThresholdKey = "Threshold";
    protected virtual int ThresholdAmount => 3;

    private const string BaseLocKey = "LEMONEMONEENTOMANCER-UNNAMED60_CALL_HOME_BASE_POWER";
    protected override string SmartDescriptionLocKey => $"{BaseLocKey}.smartDescription";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new (ThresholdKey, ThresholdAmount)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(MyEnums.Bugs),
        ..new BuzzedBundle().HoverTips
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (Owner.Player != player) return;

        foreach (var enemy in this.AllEnemies)
        {
            if (enemy.GetPowerAmount<BuzzedPower>() < DynamicVars[ThresholdKey].BaseValue) continue;
            
            Flash();

            await MyActions.GainBugs(player, Amount);
        }
    }
}