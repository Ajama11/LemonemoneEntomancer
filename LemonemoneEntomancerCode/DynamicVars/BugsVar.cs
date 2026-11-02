using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;

public class BugsVar(decimal baseValue, bool skipTooltip = false) : DynamicVar(Key, baseValue)
{
    public const string Key = "Bugs";
    public bool SkipTooltip = skipTooltip;
}

public static class BugVarExtension
{
    extension(DynamicVarSet dynamicVars)
    {
        public BugsVar Bugs => (BugsVar) dynamicVars[BugsVar.Key];
    }
}