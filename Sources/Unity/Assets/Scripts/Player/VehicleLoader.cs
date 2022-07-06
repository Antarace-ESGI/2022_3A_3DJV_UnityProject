using UnityEngine;
using UnityEngine.InputSystem;

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
            // Should not be loaded in a normal usage
            Vehicle.InitializeVehicles();

            // Get vehicle index from SelectedVehiclesScript
            var playerInput = GetComponent<PlayerInput>();
            if (playerInput)
            {
                var playerIndex = playerInput.playerIndex;
                vehicleIndex = SelectedVehiclesScript.GetSelectedVehicleIndex(playerIndex);
            }
            else
            {
                vehicleIndex = 0; // TODO: Let AI chose their vehicle ?
            }

            var vehicle = Vehicle.Vehicles[vehicleIndex];

            shipController.baseThrottle = vehicle.baseThrottle;
            shipController.boostMultiplier = vehicle.boostMultiplier;
            shipController.boostDuration = vehicle.boostDuration;
            shipController.yawStrength = vehicle.yawStrength;

            meshRenderer.material = vehicle.material;
        }
    }
}