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

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Power;

public class Unnamed60CallHome() : LemonemoneEntomancerCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    private const string Power = "Power";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new (Power, 1)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs),
        new BuzzedBundle()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);

        if (IsUpgraded)
        {
            await PowerCmd.Apply<Unnamed60CallHomeUpgradedPower>(choiceContext,
                Owner.Creature, DynamicVars[Power].BaseValue,
                Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<Unnamed60CallHomeUnupgradedPower>(choiceContext,
                Owner.Creature, DynamicVars[Power].BaseValue,
                Owner.Creature, this);
        }
    }
}