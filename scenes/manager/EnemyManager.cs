using Godot;

public partial class EnemyManager : Node
{
	public const int SpawnRadius = 350;

	[Export]
	public PackedScene BasicEnemyScene { get; set; }

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
	}

	private void OnTimerTimeout()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		Vector2 randomDir = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));
		Vector2 spawnPosition = player.GlobalPosition + randomDir * SpawnRadius;

		var enemy = BasicEnemyScene.Instantiate() as Node2D;

		var entitiesLayer = GetTree().GetFirstNodeInGroup("entities_layer");
		entitiesLayer.AddChild(enemy);
		enemy.GlobalPosition = spawnPosition;
	}
}
