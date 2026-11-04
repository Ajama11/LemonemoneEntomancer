using BaseLib.Extensions;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Common.Attack;

public class Unnamed15BugsNextTurn() : LemonemoneEntomancerCard(
    1, CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3, DamageProps.card),
        new PowerVar<BugsNextTurn>(3)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.slashPath,
                tmpSfx: TmpSfx.slashAttack)
            .Execute(choiceContext);

        await CommonActions.ApplySelf<BugsNextTurn>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<BugsNextTurn>().UpgradeValueBy(1);
    }
}