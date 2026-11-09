using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Attack;

public class Unnamed35AddFly() : LemonemoneEntomancerCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(8, DamageProps.card),
        new DiscardVar(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.flyingSlashPath,
                tmpSfx: TmpSfx.slashAttack)
            .Execute(choiceContext);

        var card = await CommonActions.SelectSingleCard(this, SelectionScreenPrompt,
            choiceContext, PileType.Hand,
            card => !card.Keywords.Contains(MyEnums.Fly));
        
        card?.AddKeyword(MyEnums.Fly);

        if (IsUpgraded)
        {
            // Sorry Hermit and Fugue, but this has to happen to stop the double-screen-creates-a-ghost-NCard bug
            if (card != null) await CardPileCmd.Add(card, PileType.Hand);
            
            await MyActions.DiscardSelection(choiceContext, this, MyEnums.DiscardSelectionType.UpTo);
        }
    }
}