using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;

public class SwarmVar(decimal baseValue, bool skipTooltip = false) : DynamicVar(Key, baseValue)
{
    public const string Key = "Swarm";
    public bool SkipTooltip = skipTooltip;
}

public static class SwarmVarExtension
{
    extension(DynamicVarSet dynamicVars)
    {
        public SwarmVar Swarm => (SwarmVar) dynamicVars[SwarmVar.Key];
    }
}