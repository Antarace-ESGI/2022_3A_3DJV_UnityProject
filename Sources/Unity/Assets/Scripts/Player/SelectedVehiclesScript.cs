using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedVehiclesScript : MonoBehaviour
{
    // Vehicle selection code
    private static SelectedVehiclesScript Instance { get; set; }

    /// <summary>
    /// Dictionary to map the following
    /// Player index: vehicle index
    /// </summary>
    private static readonly Dictionary<int, int> _vehicles = new Dictionary<int, int>();
    private static readonly Dictionary<int, InputDevice> _devices = new Dictionary<int, InputDevice>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static void SetSelectedVehicle(int playerIndex, int vehicleIndex)
    {
        _vehicles[playerIndex] = vehicleIndex;
    }

    public static void SetDeviceForPlayer(int playerIndex, InputDevice device)
    {
        _devices[playerIndex] = device;
    }

    public static int GetSelectedVehicleIndex(int playerIndex)
    {
        return _vehicles[playerIndex];
    }

    public static Dictionary<int, InputDevice> GetAllPlayers()
    {
        return _devices;
    }
}