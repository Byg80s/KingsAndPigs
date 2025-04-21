using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    //Variables


    private Controls _controls;

    [SerializeField] private bool _isJumping;
    public bool IsJumping { get => _isJumping; set => _isJumping = value; }
    [SerializeField] private bool _push;
    public bool Push { get => _push; set => _push = value; }

    [SerializeField] private Vector2 _value;
    public Vector2 Value { get => _value; }

    [SerializeField] private bool _isAtack;
    public bool Atack { get => _isAtack; set => _isAtack = value; }

    [SerializeField] private bool _isWall;
    public bool IsWall { get => _isWall; set => _isWall = value; }



    //Components
    private Animator _animator;



    // FIRST CALL
    private void Awake()
    {
        _controls = new Controls();
        _animator = GetComponent<Animator>();
    }


    // ENABLE CONTROLS SYSTEM PLAYER
    private void OnEnable()
    {
        _controls.Player.Move.performed += StartMove;
        _controls.Player.Move.canceled += StopMove;
        _controls.Player.Jump.performed += StartJump;
        _controls.Player.Jump.canceled += StopJump;
        _controls.Player.Push.performed += StarPush;
        _controls.Player.Push.canceled += StopPush;
        _controls.Player.Atack.started += StartAttack;
        _controls.Player.Wall.performed += StartWallPos;
        _controls.Player.Wall.canceled += StopWallPos;

        _controls.Player.Enable();

    }

    // METHODS FOR CONTROL

    //MOVE
    private void StartMove(InputAction.CallbackContext context)
    {
        _value = context.ReadValue<Vector2>().normalized;

        // _valueX = Mathf.RoundToInt(context.ReadValue<float>()); // This make movement input stick to 1
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _value = Vector2.zero;
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
    //Action Push
    private void StarPush(InputAction.CallbackContext context)
    {
        Push = true;
    }
    private void StopPush(InputAction.CallbackContext context)
    {
        Push = false;
    }
    //Action Attack
    private void StartAttack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("_isAttack");
       // Atack =!Atack;
    }
    private void StoptAttack(InputAction.CallbackContext context)
    {
        Atack = false;
    }
    private void StartWallPos(InputAction.CallbackContext context)
    {
        IsWall = true;
    }
    private void StopWallPos(InputAction.CallbackContext context)
    {
        IsWall = false;
    }
    //DISABLE CONTROLS SYSTEM PLAYER
    private void OnDisable()
    {
        _controls.Player.Move.performed -= StartMove;
        _controls.Player.Move.canceled -= StopMove;
        _controls.Player.Jump.performed -= StartJump;
        _controls.Player.Jump.canceled -= StopJump;
        _controls.Player.Push.performed -= StarPush;
        _controls.Player.Push.canceled -= StopPush;
        _controls.Player.Atack.started -= StartAttack;
        _controls.Player.Wall.performed -= StartWallPos;
        _controls.Player.Wall.canceled -= StopWallPos;
        _controls.Player.Disable();
    }
}
