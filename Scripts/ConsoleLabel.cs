using Godot;
using System;
using Environment = System.Environment;

public partial class ConsoleLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnConsoleControllerConsoleResponse(string message)
	{
		this.Text += Environment.NewLine + $"{DateTime.UtcNow.ToShortTimeString()}: {message}";
		var parent = this.GetParent<ScrollContainer>();
		parent.ScrollVertical = (int)parent.GetVScrollBar().MaxValue + 20;
	}
}






