using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;

public class DiscardVar(string name, decimal baseValue) : DynamicVar(name, baseValue)
{
    public const string Key = "Discard";

    public DiscardVar(decimal baseValue) : this(Key, baseValue) {}
}

public static class DiscardVarExtension
{
    extension(DynamicVarSet dynamicVars)
    {
        public DiscardVar Discard => (DiscardVar) dynamicVars[DiscardVar.Key];
    }
}