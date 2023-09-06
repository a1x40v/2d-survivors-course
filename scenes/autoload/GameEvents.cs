using Godot;
using Godot.Collections;

public partial class GameEvents : Node
{
	[Signal]
	public delegate void ExpirienceVialCollectedEventHandler(float amount);

	[Signal]
	public delegate void AbilityUpgradeAddedEventHandler(AbilityUpgrade upgrade, Dictionary<string, Dictionary> currentUpgrades);

	public void EmitExpirienceVialCollected(float amount)
	{
		EmitSignal("ExpirienceVialCollected", amount);
	}

	public void EmitAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary> currentUpgrades)
	{
		EmitSignal("AbilityUpgradeAdded", upgrade, currentUpgrades);
	}
}
