using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(PlayerInput))]
public class BasicPlayerInput : MonoBehaviour
{
    private Vector2 _look;
    private float jump;
    private Vector2 _move;

    // Called when the device is disconnected
    public void DeviceLostEvent(PlayerInput test)
    {
        Destroy(gameObject);
    }

    // All of this callback can be found on the prefab attached to this script
    // Just click on "PlayerInput" script -> Events -> Name of the group action
    // And just drag'n'drop whatever callback you want. 
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Move");
        _move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Look");
        _look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Jump");
        jump = context.ReadValue<float>();
    }

    // I added here simple "context" exemple, if you want to do specific action on how the input was made.
    // In Unity's exemple they used this state to make some kind of "Charging" shoot
    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Fire");
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }
}
