using HarmonyLib;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Patches;

[HarmonyPatch]
public static class FlyGlowGoldPatch
{
    private static bool _isFromDiscard;
    
    [HarmonyPatch(typeof(CardSelectCmd), nameof(CardSelectCmd.FromHandForDiscard))]
    [HarmonyPrefix]
    static void PrefixDiscard(out bool __state)
    {
        __state = _isFromDiscard;
        _isFromDiscard = true;
    }
    
    [HarmonyPatch(typeof(CardSelectCmd), nameof(CardSelectCmd.FromHandForDiscard))]
    [HarmonyPostfix]
    static void PostfixDiscard(bool __state)
    {
        _isFromDiscard = __state;
    }
    
    [HarmonyPatch(typeof(CardSelectCmd), nameof(CardSelectCmd.FromHand))]
    [HarmonyPrefix]
    static void PrefixFromHand(ref CardSelectorPrefs prefs)
    {
        if (!_isFromDiscard || prefs.ShouldGlowGold is null) return;
        
        var originalGlow = prefs.ShouldGlowGold;
        
        prefs.ShouldGlowGold = c =>
        {
            if (originalGlow(c)) return true;
            return c.Keywords.Contains(MyEnums.Fly);
        };
    }
}