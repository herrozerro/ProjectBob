using Godot;
using System;

public partial class InputResponse : VBoxContainer
{
    public void SetText(string input, string response)
    {
        var inputLabel = (Label)GetNode("UserInput");
        inputLabel.Text = " > " + input;
        
        var responseLabel = (Label)GetNode("Response");
        responseLabel.Text = response;
    }
}