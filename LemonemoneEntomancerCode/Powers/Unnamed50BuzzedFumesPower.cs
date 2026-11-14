using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;

public class Unnamed50BuzzedFumesPower()
    : LemonemoneEntomancerPower
{
    public override PowerType Type =>
        PowerType.Debuff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override PowerInstanceType InstanceType =>
        PowerInstanceType.InstancedPerApplier;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..new BuzzedBundle().HoverTips
    ];

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner)) return;

        await PowerCmd.Apply<BuzzedPower>(new ThrowingPlayerChoiceContext(),
            Owner, Amount,
            Applier, null);
    }
}