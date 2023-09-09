using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene EndScreenScene { get; set; }

	public override void _Ready()
	{
		var player = GetNode<CharacterBody2D>("%Player") as Player;
		player.HealthComponent.Connect("Died", new Callable(this, nameof(OnPlayerDied)));
	}

	public void OnPlayerDied()
	{
		var endScreenInstance = EndScreenScene.Instantiate() as EndScreen;
		AddChild(endScreenInstance);
		endScreenInstance.SetDefeat();
	}
}
