using Godot;
using System;
using System.Text;

public partial class TextParser : Control
{
	private PackedScene _inputResponse = (PackedScene)GD.Load("res://Scenes/Console/input_response.tscn");
	private PackedScene _response = (PackedScene)GD.Load("res://Scenes/Console/response.tscn");
	
	private VBoxContainer _historyRows;
	private ScrollContainer _scrollContainer;
	private CommandParser _commandParser;
	private ScrollBar _scrollBar;

	[Export]
	public int MaxScrollLines = 30;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_historyRows = (VBoxContainer)GetParent().FindChild("HistoryRows");
		_scrollContainer = (ScrollContainer)GetParent().FindChild("Scroll");
		_scrollBar = _scrollContainer.GetVScrollBar();
		_commandParser = (CommandParser)GetParent().FindChild("CommandParser");
		
		_scrollBar.Connect(ScrollBar.SignalName.Changed, new Callable(this, nameof(OnScrollBarChanged)));
		
		DisplayStartingMessage();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnScrollBarChanged()
	{
		_scrollContainer.ScrollVertical = (int)_scrollBar.MaxValue;
	}
	
	private void OnCommandTextSubmitted(string new_text)
	{
		if (string.IsNullOrWhiteSpace(new_text))
			return;
		
		InputResponse response = _inputResponse.Instantiate<InputResponse>();

		var commandResponse = _commandParser.ProcessCommand(new_text);
		response.SetText(new_text, commandResponse);
		
		AddControlToHistory(response);
	}

	private void DisplayStartingMessage()
	{
		Label response = _response.Instantiate<Label>();
		
		StringBuilder sb = new StringBuilder();

		sb.AppendLine("Welcome to the Singularity Lathe Console!");
		sb.AppendLine("Type 'help' for a list of commands.");
		sb.AppendLine("Your directive is to explore the solar system and file all anomalies you find.");

		response.Text = sb.ToString();
		
		AddControlToHistory(response);
	}
	
	private void AddControlToHistory(Control control)
	{
		_historyRows.AddChild(control);
		DeleteHistoryBeyondLimit();
	}

	private void DeleteHistoryBeyondLimit()
	{
		if (_historyRows.GetChildCount() > MaxScrollLines)
		{
			var child = _historyRows.GetChild(0);
			//_historyRows.RemoveChild(child);
			child.QueueFree();
		}
	}
}
