using Godot;

public partial class ArenaTimeManager : Node
{
	[Export]
	public PackedScene EndScreenScene { get; set; }

	private Timer _timer;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
	}

	public double GetTimeElapsed()
	{
		return _timer.WaitTime - _timer.TimeLeft;
	}

	public void OnTimerTimeout()
	{
		var endScreenInstance = EndScreenScene.Instantiate();
		AddChild(endScreenInstance);
	}
}
