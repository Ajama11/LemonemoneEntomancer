using BaseLib.Extensions;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
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

public class BugBite() : LemonemoneEntomancerCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(14, DamageProps.card),
        new PowerVar<BugBitePower>(2)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        MyEnums.Fly,
        CardKeyword.Exhaust
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        new BugStrengthBundle()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.bitePath,
                tmpSfx: TmpSfx.bluntAttack)
            .Execute(choiceContext);

        await CommonActions.ApplySelf<BugBitePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<BugBitePower>().UpgradeValueBy(1);
    }
}