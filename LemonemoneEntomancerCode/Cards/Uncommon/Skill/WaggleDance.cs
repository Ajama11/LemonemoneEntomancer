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

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Uncommon.Skill;

public class WaggleDance() : LemonemoneEntomancerCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new SwarmVar(1)
    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.Static(MyEnums.Bugs)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {


        if (IsUpgraded || Owner.Bugs < 4) await MyActions.CreateSwarm(this);
    }
}