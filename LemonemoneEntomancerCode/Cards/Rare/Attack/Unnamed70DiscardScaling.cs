using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Attack;

public class Unnamed70DiscardScaling() : LemonemoneEntomancerCard(
    1, CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy),
    IHasWhenDiscardedEffect, IHasDifferentDiscardDestination
{
    private const string Increase = "Increase";

    private decimal ExtraDamageFromDiscards
    {
        get;
        set
        {
            AssertMutable();
            field = value;
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(11, DamageProps.card),
        new (Increase, 3)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Retain,
        MyEnums.Fly
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.slashPath,
                tmpSfx: TmpSfx.slashAttack)
            .Execute(choiceContext);
    }
    
    public PileType DiscardDestinationPile { get; set; } = PileType.Hand;
    public CardPilePosition DiscardDestinationPosition { get; set; } = CardPilePosition.Bottom;
    public bool ShouldDoWhenDiscarded => IsUpgraded;
    public Task WhenDiscarded(PlayerChoiceContext choiceContext)
    {
        DynamicVars.Damage.BaseValue += DynamicVars[Increase].BaseValue;
        ExtraDamageFromDiscards += DynamicVars[Increase].BaseValue;
        
        return Task.CompletedTask;
    }

    protected override void AfterDowngraded()
    {
        DynamicVars.Damage.BaseValue += ExtraDamageFromDiscards;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
        DynamicVars[Increase].UpgradeValueBy(1);
    }
}