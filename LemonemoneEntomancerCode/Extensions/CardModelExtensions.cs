using System.Diagnostics.CodeAnalysis;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public static class CardModelExtensions
{
    extension(CardModel card)
    {
        public RunRngSet Rng => card.Owner.RunState.Rng;

        public IReadOnlyList<Creature> AllEnemies => card.CombatState?.HittableEnemies ?? [];
    }
}