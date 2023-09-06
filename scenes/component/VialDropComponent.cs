using Godot;

public partial class VialDropComponent : Node
{
	[Export]
	public PackedScene VialScene { get; set; }

	[Export]
	public Node HealthComponent { get; set; }

	[Export(PropertyHint.Range, "0,1,")]
	public float DropPercent { get; set; } = 0.5f;

	public override void _Ready()
	{
		HealthComponent.Connect("Died", new Callable(this, nameof(OnDied)));
	}

	public void OnDied()
	{
		if (GD.Randf() > DropPercent) return;
		if (VialScene == null) return;
		if (Owner is not Node2D) return;

		var spawnPosition = (Owner as Node2D).GlobalPosition;
		var vialInstance = VialScene.Instantiate() as Node2D;
		var entitiesLayer = GetTree().GetFirstNodeInGroup("entities_layer");
		entitiesLayer.AddChild(vialInstance);
		vialInstance.GlobalPosition = spawnPosition;
	}
}
