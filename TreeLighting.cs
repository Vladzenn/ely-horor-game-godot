using Godot;

public partial class TreeLighting : Node2D
{
    [Export]
    public bool ShowDebug = false;

    private Player player;

    private Sprite2D leftTrunk;
    private Sprite2D rightTrunk;

    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player;

        if (player == null)
        {
            GD.PushError("TreeLighting: No node found in Player group.");
            return;
        }

        leftTrunk = GetNode<Sprite2D>("LeftTrunk");
        rightTrunk = GetNode<Sprite2D>("RightTrunk");
    }

    public override void _Process(double delta)
    {

        if (player == null)
            return;

        UpdateLighting();

        if (ShowDebug)
            QueueRedraw();
    }

    private void UpdateLighting()
    {
        Vector2 toPlayer = player.GlobalPosition - GlobalPosition;

        float sum = toPlayer.X + toPlayer.Y;
        float diff = toPlayer.X - toPlayer.Y;

        // RIGHT
        if (sum > 0 && diff > 0)
        {
            SetLightable(leftTrunk, false);
            SetLightable(rightTrunk, true);
        }
        // BELOW
        else if (sum > 0 && diff < 0)
        {
            SetLightable(leftTrunk, true);
            SetLightable(rightTrunk, true);
        }
        // LEFT
        else if (sum < 0 && diff < 0)
        {
            SetLightable(leftTrunk, true);
            SetLightable(rightTrunk, false);
        }
        // ABOVE
        else
        {
            SetLightable(leftTrunk, false);
            SetLightable(rightTrunk, false);
        }
    }

    private void SetLightable(Sprite2D sprite, bool lightable)
    {
        sprite.LightMask = lightable ? 2 : 4;
    }

    public override void _Draw()
    {
        if (!ShowDebug)
            return;

        const float length = 300f;

        // Draw the X
        DrawLine(
            new Vector2(-length, -length),
            new Vector2(length, length),
            Colors.Red,
            2f);

        DrawLine(
            new Vector2(-length, length),
            new Vector2(length, -length),
            Colors.Red,
            2f);

        if (player != null)
        {
            DrawCircle(
                ToLocal(player.GlobalPosition),
                5f,
                Colors.Lime);
        }
    }
}