using Godot;

namespace ProjectBob.Scripts;

public partial class ConsoleController : Control
{
	private bool _isHidden;
	
	[Signal]
	public delegate void ConsoleResponseEventHandler(string message);
	[Signal]
	public delegate void CommandSubmissionEventHandler(string message);
	[Signal]
	public delegate void CommandCloseEventHandler(bool isHidden);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Display();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Display()
	{
		var newPos = this.Position;
		newPos.X = _isHidden ? 0 : -700;
		this.Position = newPos;
		_isHidden = !_isHidden;
		Globals.IsConsoleVisible = !_isHidden;
		Input.MouseMode = _isHidden ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible;
		
		EmitSignal(SignalName.CommandClose, !_isHidden);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_focus_next"))
		{
			Display();
		}
	}

	private void OnConsoleCommandTextSubmitted(string new_text)
	{
		//Parse commands
		string command = string.Empty;
		string response;
		switch (new_text)
		{
			case string s when s.StartsWith("go"):
				command = "go";
				response = "Going to a new system";
				break;
			case string s when s.ToLower() == "exit":
				Display();
				return;
			default:
				response = "Sorry, that command is not recognized";
				break;
		}

		//Display Text on Label
		EmitSignal(SignalName.ConsoleResponse, new_text);
		//Display Results on label
		EmitSignal(SignalName.ConsoleResponse, response);
		//Signal Command
		if (command != string.Empty)
		{
			EmitSignal(SignalName.CommandSubmission, command);
		}

	}
}



