using Godot;
using System;

public partial class Fighter : CharacterBody2D
{
	// === CONFIGURACIÓN DE TECLAS ===
	public Key ForwardKey;
	public Key BackwardKey;
	public Key JumpKey;
	public Key CrouchKey;

	// === VALORES DE MOVIMIENTO ===
	[Export] public float MoveSpeed = 250f;
	[Export] public float JumpForce = 3000f;
	[Export] public float Gravity = 9800f;

	// === VARIABLES INTERNAS ===
	private float inputX;
	private bool isJumpPressed;
	private bool isCrouchPressed;

	// === ESTA EN EL SUELO ===
	private RayCast2D groundCheck;
	private bool isGrounded;

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

	private void CheckGround()
	{
		isGrounded = groundCheck.IsColliding();
	}

	// === START ===
	public override void _Ready()
	{
		ForwardKey = Key.D;
		BackwardKey = Key.A;
		JumpKey = Key.W;
		CrouchKey = Key.S;

		groundCheck = GetNode<RayCast2D>("GroundCheck");
	}

	// === UPDATE ===
	public override void _Process(double delta)
	{
	}

	// === UPDATE FISICO ===
	public override void _PhysicsProcess(double delta) //Se llama en cada frame fisico
	{
		GetInput();
		CheckGround(); //Comprueba si estamos tocando el suelo

		// Aplicar movimiento horizontal
		Velocity = new Vector2(inputX * MoveSpeed, Velocity.Y); //La velocidad en X se multiplica por MoveSpeed y la velocidad en Y se queda como estaba

		// Aplicar gravedad
		if (!isGrounded)
		{
			Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);
		}
		else if (Velocity.Y > 0)
		{
			Velocity = new Vector2(Velocity.X, 0);
		}

		// Saltar
		if (isJumpPressed && isGrounded)
		{
			Velocity = new Vector2(Velocity.X, -JumpForce);
			isGrounded = false; // Acaba de saltar
		}

		// Mover el personaje manualmente (sin MoveAndSlide)
		Position += Velocity * (float)delta;
	}
}
