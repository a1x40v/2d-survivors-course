using Godot;

public partial class BasicEnemy : CharacterBody2D
{
	public const float Speed = 40.0f;
	private HealthComponent _healthComponent;

	public override void _Ready()
	{
		var area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;

		_healthComponent = GetNode<HealthComponent>("HealthComponent");
	}

	public override void _Process(double delta)
	{
		Vector2 direction = GetDirectionToPlayer();
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private Vector2 GetDirectionToPlayer()
	{
		var playerNode = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (playerNode != null)
			return (playerNode.GlobalPosition - GlobalPosition).Normalized();

		return Vector2.Zero;
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		_healthComponent.Damage(100);
	}
}