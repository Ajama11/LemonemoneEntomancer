using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.CombatHistoryEntries;

public class BuzzedLostEntry(
    decimal amount,
    Creature actor,
    int roundNumber,
    CombatSide currentSide,
    CombatHistory history,
    IEnumerable<Player> players)
    : CombatHistoryEntry(actor, roundNumber, currentSide, history, players)
{
    public decimal Amount { get; } = amount;

    public override string Description => $"{Actor.LogName} lost {Amount} Buzzed";
}