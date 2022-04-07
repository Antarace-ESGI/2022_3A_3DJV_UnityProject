﻿using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Vehicle
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();

        public readonly string name;
        public readonly Color color;
        public readonly float baseThrottle;
        public readonly float boostMultiplier;
        public readonly float boostDuration;
        public readonly float yawStrength;

        private Vehicle(
            string name,
            Color color,
            float baseThrottle = 10f,
            float boostMultiplier = 2f,
            float boostDuration = 2f,
            float yawStrength = 1.5f )
        {
            this.name = name;
            this.color = color;
            this.baseThrottle = baseThrottle;
            this.boostMultiplier = boostMultiplier;
            this.boostDuration = boostDuration;
            this.yawStrength = yawStrength;
        }

        public static void CreateVehicle(string name,
            Color color,
            float baseThrottle,
            float boostMultiplier,
            float boostDuration,
            float yawStrength)
        {
            _vehicles.Add(new Vehicle(name, color, baseThrottle, boostMultiplier, boostDuration, yawStrength));
        }

        public static List<Vehicle> Vehicles => _vehicles;
    }
}