using Godot;
using Godot.Collections;

public partial class UpgradeManager : Node
{
	[Export]
	public Array<AbilityUpgrade> UpgradePool { get; set; }

	[Export]
	public Node ExperienceManager { get; set; }

	public Dictionary<string, Dictionary> CurrentUpgrades { get; set; } = new Dictionary<string, Dictionary>();

	public override void _Ready()
	{
		ExperienceManager.Connect("LevelUp", new Callable(this, nameof(OnLevelUp)));
	}

	public void OnLevelUp(int currentLevel)
	{
		var chosenUpgrade = UpgradePool.PickRandom();
		if (chosenUpgrade == null) return;

		var hasUpgrade = CurrentUpgrades.ContainsKey(chosenUpgrade.Id);
		if (!hasUpgrade)
		{
			var upgrade = new Dictionary
			{
				{ "resource", chosenUpgrade },
				{ "quantity", 1 }
			};
			CurrentUpgrades.Add(chosenUpgrade.Id, upgrade);
		}
		else
		{
			CurrentUpgrades[chosenUpgrade.Id]["quantity"] = (int)CurrentUpgrades[chosenUpgrade.Id]["quantity"] + 1;
		}

		GD.Print(CurrentUpgrades);
	}
}
