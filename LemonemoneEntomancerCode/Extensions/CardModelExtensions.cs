using System.Diagnostics.CodeAnalysis;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public static class CardModelExtensions
{
    extension(CardModel card)
    {
        public RunRngSet Rng => card.Owner.RunState.Rng;

        public IReadOnlyList<Creature> AllEnemies => card.CombatState?.HittableEnemies ?? [];
        
        /// DynamicVar names are CalculatedHits, CalculationBase, and CalculationExtra
        public IEnumerable<DynamicVar> MakeCalculatedHitCountTemp(int baseVal, Func<CardModel, Creature?, decimal> bonus, int extra = 1)
        {
            return CustomCardModel.FinishMakeCalculatedVar(new CalculatedHitCountVar(DynamicVarSetCalculatedHitsExtension.Key).WithMultiplier(bonus), baseVal, extra);
        }
    }
}