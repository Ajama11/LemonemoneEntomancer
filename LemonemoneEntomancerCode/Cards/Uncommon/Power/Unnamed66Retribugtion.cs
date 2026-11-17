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

public class Unnamed66Retribugtion() : LemonemoneEntomancerCard(
    1, CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    private const string ExtraBuzzed = "ExtraBuzzed";
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<Unnamed66RetribugtionPower>(1),
        new (ExtraBuzzed, 2)
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

        await MyActions.ApplySelfWithExtra<Unnamed66RetribugtionPower>(choiceContext, this, power =>
        {
            power.IncreaseExtraBuzzed(DynamicVars[ExtraBuzzed].IntValue);
            return power;
        });
    }
}