using HarmonyLib;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Token;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Patches;

[HarmonyPatch]
public static class DiscardFromHandGlowGoldPatch
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
            return c is Swarm { IsUpgraded: true } ||
                   c is Hive { IsUpgraded: true } ||
                   c.Keywords.Contains(MyEnums.Fly);
        };
    }
}