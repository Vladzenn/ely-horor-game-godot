using Godot;

public partial class Flashlight : PointLight2D
{
    public override void _Process(double delta)
    {
        GetParent<Node2D>()
            .LookAt(GetGlobalMousePosition());
    }
}