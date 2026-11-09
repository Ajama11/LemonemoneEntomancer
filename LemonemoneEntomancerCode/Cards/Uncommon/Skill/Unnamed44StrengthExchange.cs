using BaseLib.Extensions;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Skill;

public class Unnamed44StrengthExchange() : LemonemoneEntomancerCard(
    1, CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<StrengthPower>(1),
        new PowerVar<BugStrengthPower>(2)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromPower<StrengthPower>(),
        new BugStrengthBundle()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        await CommonActions.ApplySelf<StrengthPower>(choiceContext, this, -DynamicVars.Strength.IntValue);

        await CommonActions.ApplySelf<BugStrengthPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<BugStrengthPower>().UpgradeValueBy(1);
    }
}