using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;

public class BugsVar(string name, decimal baseValue, bool skipTooltip = false) : DynamicVar(name, baseValue)
{
    public const string Key = "Bugs";
    public bool SkipTooltip = skipTooltip;

    public BugsVar(decimal baseValue, bool skipTooltip = false) : this(Key, baseValue, skipTooltip)
    {
        SkipTooltip = skipTooltip;
    }
}

public static class BugsVarExtension
{
    extension(DynamicVarSet dynamicVars)
    {
        public BugsVar Bugs => (BugsVar) dynamicVars[BugsVar.Key];
    }
}