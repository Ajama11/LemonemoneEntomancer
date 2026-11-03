using System.Diagnostics.CodeAnalysis;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public static class DynamicVarExtensions
{
    extension(DynamicVar dynamicVar)
    {
        public bool BoolVal
        {
            get => dynamicVar is BoolVar { BoolVal: true };
            set
            {
                if (dynamicVar is BoolVar boolVar)
                {
                    boolVar.BoolVal = value;
                }
            }
        }

        public decimal Calculate(Creature? target) =>
            dynamicVar is CalculatedVar calculatedVar ?
                calculatedVar.Calculate(target) : 0;
    }
}