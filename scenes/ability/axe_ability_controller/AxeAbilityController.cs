using Godot;

public partial class AxeAbilityController : Node
{
    [Export]
    public PackedScene AxeScene { get; set; }

    private float _damage = 10;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += OnTimerTimeout;
    }

    public void OnTimerTimeout()
    {
        var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
        if (player == null) return;

        var foreground = GetTree().GetFirstNodeInGroup("foreground_layer") as Node2D;
        if (foreground == null) return;

        var axeInstance = AxeScene.Instantiate() as AxeAbility;
        foreground.AddChild(axeInstance);
        axeInstance.GlobalPosition = player.GlobalPosition;
        axeInstance.HitboxComponent.Damage = _damage;
    }
}
