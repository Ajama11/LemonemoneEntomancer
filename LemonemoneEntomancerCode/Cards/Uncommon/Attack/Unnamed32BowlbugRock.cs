using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Attack;

public class Unnamed32BowlbugRock() : LemonemoneEntomancerCard(
    2, CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy), IOnBugsChanged
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(16, DamageProps.card),
        new BuzzedVar(1)
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
                vfx: VfxCmd.rockShatterPath,
                tmpSfx: TmpSfx.bluntAttack)
            .Execute(choiceContext);
    }
    
    public async Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta, CardModel? card, List<Creature>? targets)
    {
        if (card != this) return;
    
        await PowerCmd.Apply<BuzzedPower>(choiceContext,
            targets, Math.Abs(delta) * DynamicVars.Buzzed.BaseValue,
            Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}