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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Skill;

public class Unnamed48InfiniteWeak() : LemonemoneEntomancerCard(
    1, CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new MyPowerVar<WeakPower>(1),
        new CardsVar(1)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var cards = await MyActions.DiscardSelection(choiceContext, this, MyEnums.DiscardSelectionType.Any);
        
        await PowerCmd.Apply<WeakPower>(choiceContext,
            play.Target!, DynamicVars.Weak.BaseValue * cards.Count,
            Owner.Creature, this);

        if (IsUpgraded) await MyActions.Draw(choiceContext, this);
    }
}