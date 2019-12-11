using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(PlayerInput), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    
    private Vector2 _move;

    private bool _fire = false;
    private float _fireTime;


    void Update()
    {
        transform.Translate(_move * _speed * Time.deltaTime);

        if (_fire && Time.time - _fireTime > 0.1)
        {
            _fire = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

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
        _move = context.ReadValue<Vector2>();
    }

    // I added here simple "context" exemple, if you want to do specific action on how the input was made.
    // In Unity's exemple they used this state to make some kind of "Charging" shoot
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _fire = true;
            _fireTime = Time.time;
            GetComponent<SpriteRenderer>().color = Color.red;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Suspect"));
            if (hit.collider != null)
            {
                hit.collider.GetComponent<SuspectController>().OnKilled();
                Debug.Log("Did Hit: " + hit.collider);
            }
            else
            {
                Debug.Log("Nothing here");
            }
        }
    }
}
