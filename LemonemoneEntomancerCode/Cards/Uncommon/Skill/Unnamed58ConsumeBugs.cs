using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Hooks;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Skill;

public class Unnamed58ConsumeBugs() : LemonemoneEntomancerCard(
    5, CardType.Skill, CardRarity.Uncommon,
    TargetType.Self), IOnBugsChanged
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(24, BlockProps.card),
        new EnergyVar(1)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);

        await BugsSingleton.LoseBugs(Owner, Really.bigNumber, this);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || IsClone) return Task.CompletedTask;
        
        EnergyCost.AddThisCombat(Owner.Bugs * -DynamicVars.Energy.IntValue);
        
        return Task.CompletedTask;
    }

    public Task OnBugsChanged(Player player, PlayerChoiceContext choiceContext, int delta, CardModel? card, List<Creature>? targets)
    {
        EnergyCost.AddThisCombat(delta * -DynamicVars.Energy.IntValue);
        
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}