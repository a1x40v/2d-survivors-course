using Godot;

public partial class EndScreen : CanvasLayer
{
	private Label _titleLabel;
	private Label _descriptionLabel;

	public override void _Ready()
	{

		_titleLabel = GetNode<Label>("%TitleLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");

		GetTree().Paused = true;
		var restartButton = GetNode<Button>("%RestartButton");
		restartButton.Pressed += OnRestartButtonPressed;
		var quitButton = GetNode<Button>("%QuitButton");
		quitButton.Pressed += OnQuitButtonPressed;
	}

	public void SetDefeat()
	{
		_titleLabel.Text = "Defeat";
		_descriptionLabel.Text = "You lost";
	}

	public void OnRestartButtonPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://scenes/main/Main.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
