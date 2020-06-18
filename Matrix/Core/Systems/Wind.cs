﻿using System;
using Angem;
using Matrix.Tools;
using Precalc;

namespace Matrix.Core.Systems
{
    public class Wind : RegionSystem
    {
        public readonly int2[] Directions =
            {
                int2.Right,
            },
            AdditionalDirections =
            {
                int2.Forward,
                int2.Back,
            };

        public readonly NaturalFunction<double>[] ChanceFunctions;

        public Wind()
        {
            ChanceFunctions = new NaturalFunction<double>[Terrain.Size];

            for (var i = 0; i < ChanceFunctions.Length; i++)
            {
                var chance = BasicChances[i];
                ChanceFunctions[i] = new NaturalFunction<double>(
                    delta => chance * Math.Pow(1.41, delta),
                    20,
                    -10);
            }
        }

        [Constant] public double[] BasicChances;
        
        public void MoveGas(int2 v, Region region, byte gas)
        {
            foreach (var dir in Directions)
            {
                if (!(v + dir).Inside(State.Field.Size)) continue;

                var other = State.Field[v + dir];
                
                if (!State.Random.Chance(
                    ChanceFunctions[gas].Calculate( 
                        region.Terrain.SliceFrom(Terrain.CLOUDS) 
                        - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2)))
                    continue;

                other.Terrain.Clouds += region.Terrain.Clouds;
                region.Terrain.Clouds = 0;
                break;
            }

            foreach (var dir in AdditionalDirections)
            {
                if (!(v + dir).Inside(State.Field.Size)) continue;

                var other = State.Field[v + dir];
                if (!State.Random.Chance(
                    ChanceFunctions[gas].Calculate(
                        region.Terrain.SliceFrom(Terrain.CLOUDS) 
                        - other.Terrain.SliceFrom(Terrain.CLOUDS) - 2) / 2)) continue;

                other.Terrain.Clouds += region.Terrain.Clouds;
                region.Terrain.Clouds = 0;
                break;
            }
        }

        protected override void UpdateEntity(int2 position, Region region)
        {
            for (byte i = 0; i < Terrain.Size; i++)
            {
                if (BasicChances[i] > 0)
                    MoveGas(position, region, i);
            }
        }
    }
}