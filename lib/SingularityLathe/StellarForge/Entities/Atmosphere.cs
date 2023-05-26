using System;
using System.Collections.Generic;

namespace SingularityLathe.Forge.StellarForge
{
    public class Atmosphere
    {
        public string AtmosphereType { get; set; }
        public bool IsBreathable { get; set; }
        public double LifeWeight { get; set; }

        public static List<Atmosphere> GetAtmospheres()
        {
            var atmos = new List<Atmosphere>
            {
                new()
                {
                    AtmosphereType = "Corrosive",
                    IsBreathable = false,
                    LifeWeight = .01
                },

                new()
                {
                    AtmosphereType = "Inert Gas",
                    IsBreathable = false,
                    LifeWeight = .05
                },

                new()
                {
                    AtmosphereType = "Airless or thin atmosphere",
                    IsBreathable = false,
                    LifeWeight = .07
                },

                new()
                {
                    AtmosphereType = "Breathable mix",
                    IsBreathable = true,
                    LifeWeight = .2
                },

                new()
                {
                    AtmosphereType = "Thick atmosphere, breathable with a pressure mask",
                    IsBreathable = true,
                    LifeWeight = .25
                },

                new()
                {
                    AtmosphereType = "Invasive, toxic atmosphere",
                    IsBreathable = false,
                    LifeWeight = .05
                },

                new()
                {
                    AtmosphereType = "Corrosive and invasive atmosphere",
                    IsBreathable = false,
                    LifeWeight = .04
                }
            };

            return atmos;
        }

        public static Atmosphere GetRandomAtmosphere(Random rnd)
        {
            var atmos = GetAtmospheres();

            return atmos[rnd.Next(atmos.Count)];
        }
    }


}
