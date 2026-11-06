using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
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

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Ancient;

public class Unnamed92OrobasUpgrade() : LemonemoneEntomancerCard(
    1, CardType.Attack, CardRarity.Ancient,
    TargetType.AnyEnemy), IOnBugsChanged
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10, DamageProps.card),
        new SwarmVar(1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [

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
                vfx: VfxCmd.dramaticStabPath,
                tmpSfx: TmpSfx.daggerThrow)
            .Execute(choiceContext);
    }
    
    public async Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta, CardModel? card, List<Creature>? targets)
    {
        if (card != this) return;

        await MyActions.CreateSwarm(DynamicVars.Swarm.IntValue * Math.Abs(delta), player, CombatState!);
    }

    protected override void OnUpgrade()
    {

    }
}