using Godot;

public partial class HealthComponent : Node
{
	[Export]
	public float MaxHealth { get; set; } = 10;

	[Signal]
	public delegate void DiedEventHandler();

	[Signal]
	public delegate void HealthChangedEventHandler();

	public float CurrentHealth { get; set; }

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	private void CheckDeath()
	{
		if (CurrentHealth == 0)
		{
			EmitSignal("Died");
			Owner.QueueFree();
		}
	}

	public void Damage(float damage)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
		EmitSignal("HealthChanged");
		CallDeferred(nameof(CheckDeath));
	}

	public float GetHealthPercent()
	{
		if (MaxHealth <= 0) return 0;
		return Mathf.Min(CurrentHealth / MaxHealth, 1);
	}
}
