using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class VehicleLoader : MonoBehaviour
    {
        public ShipController shipController;
        public MeshRenderer meshRenderer;
        [Range(0, 3)] public int vehicleIndex;

        // Start is called before the first frame update
        void Start()
        {
            // Should not be loaded in a normal usage
            Vehicle.InitializeVehicles();

            // Get vehicle index from SelectedVehiclesScript
            var playerIndex = GetComponent<PlayerInput>().playerIndex;
            vehicleIndex = SelectedVehiclesScript.GetSelectedVehicleIndex(playerIndex);
            Debug.Log($"Found vehicle {vehicleIndex} for player {playerIndex}");

            Vehicle vehicle = Vehicle.Vehicles[vehicleIndex];

            shipController.baseThrottle = vehicle.baseThrottle;
            shipController.boostMultiplier = vehicle.boostMultiplier;
            shipController.boostDuration = vehicle.boostDuration;
            shipController.yawStrength = vehicle.yawStrength;

            meshRenderer.material = vehicle.material;
        }
    }
}