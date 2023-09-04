using Godot;

public partial class ExperienceBar : CanvasLayer
{
	[Export]
	public Node ExperienceManager { get; set; }
	private ProgressBar _progressBar;

	public override void _Ready()
	{
		_progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
		ExperienceManager.Connect("ExpirienceUpdated", new Callable(this, nameof(OnExperienceUpdated)));
	}

	public void OnExperienceUpdated(float currentExp, float targetExp)
	{
		float percent = currentExp / targetExp;
		_progressBar.Value = percent;
	}
}
