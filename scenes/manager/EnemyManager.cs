using Godot;

public partial class EnemyManager : Node
{
	public const int SpawnRadius = 350;

	[Export]
	public PackedScene BasicEnemyScene { get; set; }

	[Export]
	public ArenaTimeManager ArenaTimeManager { get; set; }

	private double _baseSpawnTime;
	private Timer _timer;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_baseSpawnTime = _timer.WaitTime;
		_timer.Timeout += OnTimerTimeout;
		ArenaTimeManager.Connect("ArenaDifficultyIncreased", new Callable(this, nameof(OnArenaDifficultyIncreased)));
	}

	private void OnTimerTimeout()
	{
		_timer.Start();

		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		Vector2 randomDir = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));
		Vector2 spawnPosition = player.GlobalPosition + randomDir * SpawnRadius;

		var enemy = BasicEnemyScene.Instantiate() as Node2D;

		var entitiesLayer = GetTree().GetFirstNodeInGroup("entities_layer");
		entitiesLayer.AddChild(enemy);
		enemy.GlobalPosition = spawnPosition;
	}

	public void OnArenaDifficultyIncreased(int difficulty)
	{
		double timeOff = .1 / 12 * difficulty;
		timeOff = Mathf.Min(timeOff, 0.7);
		_timer.WaitTime = _baseSpawnTime - timeOff;
	}
}
