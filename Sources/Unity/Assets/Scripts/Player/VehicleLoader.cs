using UnityEngine;

namespace Player
{
    public class VehicleLoader : MonoBehaviour
    {
        public ShipController shipController;
        public MeshRenderer meshRenderer;
        [Range(0, 3)] public int vehicleIndex;

        // Start is called before the first frame update
        void Start()
        {
            Vehicle.InitializeVehicles();

            Vehicle vehicle = Vehicle.Vehicles[vehicleIndex];

            shipController.baseThrottle = vehicle.baseThrottle;
            shipController.boostMultiplier = vehicle.boostMultiplier;
            shipController.boostDuration = vehicle.boostDuration;
            shipController.yawStrength = vehicle.yawStrength;

            meshRenderer.material = vehicle.material;
        }
    }
}