using BaseLib.Extensions;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;

public class BundledHoverTipManager : List<BundledHoverTip>
{
    public enum Category
    {
        Start = 0,
        Middle = 100,
        End = 1000
    }

    public List<IHoverTip> GetHoverTips()
    {
        List<IHoverTip> list = [];
        
        foreach (var bundle in this)
        {
            list = [..list, ..bundle.HoverTips];
        }

        return list;
    }

    private string DebugDump()
    {
        string bundleNames = "";
        
        foreach (var bundle in this)
        {
            if (bundleNames.Length > 0) bundleNames += "\n";
            bundleNames += $"{bundle.SortOrder}: {bundle.Name}";
        }

        return bundleNames;
    }

    private void Reorder(string anchor, params string[] otherBundles)
    {
        var matchingAnchor = Find(
            b => b.Name == anchor
        );

        if (matchingAnchor == null)
        {
            MainFile.Logger.Error($"BundledHoverTipManager.Reorder() could not find anchor {anchor}!\nCurrent bundles:\n{DebugDump()}");
            return;
        }

        for (int i = 0; i < otherBundles.Length; i++)
        {
            var matchingBundle = Find(
                b => b.Name == otherBundles[i]
            );
            
            if (matchingBundle == null)
            {
                MainFile.Logger.Error($"BundledHoverTipManager.Reorder() could not find {otherBundles[i]}!\nCurrent bundles:\n{DebugDump()}");
            }

            matchingBundle?.SortOrder = matchingAnchor.SortOrder + i + 1;
        }
    }

    public void SortHoverTips(List<List<string>>? reorders = null)
    {
        if (reorders != null)
        {
            foreach (var reorder in reorders)
            {
                Reorder(reorder[0], reorder.Skip(1).ToArray());
            }
        }
        
        Sort((x, y) => x.InternalSortOrder - y.InternalSortOrder);
    }
}