using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;

public class BuzzedVar(string name, decimal baseValue, bool skipTooltip = false) : MyPowerVar<BuzzedPower>(name, baseValue, skipTooltip)
{
    public BuzzedVar(decimal baseValue, bool skipTooltip = false) : this(nameof(BuzzedPower), baseValue, skipTooltip) { }
}

public static class BuzzedVarExtension
{
    extension(DynamicVarSet dynamicVars)
    {
        public BuzzedVar Buzzed => (BuzzedVar) dynamicVars[nameof(BuzzedPower)];
    }
}