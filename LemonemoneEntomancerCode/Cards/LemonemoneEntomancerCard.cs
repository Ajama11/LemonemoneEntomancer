using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips;
using LemonemoneEntomancer.LemonemoneEntomancerCode.BundledHoverTips.Core;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Status;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Character;
using LemonemoneEntomancer.LemonemoneEntomancerCode.DynamicVars;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Cards;

/// <summary>
/// This is the base class for your mod's cards, which is set up to load the card's images from your mod's resources.
/// When creating a card, right click the Cards folder and create a new file with the Custom Card template.
/// This will generate a class that extends this one.
/// You can also just create the class manually; just make sure to inherit from this class.
/// </summary>
[Pool(typeof(EntomancerCardPool))]
public abstract class LemonemoneEntomancerCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    
    public virtual BundledHoverTipManager MyBundles => [];
    public virtual List<List<string>> BundleReorders => [];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            BundledHoverTipManager bundles = [];

            #region Automatic Bundles
            
            foreach (var keyword in ((IEnumerable<CardKeyword>) CardKeywordOrder.beforeDescription).Reverse())
            {
                if (Keywords.Contains(keyword))
                {
                    bundles.Add(new BundledHoverTip(
                        keyword.GetTitle().GetRawText(),
                        HoverTipFactory.FromKeyword(keyword),
                        BundledHoverTipManager.Category.Start
                    ));

                    if (keyword == CardKeyword.Ethereal && !Keywords.Contains(CardKeyword.Exhaust))
                    {
                        bundles.Add(new BundledHoverTip(
                            CardKeyword.Exhaust.GetTitle().GetRawText(),
                            HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
                            BundledHoverTipManager.Category.Start
                        ));
                    }
                }
            }
            
            foreach (var keyword in CardKeywordOrder.afterDescription)
            {
                if (!Keywords.Contains(keyword)) continue;
                
                if (keyword == MyEnums.Fly)
                {
                    bundles.Add(new FlyBundle());
                    continue;
                }
                    
                bundles.Add(new BundledHoverTip(
                    keyword.GetTitle().GetRawText(),
                    HoverTipFactory.FromKeyword(keyword),
                    BundledHoverTipManager.Category.End
                ));
            }

            if (GainsBlock)
            {
                bundles.Add(BundledHoverTipFactory.Static(StaticHoverTip.Block));
            }

            foreach (var myPowerVar in DynamicVars.Values.OfType<IPowerHoverTip>())
            {
                var powerHoverTip = myPowerVar.GetPowerHoverTip();
                if (powerHoverTip.SkipTooltip) continue;
                
                bundles.Add(new BundledHoverTip(powerHoverTip.Name, powerHoverTip.HoverTip));
            }
            
            #endregion
            
            if (DynamicVars.Values.Any(dv => dv is SwarmVar { SkipTooltip: false }))
            {
                bool upgraded = DynamicVars.Values.Any(dv => dv is SwarmVar { SkipTooltip: false, Upgraded: true });
                bundles.Add(BundledHoverTipFactory.FromCard<Swarm>(upgraded));
            }
            
            bundles.AddRange(MyBundles);
            
            bundles.SortHoverTips(BundleReorders);
            return bundles.GetHoverTips();
        }
    }
}