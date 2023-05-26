using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using SingularityLathe.Forge.StellarForge;

public partial class CommandParser : Node
{
    private GameSim _game;
    public void Initialize(GameSim game)
    {
        _game = game;
    }
    
    
    public string ProcessCommand(string input)
    {
        var words = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (!words.Any())
        {
            return "Error: No words parsed";
        }

        var command = words[0];
        List<string> arguments = new List<string>();
        if (words.Length > 1)
        {
            arguments.AddRange(words.Skip(1).ToList());
        }

        return command switch
        {
            "go" => Go(arguments),
            "scan" => Scan(arguments),
            "shippy" => ToggleShippy(),
            "help" => Help(),
            _ => "Unrecognized command, please try again"
        };
    }

    private string ToggleShippy()
    {
        var shippyContainer = GetParent().FindChild("ShippyContainer") as ShippyContainer;
        
        shippyContainer.ToggleShippyContainer();

        ShippySpeak();
        
        return "Shippy toggled";
    }

    private string ShippySpeak()
    {
        var shippyContainer = GetParent().FindChild("ShippyContainer") as ShippyContainer;

        var shippyLines = new List<string>();
        shippyLines.Add("Hello, I am Shippy");
        shippyLines.Add("I am your ship's AI");
        shippyLines.Add("I will help you navigate the galaxy");
        shippyLines.Add("I can also help you with your ship's systems");
        
        
        Task.Run(()=> shippyContainer.ShippySpeak(shippyLines));

        return string.Empty;
    }

    private string Scan(List<string> arguments)
    {
        if (arguments.Count == 0)
        {
            return ScanSystem();
        }

        if (arguments[0] == "anomaly")
        {
            return ScanAnomalies();
        }

        return "Unrecognized scan command";
    }

    private string ScanAnomalies()
    {
        throw new NotImplementedException();
    }

    private string ScanSystem()
    {
        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine("Scanning system...");
        sb.AppendLine($"Star: {_game.SolarSystem.SystemStar.Name}");
        foreach (Planet planet in _game.SolarSystem.SystemStar.ChildBodies.Cast<Planet>())
        {
            sb.AppendLine($"Planet: {planet.Name} Type: {planet.PlanetaryType.Description} Atmo: {planet.Atmosphere.AtmosphereType}");
        }

        return sb.ToString();
    }

    private string Go(List<string> arguments)
    {
        if (arguments.Count == 0)
        {
            return "Go where?";
        }
        var destinations = _game.SolarSystem.SystemStar.Flatten();
        var destination = destinations.FirstOrDefault(d => d.Name.EndsWith(arguments[0].ToLower()));
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"You have arrived at {destination.Name}");

        if (destination.Anomalies.Any())
        {
            sb.AppendLine($"There are {destination.Anomalies.Count} anomalies detected.");
        }
        
        sb.AppendLine($"There are {destination.ChildBodies.Count} child bodies.");
        if (destination.ChildBodies.Any())
        {
            foreach (var destinationChildBody in destination.ChildBodies)
            {
                switch (destinationChildBody)
                {
                    case Moon moon:
                        sb.AppendLine($"Moon: {moon.Name}");
                        break;
                }
            }
        }

        return sb.ToString();
    }
    
    private string Help()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("Available commands:");
        sb.AppendLine("go [system name] - Go to a new system");
        sb.AppendLine("scan - Scan the current system");
        sb.AppendLine("scan anomaly - Scan any anomalies in your current vicinity");
        sb.AppendLine("help - Display this help text");
        
        return sb.ToString();
    }
}