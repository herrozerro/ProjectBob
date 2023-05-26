using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ProjectBob.Scripts;
using SingularityLathe.Forge.StellarForge;
using SingularityLathe.Forge.StellarForge.Services;
using SingularityLathe.RadLibs;

public partial class GameSim : Node
{
	private StarSystemBuilderService _solarSystemGenerator;
	private StarSystem _solarSystem;
	
	public StarSystem SolarSystem
	{
		get => _solarSystem;
		set => _solarSystem = value;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StringBuilder sb = new StringBuilder();
		using var file = FileAccess.Open("res://json/anomalies.json", FileAccess.ModeFlags.Read);
		while (!file.EofReached())
		{
			sb.AppendLine(file.GetLine());
		}
		var anomalies = JsonConvert.DeserializeObject<List<Anomaly>>(sb.ToString());
		
		var rand = new Random();
		var stellarConfig = new StellarForgeConfiguration();
		_solarSystemGenerator = new StarSystemBuilderService(rand,
			new PlanetBuilderService(rand, new MoonBuilderService(rand, stellarConfig), stellarConfig),
			new AnomalyGeneratorService(rand, anomalies), stellarConfig);
		
		_solarSystem = _solarSystemGenerator.GenerateStar().GenerateSystem().Build();
	}
}
