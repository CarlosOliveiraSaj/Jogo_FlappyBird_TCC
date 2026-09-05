using Godot;
using System;

public partial class TouchArea : Area2D
{
    [Signal]
    public delegate void DidTouchEventHandler();
    public override void _Ready()
    {
        InputEvent += OnInputEvent;
    }

private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
{
    if (@event is InputEventScreenTouch touchEvent && touchEvent.Pressed)
    {
        GD.Print("Tela tocada!");
            
        EmitSignal(SignalName.DidTouch);
    }
}
}
