using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Players;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Nodes;

public partial class NBugWidget : Control
{
	private const string Font = LemonemoneEntomancerMegaLabel.MegaLabelFont;

	private Label? _amountLabel;
	private Button? _noneButton;
	private Button? _allButton;
	private Button? _minusButton;
	private Button? _plusButton;

	private Player? _player;

	public override void _Ready()
	{
		_amountLabel = GetNode<Label>("%AmountLabel");
		_noneButton = GetNode<Button>("%NoneButton");
		_allButton = GetNode<Button>("%AllButton");
		_minusButton = GetNode<Button>("%MinusButton");
		_plusButton = GetNode<Button>("%PlusButton");

		_noneButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_allButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_minusButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_plusButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
	}

	public void Initialize(Player player)
	{
		_player = player;
	}
}
