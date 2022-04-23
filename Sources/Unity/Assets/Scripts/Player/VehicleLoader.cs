using UnityEngine;

namespace Player
{
    public class VehicleLoader : MonoBehaviour
    {
        private static bool _initialized;
        
        public ShipController shipController;
        public MeshRenderer meshRenderer;
        [Range(0, 3)] public int vehicleIndex;

        // Start is called before the first frame update
        void Start()
        {
            if (!_initialized)
            {
                Vehicle.CreateVehicle("RedPlayer", Color.blue, 10, 2, 2, 5);
                Vehicle.CreateVehicle("Quic", Color.red, 15, 1.5f, 2, 2);
                Vehicle.CreateVehicle("Boosty", Color.gray, 5, 5, 2, 5);
                Vehicle.CreateVehicle("Agil", Color.green, 10, 10, 1, 10);

                _initialized = true;
            }
            
            Vehicle vehicle = Vehicle.Vehicles[vehicleIndex];

            shipController.baseThrottle = vehicle.baseThrottle;
            shipController.boostMultiplier = vehicle.boostMultiplier;
            shipController.boostDuration = vehicle.boostDuration;
            shipController.yawStrength = vehicle.yawStrength;

            meshRenderer.material = vehicle.material;
        }
    }
}