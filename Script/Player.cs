using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 300f;

    [Export]
    public bool FlashlightStartsOn = true;

    public PointLight2D FlashlightNode;


    public override void _Ready()
    {
        if (HasNode("FlashlightPivot/FlashlightLight"))
        {
            FlashlightNode =
                GetNode<PointLight2D>(
                    "FlashlightPivot/FlashlightLight"
                );

            FlashlightNode.Visible = FlashlightStartsOn;
        }
    }


    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector(
            "move_left",
            "move_right",
            "move_up",
            "move_down"
        );


        Velocity = direction * Speed;


        MoveAndSlide();


        if (Input.IsActionJustPressed("Toggle_Flashlight"))
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        if (FlashlightNode == null)  return;

        FlashlightNode.Visible = !FlashlightNode.Visible;
    }
}