using Godot;
using System;

public partial class InputComponent : Node
{
    private Vector2 direction = Vector2.Zero;

    public Vector2 Direction
    {
        get { return direction; }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 previousDirection = direction;
        direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        // 调试输出
        if (direction != previousDirection)
        {
            GD.Print($"InputComponent: Direction changed to {direction}");
        }

        // 也可以输出原始按键状态
        if (Input.IsActionPressed("ui_left"))
            GD.Print("Left key pressed");
        if (Input.IsActionPressed("ui_right"))
            GD.Print("Right key pressed");
        if (Input.IsActionPressed("ui_up"))
            GD.Print("Up key pressed");
        if (Input.IsActionPressed("ui_down"))
            GD.Print("Down key pressed");
    }
}
