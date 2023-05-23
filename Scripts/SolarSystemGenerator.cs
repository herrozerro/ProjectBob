using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ProjectBob.Scripts;

public partial class SolarSystemGenerator : Node3D
{
	private List<Node3D> _planets = new();
	private Node3D _star = new();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		
		for (int i = 0; i < numberOfPlanets; i++)
		{
			Node3D planetInstance = planetScene.Instantiate<Node3D>();
			planetInstance.GlobalPosition = new Vector3(200 * (i + 1), 0, 0);
			var planet = (CsgSphere3D)planetInstance.FindChild("CSGSphere3D");
			var mat = new StandardMaterial3D();
			mat.AlbedoColor = new Color(rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle());
			planet.Material = mat;
			_star.AddChild(planetInstance);
			_planets.Add(planetInstance);
		}
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



