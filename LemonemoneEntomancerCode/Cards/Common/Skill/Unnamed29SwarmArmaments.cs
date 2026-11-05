using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Common.Skill;

public class Unnamed29SwarmArmaments() : LemonemoneEntomancerCard(
    0, CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(2, BlockProps.card),
        new DiscardVar(1)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromCard<Swarm>(true)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);

        List<CardModel> swarms = [];

        if (IsUpgraded)
        {
            swarms.AddRange(Owner.Hand.Where(c => c is Swarm).ToList());
        }
        else
        {
            // CardSelectCmd.FromHandForUpgrade doesn't allow for a filter, and I do not want to transpiler it or copy-paste the whole thing just for an unupgraded Common
            var card = await CommonActions.SelectSingleCard(
                this, CardSelectorPrefs.UpgradeSelectionPrompt,
                choiceContext, PileType.Hand,
                c => c.IsUpgradable && c is Swarm);
            
            if (card != null) swarms.Add(card);
        }

        foreach (var swarm in swarms)
        {
            CardCmd.Upgrade(swarm);
            if (IsUpgraded) continue;
            
            // Needed to have the game not automatically include the single upgraded Swarm in the Discard selection and then create a ghost NCard if "2" cards are discarded
            await CardPileCmd.Add(swarm, PileType.Hand);
        }

        await MyActions.DiscardSelection(choiceContext, this);
    }
}