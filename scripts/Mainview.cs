using Godot;
using System;

public partial class Mainview : Node2D
{
	private Bird _bird;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Buscamos o nó de forma nativa e segura aqui
		_bird = GetNode<Bird>("Bird");
	}

public void OnTouchAreaDidTouch()
{
	//Verifica se o objeto guardado em _bird possui o método "did_touch"
	if (_bird != null)
	{
		//Executa o método usando o nome original dele no Godot
		_bird.DidTouch();
	}
		
} 
}
