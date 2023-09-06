using Godot;

public partial class AbilityUpgradeCard : Panel
{
	[Signal]
	public delegate void AbilityCardSelectedEventHandler();

	private Label _nameLabel;
	private Label _descriptionLabel;

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%NameLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");
		GuiInput += OnGuiInput;
	}

	public void SetAbilityUpgrade(AbilityUpgrade upgrade)
	{
		_nameLabel.Text = upgrade.Name;
		_descriptionLabel.Text = upgrade.Desctiption;
	}

	public void OnGuiInput(InputEvent evt)
	{
		if (Input.IsActionPressed("left_click"))
		{
			EmitSignal("AbilityCardSelected");
		}
	}
}
