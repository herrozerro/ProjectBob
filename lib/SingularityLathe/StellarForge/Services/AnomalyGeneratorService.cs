using SingularityLathe.RadLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;

namespace SingularityLathe.Forge.StellarForge.Services
{
    public class AnomalyGeneratorService
    {
        private readonly Random _random;
        private readonly List<Anomaly> _anomalies;
        public AnomalyGeneratorService(Random random, List<Anomaly> anomalies)
        {
            _random = random;
            _anomalies = anomalies;
        }
        
        public Anomaly GenerateAnomaly()
        {
            return _anomalies[_random.Next(0, _anomalies.Count)];
        }
    }
}
