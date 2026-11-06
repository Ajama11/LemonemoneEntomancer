using HarmonyLib;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.HoverTips), MethodType.Getter)]
public static class OffclassHoverTipsPatch
{
    [HarmonyPostfix]
    public static IEnumerable<IHoverTip> Postfix(IEnumerable<IHoverTip> __result, CardModel __instance)
    {
        if (__instance is LemonemoneEntomancerCard) return __result;

        List<IHoverTip> resultToReturn = [..__result];

        if (__instance.Keywords.Contains(MyEnums.Fly) && !resultToReturn.Contains(HoverTipFactory.FromPower<BuzzedPower>()))
        {
            int flyIndex = resultToReturn.IndexOf(HoverTipFactory.FromKeyword(MyEnums.Fly));
            
            resultToReturn.Insert(flyIndex + 1, 
                HoverTipFactory.FromPower<BuzzedPower>());
            
            resultToReturn.Insert(flyIndex + 2, 
                HoverTipFactory.FromCard<Swarm>());
        }

        return resultToReturn;
    }
}