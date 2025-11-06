using Godot;
using System;
using System.Numerics;

public partial class Fighter : CharacterBody2D
{
	// === CONFIGURACIÓN DE TECLAS ===
	public Key ForwardKey;
	public Key BackwardKey;
	public Key JumpKey;
	public Key CrouchKey;

	// === VALORES DE MOVIMIENTO ===
	[Export] public float MoveSpeed = 250f;
	[Export] public float JumpForce = 600f;
	[Export] public float Gravity = 1200f;

	// === VARIABLES INTERNAS ===
	private float inputX;
	private bool isJumpPressed;
	private bool isCrouchPressed;

	// === LECTURA DE INPUT ===
	private void GetInput()
	{
		inputX = 0; //Reseteamos la velocidad a 0 en cada frame mientras no se mueva
		if (Input.IsKeyPressed(ForwardKey))
		{
			inputX += 1;
		}
		if (Input.IsKeyPressed(BackwardKey)) //Si es else if se priorizan, si son dos if se cancelan
		{
			inputX -= 1;
		}

		isJumpPressed = Input.IsKeyPressed(JumpKey); //Si se esta pulsando 'JumpKey' Input.IsKeyPressed(JumpKey) es igual a true, isJumpPressed es igual a Input.IsKeyPressed(JumpKey), por lo que isJumpPressed es igual a true
		isCrouchPressed = Input.IsKeyPressed(CrouchKey);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ForwardKey = Key.D;
		BackwardKey = Key.A;
		JumpKey = Key.W;
		CrouchKey = Key.S;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta) //Se llama en cada frame fisico
	{
		GetInput();
		Velocity = new Vector2(inputX * MoveSpeed, Velocity.Y); //La velocidad en X se multiplica por MoveSpeed y la velocidad en Y se queda como estaba
		
		if (!IsOnFloor())
		{
			Velocity = new Vector2(Veclocity.X, Velocity.Y + Gravity * (float)delta);
		}

		if (isJumpPressed && IsOnFloor())
		{
			Velocity = new Vector2(Veclocity.X, -JumpForce);
		}

		MoveAndSlide();
	}
}
