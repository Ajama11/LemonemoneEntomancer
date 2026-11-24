using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Skill;

public class Children() : LemonemoneEntomancerCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(3),
        new BugsVar(3),
        new BuzzedVar(3, true)
    ];

    public override bool ReportMissingBundleReorders => false;
    public override List<List<string>> BundleReorders =>
    [
        [
            BugsVar.Key,
            nameof(BuzzedPower)
        ]
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        await CommonActions.ApplySelf<EnergyNextTurnPower>(choiceContext, this, DynamicVars.Energy.BaseValue);

        await CommonActions.ApplySelf<BugsNextTurnPower>(choiceContext, this, DynamicVars.Bugs.BaseValue);

        if (IsUpgraded)
            await CommonActions.ApplySelf<ChildrenPower>(choiceContext, this, DynamicVars.Buzzed.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Buzzed.SkipTooltip = false;
    }
}