using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Attack;

public class Unnamed38FlakGamble() : LemonemoneEntomancerCard(
    1, CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    private const string CalculatedHits = "CalculatedHits";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(4, DamageProps.card),
        ..MakeCalculatedVar(CalculatedHits, 0, static (card, _) => card.Owner.Hand.Count(c => c.Type == CardType.Status))
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var times = (int) DynamicVars[CalculatedHits].Calculate(null);

        await CommonActions.CardAttack(this, play,
                hitCount: times,
                vfx: VfxCmd.bluntPath,
                tmpSfx: TmpSfx.bluntAttack)
            .Execute(choiceContext);

        IEnumerable<CardModel> statuses = Owner.Hand
            .Where(card => card.Type == CardType.Status);

        await CardCmd.Discard(choiceContext, statuses);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}