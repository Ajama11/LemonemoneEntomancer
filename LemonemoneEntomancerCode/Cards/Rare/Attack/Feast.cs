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

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Attack;

public class Feast() : LemonemoneEntomancerCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy), IOnBugsSpent
{
    private int BugsSpent { get; set; }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(20, DamageProps.card),
        new BugsVar(1)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (IsUpgraded) await MyActions.GainBugs(this);

        BugsSpent = 0;
        
        var attackCommand = await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.bitePath,
                tmpSfx: TmpSfx.heavyAttack)
            .Execute(choiceContext);

        if (attackCommand.Results.SelectMany(list => list).Any(result => result.WasTargetKilled))
        {
            await MyActions.GainBugs(Owner, BugsSpent);
        }
    }
    
    public Task OnBugsSpent(Player player, PlayerChoiceContext choiceContext, int amount, CardModel? card, List<Creature>? targets)
    {
        if (card != this) return Task.CompletedTask;
        
        BugsSpent = amount;
        
        return Task.CompletedTask;
    }
}