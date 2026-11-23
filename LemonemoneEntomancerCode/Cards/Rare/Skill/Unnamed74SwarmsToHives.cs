using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Token;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Skill;

public class Unnamed74SwarmsToHives() : LemonemoneEntomancerCard(
    0, CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [

    ];

    public override BundledHoverTipManager MyBundles =>
    [
        BundledHoverTipFactory.FromCard<Swarm>(),
        BundledHoverTipFactory.FromCard<Hive>(IsUpgraded)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        foreach (var swarm in Owner.Hand.OfType<Swarm>().ToList())
        {
            var result = await CardCmd.TransformTo<Hive>(swarm);
            if (IsUpgraded && result != null) CardCmd.Upgrade(result.Value.cardAdded);
        }
    }
}