using Godot;
using Godot.Collections;

public partial class UpgradeScreen : CanvasLayer
{
	[Signal]
	public delegate void UpgradeSelectedEventHandler(AbilityUpgrade upgrade);

	[Export]
	public PackedScene UpgradeCardScene { get; set; }

	private HBoxContainer _cardContainer;

	public override void _Ready()
	{
		_cardContainer = GetNode<HBoxContainer>("%CardContainer");
		GetTree().Paused = true;
	}

	public void SetAbilityUpgrades(Array<AbilityUpgrade> upgrades)
	{
		foreach (var upgrade in upgrades)
		{
			var cardInstance = UpgradeCardScene.Instantiate() as AbilityUpgradeCard;
			_cardContainer.AddChild(cardInstance);
			cardInstance.SetAbilityUpgrade(upgrade);
			cardInstance.Connect("AbilityCardSelected", Callable.From(() => { OnAbilityCardSelected(upgrade); }));
		}
	}

	public void OnAbilityCardSelected(AbilityUpgrade upgrade)
	{
		EmitSignal("UpgradeSelected", upgrade);
		GetTree().Paused = false;
		QueueFree();
	}
}
