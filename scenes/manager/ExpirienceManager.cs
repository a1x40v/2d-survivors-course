using Godot;

public partial class ExpirienceManager : Node
{
	public const float TargetExperienceGrow = 5;

	[Signal]
	public delegate void ExpirienceUpdatedEventHandler(float currentExp, float targetExp);

	[Signal]
	public delegate void LevelUpEventHandler(int newLevel);

	private float _currentExp;
	private float _targetExperience = 5;
	private int _currentLevel = 1;

	public override void _Ready()
	{
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("ExpirienceVialCollected", new Callable(this, nameof(OnExpirienceVialCollected)));
	}

	public void IncrementExpirience(float amount)
	{
		_currentExp = Mathf.Min(_currentExp + amount, _targetExperience);
		EmitSignal("ExpirienceUpdated", _currentExp, _targetExperience);

		if (_currentExp == _targetExperience)
		{
			_currentLevel++;
			_targetExperience += TargetExperienceGrow;
			_currentExp = 0;
			EmitSignal("ExpirienceUpdated", _currentExp, _targetExperience);
			EmitSignal("LevelUp", _currentLevel);
		}
		GD.Print(_currentExp);
	}

	public void OnExpirienceVialCollected(float amount)
	{
		IncrementExpirience(amount);
	}
}
