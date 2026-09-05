using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Bird : CharacterBody2D
{
	[Signal]
	public delegate void DidDiedEventHandler();
	
	public bool DidStartGame = false;
	public bool IsDead = false;
	public const float JumpVelocity = -440.0f;
    private AnimatedSprite2D anim;
	private Line2D arrow;

    public override void _Ready()
    {
        anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		arrow = GetNode<Line2D>("Arrow");

		arrow.ClearPoints();
		arrow.AddPoint(new Vector2(0, 40));
		arrow.AddPoint(new Vector2(0, -40));
		arrow.AddPoint(new Vector2(-10, -25));
		arrow.AddPoint(new Vector2(0, -40));
		arrow.AddPoint(new Vector2(10, -25));

		arrow.Hide();

    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//Adiciona gravidade
		if (DidStartGame)
		{
			velocity += GetGravity() * (float)delta;
			DidTouch();
		}

		Velocity = velocity;
		MoveAndSlide();

		if (GetSlideCollisionCount() != 0)
		{
			DeathAction();
		}

		RotateBird((float)delta);
		
		UpdateArrow();
	}
	public void DidTouch()
	{
		if (DidStartGame == false)
		{
			DidStartGame = true;
			GameManager.Instance.IsGamePlaying = true; //Game manager
		}

		if (!IsDead)
		{
			Jump();
		}
	}

	public void Jump()
	{
		Vector2 velocity = Velocity;
		velocity.Y = JumpVelocity;
		Velocity = velocity;
	}

	public void DeathAction()
	{
		if (!IsDead)
		{
			IsDead = true;
			GameManager.Instance.IsGamePlaying = false; //Game Manager
			anim.Stop();
			EmitSignal(SignalName.DidDied);
		}
	}

	public void RotateBird(float delta)
	{
		if (!DidStartGame) return; 

		float grausAtuais = Mathf.RadToDeg(Rotation);

		if (Velocity.Y > 0 && grausAtuais < 90)
	{
		Rotation += 2 * Mathf.DegToRad(1.4f);
	} 
		else if (Velocity.Y < 0 && grausAtuais > -30)
	{
		Rotation -= 2 * Mathf.DegToRad(1.2f);
	}
	}

	private void UpdateArrow()
	{
    	if (!DidStartGame)
   	{
		//Mostra a seta
        arrow.Hide();
        return;
    }

    	if (Velocity.Y < 0)
    {
        //Mostra a seta do pássaro subindo, rotação 0 para a seta não girar
        arrow.Show();
        arrow.GlobalRotation = 0;
    }
    	else if (Velocity.Y > 0)
    {
        //Mostra a seta do pássaro descendo
       	arrow.Show();
        arrow.GlobalRotation = Mathf.Pi;
    }
    	else
    {
        arrow.Hide();
    }
	}
}
