using System;
using System.Collections.Generic;

namespace SingularityLathe.Forge.StellarForge
{
    public class Tempature
    {
        public string Description { get; set; }
        public double LifeWeight { get; set; }

        public static List<Tempature> GetTempatures()
        {
            var temps = new List<Tempature>
            {
                new()
                {
                    Description = "Frozen",
                    LifeWeight = .1
                },

                new()
                {
                    Description = "Variable cold-to-temperate",
                    LifeWeight = .05
                },

                new()
                {
                    Description = "Cold",
                    LifeWeight = .1
                },

                new()
                {
                    Description = "Temperate",
                    LifeWeight = .15
                },

                new()
                {
                    Description = "Warm",
                    LifeWeight = .2
                },

                new()
                {
                    Description = "Variable temperate-to-warm",
                    LifeWeight = .1
                },

                new()
                {
                    Description = "Burning",
                    LifeWeight = .01
                }
            };

            return temps;
        } 

        public static Tempature GetRandomTemperature(Random rnd)
        {
            var temps = GetTempatures();

            return temps[rnd.Next(temps.Count)];
        }
    }


}
