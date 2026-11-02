using Godot;
using LemonemoneEntomancer.LemonemoneEntomancerCode.NetActions;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Powers;
using LemonemoneEntomancer.LemonemoneEntomancerCode.Singletons;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Runs;

namespace LemonemoneEntomancer.LemonemoneEntomancerCode.Nodes;

public partial class NBugWidget : Control
{
	private const string Font = LemonemoneEntomancerMegaLabel.MegaLabelFont;

	private MegaLabel? _amountLabel;
	private Button? _noneButton;
	private Button? _allButton;
	private Button? _minusButton;
	private Button? _plusButton;

	private Player? _player;

	public override void _Ready()
	{
		_amountLabel = GetNode<MegaLabel>("%AmountLabel");
		_noneButton = GetNode<Button>("%NoneButton");
		_allButton = GetNode<Button>("%AllButton");
		_minusButton = GetNode<Button>("%MinusButton");
		_plusButton = GetNode<Button>("%PlusButton");

		_noneButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_allButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_minusButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));
		_plusButton.AddThemeFontOverride(ThemeConstants.Label.Font, PreloadManager.Cache.GetAsset<Font>(Font));

		_noneButton.Pressed += OnNoneButtonPressed;
		_allButton.Pressed += OnAllButtonPressed;
		_minusButton.Pressed += OnMinusButtonPressed;
		_plusButton.Pressed += OnPlusButtonPressed;
	}
	
	public void Initialize(Player player)
	{
		_player = player;

		BugsSingleton.LocalBugAmountChanged += UpdateLabel;
		BugsSingleton.LocalBugSlotsChanged += UpdateLabel;
		BugsSingleton.LocalBugsInWaitingChanged += UpdateLabel;

		UpdateLabel();
	}
	
	private void OnNoneButtonPressed()
	{
		if (_player == null) return;
		
		var action = new LoseBugsInWaitingAction(_player, Really.bigNumber);
		RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(action);
	}

	private void OnAllButtonPressed()
	{
		if (_player == null) return;
		
		var action = new GainBugsInWaitingAction(_player, Really.bigNumber);
		RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(action);
	}

	private void OnMinusButtonPressed()
	{
		if (_player == null) return;
		
		var action = new LoseBugsInWaitingAction(_player, 1);
		RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(action);
	}

	private void OnPlusButtonPressed()
	{
		if (_player == null) return;
		
		var action = new GainBugsInWaitingAction(_player, 1);
		RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(action);
	}

	private void UpdateLabel()
	{
		// ReSharper disable once UseNullPropagation
		if (_player == null) return;
		var combatState = _player.PlayerCombatState;
		if (combatState == null) return;

		int currentBugs = BugsSingleton.BugAmount[combatState];
		int maxBugs = BugsSingleton.BugSlots[combatState];
		int bugsInWaiting = _player.Creature.GetPowerAmount<BugsInWaitingPower>();

		string bugsInWaitingString = bugsInWaiting > 0 ? $" (-{bugsInWaiting})" : "";

		// Replace this with a LocString later! The buttons too!
		_amountLabel?.SetTextAutoSize($"{currentBugs}{bugsInWaitingString} / {maxBugs}");
	}
}
