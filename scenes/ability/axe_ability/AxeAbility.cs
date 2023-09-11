using Godot;

public partial class AxeAbility : Node2D
{
    public const int MaxRadius = 100;

    private Vector2 _baseRotation = Vector2.Right;
    private Player _player;

    public HitboxComponent HitboxComponent { get; set; }

    public override void _Ready()
    {
        _baseRotation = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));

        HitboxComponent = GetNode<Area2D>("HitboxComponent") as HitboxComponent;
        _player = GetTree().GetFirstNodeInGroup("player") as Player;

        var tween = CreateTween();
        tween.TweenMethod(Callable.From((float x) => TweenMethod(x)), 0f, 2f, 3);
        tween.TweenCallback(Callable.From(QueueFree));
    }

    public void TweenMethod(float rotations)
    {
        float percent = rotations / 2;
        float currentRadius = percent * MaxRadius;
        Vector2 currentDir = _baseRotation.Rotated(rotations * Mathf.Tau);

        if (_player == null) return;

        GlobalPosition = _player.GlobalPosition + currentDir * currentRadius;
    }
}
