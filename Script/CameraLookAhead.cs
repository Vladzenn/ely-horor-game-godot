using Godot;
using System;

public partial class CameraLookAhead : Camera2D
{
    [Export] public float LeadDistance = 200.0f;
    [Export] public float SmoothSpeed = 5.0f;
    [Export] public float ReturnSpeed = 4.0f;

    private Player player;

    //DEBUD TYPE SHIT
    [Export] public Boolean SmoothCam;

    public override void _Ready()
    {
        player = GetParent<Player>();
    }


    public override void _Process(double delta)
    {
        if (player == null) return;


        PointLight2D flashlight = player.FlashlightNode;

        bool flashlightActive =
            flashlight != null &&
            flashlight.Visible;


        if (flashlightActive)
        {
            Vector2 mouseDirection =
                (GetGlobalMousePosition() -
                 player.GlobalPosition)
                .Normalized();

            Vector2 targetPosition =
                player.GlobalPosition +
                (mouseDirection * LeadDistance);

            if (SmoothCam)
            {
                GlobalPosition =
                    GlobalPosition.Lerp(
                        targetPosition,
                        SmoothSpeed * (float)delta
                    );
            }
            else
            {
                GlobalPosition = targetPosition;
            }
        }
        else
        {
            if (SmoothCam)
            {
                GlobalPosition =
                    GlobalPosition.Lerp(
                        player.GlobalPosition,
                        ReturnSpeed * (float)delta
                    );
            }
            else
            {
                GlobalPosition = player.GlobalPosition;
            }
        }
    }
}