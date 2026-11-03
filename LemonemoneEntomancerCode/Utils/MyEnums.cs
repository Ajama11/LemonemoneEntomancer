using System.Diagnostics.CodeAnalysis;
using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;

[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible")]
public class MyEnums
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Fly;
    
    [CustomEnum]
    public static StaticHoverTip Bugs;
}