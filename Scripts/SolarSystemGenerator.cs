using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using SingularityLathe.Forge.StellarForge;
using SingularityLathe.Forge.StellarForge.Services;
using SingularityLathe.RadLibs;

namespace ProjectBob.Scripts;

public partial class SolarSystemGenerator : Node3D
{
	private List<Node3D> _planets = new();
	private Node3D _star = new();

	private StarSystemBuilderService builder;

	private IStellarBody SolarSystem;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var rand = new Random();
		var stellarConfig = new StellarForgeConfiguration();
		builder = new StarSystemBuilderService(rand,
			new PlanetBuilderService(rand, new MoonBuilderService(rand, stellarConfig), stellarConfig),
			new AnomalyGeneratorService(rand, new List<Anomaly>()), stellarConfig);

		GenerateSystem();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var planet in _planets)
		{
			OrbitPlanet((float)delta, _star, planet);
		}
	}
	
	private void GenerateSystem()
	{
		SolarSystem = builder.GenerateStar().GenerateSystem().Build().SystemStar;
		
		PackedScene starScene = (PackedScene)GD.Load("res://Scenes/SolarSystem/Star.tscn");
		PackedScene planetScene = (PackedScene)GD.Load("res://Scenes/SolarSystem/Planet.tscn");

		_star = starScene.Instantiate<Node3D>();

		foreach (var planet in _planets)
		{
			_star.RemoveChild(planet);
			planet.QueueFree();
		}
		_planets.Clear();
		
		_star.GlobalPosition = new Vector3(0, 0, 0);
		AddChild(_star);
		var rnd = new Random();
		int numberOfPlanets = rnd.Next(1, 5);

		int i = 0;
		var rand = new Random();
		foreach (var planet in SolarSystem.ChildBodies)
		{
			Node3D planetInstance = planetScene.Instantiate<Node3D>();
			planetInstance.GlobalPosition = new Vector3( rand.Next(200 * (i + 1), 500 * (i + 1)), 0, 0);
			
			var planetSphere3D = (CsgSphere3D)planetInstance.FindChild("CSGSphere3D");
			var mat = new StandardMaterial3D();
			mat.AlbedoColor = new Color(rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle());
			planetSphere3D.Material = mat;
			
			_star.AddChild(planetInstance);
			_planets.Add(planetInstance);
			i++;
		}

		Globals.SolarSystem = SolarSystem;
	}

	private void OrbitPlanet(float delta, Node3D starInstance, Node3D planetInstance)
	{
		// Get the current position relative to the star
		Vector3 relativePos = planetInstance.GlobalPosition - starInstance.GlobalPosition;

		// Calculate the distance from the star
		float distance = relativePos.Length();

		// Calculate the rotation in radians per second
		float rotationSpeed = 100 / distance; // Adjust the 1000 value to change the base speed

		// Rotate the relative position vector around the Y axis
		Vector3 newRelativePos = new Vector3(
			relativePos.X * Mathf.Cos(rotationSpeed * delta) - relativePos.Z * Mathf.Sin(rotationSpeed * delta),
			relativePos.Y,
			relativePos.X * Mathf.Sin(rotationSpeed * delta) + relativePos.Z * Mathf.Cos(rotationSpeed * delta));

		// Update the planet's global position
		planetInstance.GlobalPosition = starInstance.GlobalPosition + newRelativePos;
	}
	
	private void OnMissionControlSolarSystemCommand(string command)
	{
		if (command.ToLower().StartsWith("go"))
		{
			GenerateSystem();
		}
	}
}



