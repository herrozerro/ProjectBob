using System.Collections.Generic;

namespace SingularityLathe.Forge.StellarForge;

public class Anomaly
{
    public string EventType { get; set; }
    public string EventDescription { get; set; }
    public string Hazard { get; set; }
    public double Difficulty { get; set; }
    public List<EventOption> Options { get; set; }
    
    public Anomaly()
    {
        Options = new List<EventOption>();
    }
}

public class EventOption
{
    public string OptionDescription { get; set; }
    public double SuccessChance { get; set; }
    public double DamageChance { get; set; }
    public List<(string, int)> ResourcesConsumed { get; set; }
    public List<string> Rewards { get; set; }
    public List<string> Consequences { get; set; }
}