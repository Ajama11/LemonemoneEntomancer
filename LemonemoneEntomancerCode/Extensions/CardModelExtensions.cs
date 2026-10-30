using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

public static class CardModelExtensions
{
    extension(CardModel card)
    {
        public RunRngSet Rng => card.Owner.RunState.Rng;
    }
}