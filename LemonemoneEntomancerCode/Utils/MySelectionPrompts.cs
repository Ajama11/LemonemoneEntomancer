using MegaCrit.Sts2.Core.Localization;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;

public static class MySelectionPrompts
{
    private static readonly string UpperModId = MainFile.ModId.ToUpperInvariant();

    public static LocString DiscardUpTo => new("card_selection", $"{UpperModId}-DISCARD_UP_TO");
    
    public static LocString DiscardAny => new("card_selection", $"{UpperModId}-DISCARD_ANY");
}