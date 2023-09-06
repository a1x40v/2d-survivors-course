using Godot;

public partial class AbilityUpgrade : Resource
{
    [Export]
    public string Id { get; set; }

    [Export]
    public string Name { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string Desctiption { get; set; }
}
