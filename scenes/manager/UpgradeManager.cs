using System.Linq;
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

    private Array<AbilityUpgrade> pickUpgrades()
    {
        var chosenUpgrades = new Array<AbilityUpgrade> { };
        var filteredUpgrades = UpgradePool.Duplicate();
        for (int i = 0; i < 2; i++)
        {
            if (filteredUpgrades.Count == 0) break;
            var chosenUpgrade = filteredUpgrades.PickRandom();
            chosenUpgrades.Add(chosenUpgrade);
            filteredUpgrades = new Array<AbilityUpgrade>(filteredUpgrades.Where(x => x.Id != chosenUpgrade.Id));
        }

        return chosenUpgrades;
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

        if (upgrade.MaxQuantity > 0)
        {
            var currentQuantity = (int)CurrentUpgrades[upgrade.Id]["quantity"];
            if (currentQuantity == upgrade.MaxQuantity)
            {
                UpgradePool = new Array<AbilityUpgrade>(UpgradePool.Where(x => x.Id != upgrade.Id));
            }
        }

        var gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.EmitAbilityUpgradeAdded(upgrade, CurrentUpgrades);

        GD.Print(CurrentUpgrades);
    }

    public void OnLevelUp(int currentLevel)
    {
        var upgradeScreenInstance = UpgradeScreenScene.Instantiate() as UpgradeScreen;
        AddChild(upgradeScreenInstance);
        var chosenUpgrades = pickUpgrades();
        upgradeScreenInstance.SetAbilityUpgrades(chosenUpgrades);
        upgradeScreenInstance.Connect("UpgradeSelected", new Callable(this, nameof(OnUpgradeSelected)));
    }

    public void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        ApplyUpgrade(upgrade);
    }
}
