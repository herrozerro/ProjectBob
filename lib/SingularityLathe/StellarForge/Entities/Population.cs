﻿using System;
using System.Collections.Generic;
using System.Linq;
using SingularityLathe.Forge.Interfaces;

namespace SingularityLathe.Forge.StellarForge
{
    public class Population : IWeightedItem
    {
        public int ItemWeight { get; set; }
        public string Description { get; set; }

        public static List<Population> GetPopulations()
        {
            var pops = new List<Population>
            {
                new()
                {
                    Description = "Failed colonoy",
                    ItemWeight = 20
                },
                new()
                {
                    Description = "Outpost",
                    ItemWeight = 15
                },
                new()
                {
                    Description = "Tens of thousands",
                    ItemWeight = 15
                },
                new()
                {
                    Description = "Hundreds of thousands",
                    ItemWeight = 10
                },
                new()
                {
                    Description = "Millions",
                    ItemWeight = 10
                },
                new()
                {
                    Description = "Billions",
                    ItemWeight = 20
                },
                new()
                {
                    Description = "Alien Civilization",
                    ItemWeight = 5
                }
            };

            return pops;
        }

        public static Population GetRandomPopulation(Random rnd)
        {
            var pop = GetPopulations().GetRandomWeightedItem(rnd);

            return (Population)pop;
        }
    }


}
