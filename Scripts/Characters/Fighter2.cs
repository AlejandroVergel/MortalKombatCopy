using Godot;
using System;

// KEYs
public Key ForwardKey;
public Key BackwardKey;
public key JumpKey;
public Key CrouchKey;

private float inputX;
private bool isJumpPressed;
private bool isCrouchPressed;

private void GetInput()
{
	if (Input.IsKeyPressed(ForwardKey))
	{
		inputX = 1;
	}
	else if (Input.IsKeyPressed(BackwardKey))
	{
		inputX = -1;
	}
}


public partial class Fighter2 : CharacterBody2D
{
	public override void _Ready()
	{
		ForwardKey = Key.D;
		BackwardKey = Key.A;
		JumpKey = Key.W;
		CrouchKey = Key.S;
	}
}
