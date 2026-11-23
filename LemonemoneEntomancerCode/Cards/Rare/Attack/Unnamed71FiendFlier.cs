using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Attack;

public class Unnamed71FiendFlier() : LemonemoneEntomancerCard(
    0, CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override bool HasEnergyCostX => true;

    private const string XDamage = "XDamage";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        ..MakeCalculatedDamage(XDamage, 0, static (card, _) =>
        {
            if (card.Owner.PlayerCombatState == null || card.CombatState == null) return 0;

            decimal xValue = Hook.ModifyXValue(card.CombatState, card, card.Owner.PlayerCombatState.Energy);

            if (card.IsUpgraded) xValue++;
            
            return xValue;
        }),
        ..this.MakeCalculatedHitCountTemp(0, static (card, _) =>
            card.Owner.Hand.Count(c => c != card))
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        MyEnums.Fly,
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        // This is where the draw would've gone, if it weren't for the "Lies to the player" problem
        
        await CommonActions.CardAttack(this, play,
                play.Target,
                ((CalculatedDamageVar) DynamicVars[XDamage]),
                (int) DynamicVars.CalculatedHits.Calculate(null),
                VfxCmd.slashPath,
                tmpSfx: TmpSfx.slashAttack)
            .Execute(choiceContext);
    }
}