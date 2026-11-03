using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Extensions;
using Godot;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Cards.Basic;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Character;

public class Entomancer : PlaceholderCharacterModel
{
    public const string CharacterId = "LemonemoneEntomancer";

    public static readonly Color Color = new("B59A00");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<Strike>(),
        ModelDb.Card<Strike>(),
        ModelDb.Card<Strike>(),
        ModelDb.Card<Strike>(),
        ModelDb.Card<Defend>(),
        ModelDb.Card<Defend>(),
        ModelDb.Card<Defend>(),
        ModelDb.Card<Defend>(),
        ModelDb.Card<BeeAssault>(),
        ModelDb.Card<CallTheSwarm>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<TheHive>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<EntomancerCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<EntomancerRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<EntomancerPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}