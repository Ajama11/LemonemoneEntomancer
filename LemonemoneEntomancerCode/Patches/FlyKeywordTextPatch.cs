using HarmonyLib;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Patches;

[HarmonyPatch(typeof(CardKeywordOrder), MethodType.StaticConstructor)]
public static class FlyKeywordTextPatch
{
    [HarmonyPostfix]
    public static void Postfix(ref CardKeyword[] ___afterDescription)
    {
        ___afterDescription = [MyEnums.Fly, ..___afterDescription];
    }
}