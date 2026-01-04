using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private NodePath inputComponentPath = "InputComponent";
	private InputComponent _inputComponent = new();
    private const float SPEED = 300.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _inputComponent = GetNode<InputComponent>(inputComponentPath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Velocity = _inputComponent.Direction * SPEED;
		MoveAndSlide();
	}
}
