using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    //Variables


    private Controls _controls;

    [SerializeField] private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }


    [SerializeField] private float _valueX;
    public float ValueX { get => _valueX; }




    // FIRST CALL
    private void Awake()
    {
        _controls = new Controls();
    }


    // ENABLE CONTROLS SYSTEM PLAYER
    private void OnEnable()
    {
        _controls.Player.Move.performed += StartMove;
        _controls.Player.Move.canceled += StopMove;
        _controls.Player.Jump.performed += StartJump;
        _controls.Player.Jump.canceled += StopJump;
        _controls.Player.Enable();

    }

    // METHODS FOR CONTROL

    //MOVE
    private void StartMove(InputAction.CallbackContext context)
    {
        _valueX = context.ReadValue<float>();

        // _valueX = Mathf.RoundToInt(context.ReadValue<float>()); // This make movement input stick to 1
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _valueX = 0f;
    }
    //ACTION JUMP
    private void StartJump(InputAction.CallbackContext context)
    {
        IsJumping = true;
    }
    private void StopJump(InputAction.CallbackContext context)
    {
        IsJumping = false;
    }
    //DISABLE CONTROLS SYSTEM PLAYER
    private void OnDisable()
    {
        _controls.Player.Move.performed -= StartMove;
        _controls.Player.Move.canceled -= StopMove;
        _controls.Player.Jump.performed -= StartJump;
        _controls.Player.Jump.canceled -= StopJump;
        _controls.Player.Disable();
    }
}
