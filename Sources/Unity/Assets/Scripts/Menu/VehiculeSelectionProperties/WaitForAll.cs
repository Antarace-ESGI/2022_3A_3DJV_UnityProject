using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the wait for all player to choose a vehicle mechanism
/// </summary>
public class WaitForAll : MonoBehaviour
{
    // Those must be set in the inspector
    public GameObject selectionCanvas; // Displayed to the player to let them chose a vehicle
    public GameObject selectedCanvas; // Displayed when the player has chosen their vehicle
    public PlayerInput input; // Can be used to retrieve the player index number

    public PlayerSelectionScript playerSelection; // This is set by the PlayerSelectionScript itself
    private bool _isReady;

    /// <summary>
    /// Tells whether or not the player has clicked the "OK" button in the selectionCanvas
    /// Is updated by ActionMenuScript
    /// </summary>
    public bool IsReady
    {
        get => _isReady;
        set
        {
            _isReady = value;
            selectionCanvas.SetActive(false);
            selectedCanvas.SetActive(true);
            playerSelection.CheckPlayerReady();
        }
    }

    /// <summary>
    /// Index of the selected vehicle array (Vehicle._vehicles)
    /// Is updated by VehicleButton
    /// </summary>
    public int ChosenVehicle { get; set; }
}
