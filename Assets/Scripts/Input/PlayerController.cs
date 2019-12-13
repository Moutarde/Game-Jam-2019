using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(PlayerInput), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public int Slot { get; private set; }
    public int Score { get; private set; }
    public Color PlayerColor { get; set; }

    [SerializeField]
    private float _speed;
    
    private Vector2 _move;

    private bool _fire = false;
    private float _fireTime;

    private SpriteRenderer m_spriteRenderer;


    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        int result = GameManager.Instance.RegisterPlayer(this);
        if (result == -1)
        {
            Destroy(gameObject);
        }
        else
        {
            Slot = result;
        }

        m_spriteRenderer.color = PlayerColor;
    }

    void Update()
    {
        transform.Translate(_move * _speed * Time.deltaTime);

        if (_fire && Time.time - _fireTime > 0.1)
        {
            _fire = false;
            m_spriteRenderer.color = PlayerColor;
        }

        CheckHover();
    }

    // Called when the device is disconnected
    public void DeviceLostEvent(PlayerInput test)
    {
        GameManager.Instance.UnregisterPlayer(this);
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
            m_spriteRenderer.color = Color.red;
            GetComponent<ShootSoundManager>().PlayShootSound();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("SuspectHitBox"));
            if (hit.collider != null)
            {
                SuspectController suspectController = hit.collider.GetComponent<SuspectController>();
                GameManager.Instance.HandlePlayerKill(this, suspectController);
            }
        }
    }

    public void OnPressStart(InputAction.CallbackContext context)
    {
        GameManager.Instance.HandleStartPressed();
    }

    public void Reset()
    {
        Score = 0;
        // TODO: reset position
    }

    public void AddPoints(int value)
    {
        Score += value;
    }

    private void CheckHover()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("SuspectHitBox"));
        if (hit.collider != null)
        {
            transform.localScale = new Vector3(0.8f, 0.8f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f);
        }
    }

}
