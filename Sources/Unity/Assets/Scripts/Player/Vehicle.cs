using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Vehicle
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();
        private static bool _initialized;

        public readonly string name;
        public readonly Material material;
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
            float yawStrength = 1.5f)
        {
            // TODO: Use model reference instead of material
            var material = new Material(Shader.Find("Standard"))
            {
                color = color
            };

            this.name = name;
            this.material = material;
            this.baseThrottle = baseThrottle;
            this.boostMultiplier = boostMultiplier;
            this.boostDuration = boostDuration;
            this.yawStrength = yawStrength;
        }

        private static void CreateVehicle(string name,
            Color color,
            float baseThrottle,
            float boostMultiplier,
            float boostDuration,
            float yawStrength)
        {
            _vehicles.Add(new Vehicle(name, color, baseThrottle, boostMultiplier, boostDuration, yawStrength));
        }

        public static void InitializeVehicles()
        {
            if (!_initialized)
            {
                CreateVehicle("RedPlayer", Color.blue, 10, 2, 2, 5);
                CreateVehicle("Quic", Color.red, 15, 1.5f, 2, 2);
                CreateVehicle("Boosty", Color.gray, 5, 5, 2, 5);
                CreateVehicle("Agil", Color.green, 10, 10, 1, 10);

                _initialized = true;
            }
        }

        public static List<Vehicle> Vehicles => _vehicles;
    }
}