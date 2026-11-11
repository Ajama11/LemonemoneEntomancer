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

public class Unnamed45DoubleDip() : LemonemoneEntomancerCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];
    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromPower<StrengthPower>(),
        new BuzzedBundle()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        foreach (var enemy in this.AllEnemies)
        {
            await PowerCmd.Apply<Unnamed45DoubleDipPower>(choiceContext,
                enemy, enemy.GetPowerAmount<BuzzedPower>(),
                Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}