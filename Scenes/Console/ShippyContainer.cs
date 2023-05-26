using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ShippyContainer : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ToggleShippyContainer()
	{
		var isVisible = this.Visible;

		if (!isVisible)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	public async Task ShippySpeak(List<string> lines)
	{
		var shippyText = FindChild("ShippyText") as Label;
		var shippyFace = FindChild("ShippyFace") as TextureRect;

		var i = 1;
		foreach (var line in lines)
		{
			shippyText.Text = line;
			shippyFace.Texture = ResourceLoader.Load($"res://textures/Shippy/slice{i++}.png") as Texture2D;
			await ToSignal(GetTree().CreateTimer(5), "timeout");
		}
	}
}
