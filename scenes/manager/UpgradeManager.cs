using Godot;
using Godot.Collections;

public partial class UpgradeManager : Node
{
	[Export]
	public Array<AbilityUpgrade> UpgradePool { get; set; }

	[Export]
	public Node ExperienceManager { get; set; }

	[Export]
	public PackedScene UpgradeScreenScene { get; set; }

	public Dictionary<string, Dictionary> CurrentUpgrades { get; set; } = new Dictionary<string, Dictionary>();

	public override void _Ready()
	{
		ExperienceManager.Connect("LevelUp", new Callable(this, nameof(OnLevelUp)));
	}

	public void OnLevelUp(int currentLevel)
	{
		var chosenUpgrade = UpgradePool.PickRandom();
		if (chosenUpgrade == null) return;

		var upgradeScreenInstance = UpgradeScreenScene.Instantiate() as UpgradeScreen;
		AddChild(upgradeScreenInstance);
		upgradeScreenInstance.SetAbilityUpgrades(new Array<AbilityUpgrade> { chosenUpgrade });
		upgradeScreenInstance.Connect("UpgradeSelected", new Callable(this, nameof(OnUpgradeSelected)));
	}

	public void ApplyUpgrade(AbilityUpgrade upgrade)
	{
		var hasUpgrade = CurrentUpgrades.ContainsKey(upgrade.Id);
		if (!hasUpgrade)
		{
			var dic = new Dictionary
			{
				{ "resource", upgrade },
				{ "quantity", 1 }
			};
			CurrentUpgrades.Add(upgrade.Id, dic);
		}
		else
		{
			CurrentUpgrades[upgrade.Id]["quantity"] = (int)CurrentUpgrades[upgrade.Id]["quantity"] + 1;
		}

		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.EmitAbilityUpgradeAdded(upgrade, CurrentUpgrades);

		GD.Print(CurrentUpgrades);
	}

	public void OnUpgradeSelected(AbilityUpgrade upgrade)
	{
		ApplyUpgrade(upgrade);
	}
}
