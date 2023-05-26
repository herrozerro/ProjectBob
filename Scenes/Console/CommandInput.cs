using Godot;
using System;

public partial class CommandInput : LineEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnCommandTextSubmitted(string new_text)
	{
		this.Clear();
	}
}
