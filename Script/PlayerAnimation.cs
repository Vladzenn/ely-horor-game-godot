using Godot;

public partial class PlayerAnimation : AnimatedSprite2D
{
    public override void _Process(double delta)
    {
        Vector2 input = Input.GetVector(
            "move_left",
            "move_right",
            "move_up",
            "move_down"
        );

        if (input == Vector2.Zero)
            Play("Idle");
        else
            Play("Walk");

        FlipH = GetGlobalMousePosition().X < GlobalPosition.X;
    }
}