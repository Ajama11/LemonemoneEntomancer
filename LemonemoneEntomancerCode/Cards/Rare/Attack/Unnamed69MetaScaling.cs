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
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Rare.Attack;

public class Unnamed69MetaScaling() : LemonemoneEntomancerCard(
    1, CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    private const string Increase = "Increase";
    private const int BaseBugs = 1;

    [SavedProperty]
    public int CurrentBugs
    {
        get;
        set
        {
            AssertMutable();
            field = value;
            DynamicVars.Bugs.BaseValue = field;
        }
    } = BaseBugs;

    [SavedProperty]
    public int IncreasedBugs
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
        new DamageVar(10, DamageProps.card),
        new BugsVar(CurrentBugs),
        new (Increase, 1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play,
                vfx: VfxCmd.slashPath,
                tmpSfx: TmpSfx.slashAttack)
            .Execute(choiceContext);

        await MyActions.GainBugs(this);
        
        BuffFromPlay(DynamicVars[Increase].IntValue);
        (DeckVersion as Unnamed69MetaScaling)?.BuffFromPlay(DynamicVars[Increase].IntValue);
    }

    public void BuffFromPlay(int extraBugs)
    {
        IncreasedBugs += extraBugs;
        UpdateBugs();
    }

    public void UpdateBugs()
    {
        CurrentBugs = BaseBugs + IncreasedBugs;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}