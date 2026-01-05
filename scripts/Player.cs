using Godot;
using System;
using System.Collections.Specialized;

public partial class Player : CharacterBody2D
{
    private NodePath inputComponentPath = "InputComponent";
	private InputComponent _inputComponent = new();
    private const float SPEED = 500.0f;
	private const float acc = 0.95f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _inputComponent = GetNode<InputComponent>(inputComponentPath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if ((long)(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y) == 0 && _inputComponent.Direction != Vector2.Zero)
        {
            Velocity = _inputComponent.Direction * SPEED;
        }
        else
        {
            float newX = Velocity.X * acc;
            float newY = Velocity.Y * acc;
            if (Math.Abs(newX) < 100) newX = 0;
            if (Math.Abs(newY) < 100) newY = 0;
            Velocity = new(newX, newY);
        }

        MoveAndSlide();
	}
}
