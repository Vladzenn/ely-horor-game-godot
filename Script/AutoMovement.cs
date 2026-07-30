using Godot;
using System;

public partial class AutoMovement : Sprite2D
{
	public enum MovementMode
	{
		IN_CIRCLES
	}

	[Export] public float speed = 100.0f;
    [Export] public MovementMode movementMode = MovementMode.IN_CIRCLES;
	[Export] public float radius = 100.0f;

	private Vector2 centerPosition;
	private float angle = 0.0f;
    public override void _Ready()
	{
		centerPosition = GlobalPosition;
    }

	public override void _Process(double delta)
	{
		switch (movementMode)
		{
			case MovementMode.IN_CIRCLES:
				angle += speed * (float)delta;
				GlobalPosition = centerPosition + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
				break;
        }
	}
}
