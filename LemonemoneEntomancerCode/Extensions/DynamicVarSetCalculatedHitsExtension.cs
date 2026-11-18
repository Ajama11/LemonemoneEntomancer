using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;

public static class DynamicVarSetCalculatedHitsExtension
{
    public const string Key = "CalculatedHits";
    
    extension(DynamicVarSet dynamicVars)
    {
        public CalculatedHitCountVar CalculatedHits => (CalculatedHitCountVar) dynamicVars[Key];
    }
}