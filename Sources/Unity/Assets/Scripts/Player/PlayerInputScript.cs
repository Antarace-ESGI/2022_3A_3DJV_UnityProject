using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private PlayerController _controls;
    
    // Accessor

    [SerializeField] private GameObject _blaster;
    
    private void Start()
    {
        _controls ??= new PlayerController();
        
        // Input callback
        
        _controls.Player.Use.performed += ctx => UseBonus();
        _controls.Player.Shoot.performed += ctx => Shoot();
        _controls.Player.Boost.performed += ctx => Boost();
        _controls.Player.Boost.canceled += ctx => UnBoost();
        _controls.Player.Pause.performed += ctx => Pause();
        _controls.Player.Jump.performed += ctx => Jump();
        _controls.Player.Movement.performed += ctx => Direction(ctx.ReadValue<Vector2>());
        
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

    // Action function

    private void Direction(Vector2 dir)
    {
        Debug.Log(dir);
        //gameObject.GetComponent<ShipController>().InputUpdate(dir);
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        Vector3 rawInput = new Vector3(movement.x, 0, movement.y);
        //gameObject.GetComponent<ShipController>().InputUpdate(rawInput);
    }
    
    private void UseBonus()
    {
        gameObject.GetComponent<PlayerStatsScript>().unableBonusUse();
    }

    private void Shoot()
    {
        _blaster.GetComponent<PlayerBlaster>().Shoot();
    }

    private void Boost()
    {
        gameObject.GetComponent<ShipController>().ActiveBoost(true);
    }

    private void UnBoost()
    {
        gameObject.GetComponent<ShipController>().ActiveBoost(false);
    }
    
    private void Jump()
    {
        
    }

    private void Pause()
    {
        
    }
}
