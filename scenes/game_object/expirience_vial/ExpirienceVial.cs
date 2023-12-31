using Godot;

public partial class ExpirienceVial : Node2D
{
	public override void _Ready()
	{
		var area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.EmitExpirienceVialCollected(1);
		QueueFree();
	}
}
