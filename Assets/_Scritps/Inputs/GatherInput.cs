using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GatherInput : MonoBehaviour
{
    //Variables


    private Controls _controls;
    private PlayerController _playerController;

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

    [SerializeField] private bool _IsTake;
    public bool IsTake { get => _IsTake; set => _IsTake = value; }

    [SerializeField] private bool _isUp;
    public bool UpPt { get => _isUp; set => _isUp = value; }

    [SerializeField] private bool _isDown;
    public bool DownPt { get => _isDown; set => _isDown = value; }


    private float _coolDown = 0.3f;
    private float _timeAttackReady = 0;

    //Components
    private Animator _animator;



    // FIRST CALL
    private void Awake()
    {
        _controls = new Controls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        _timeAttackReady += Time.deltaTime;

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
        _controls.Player.Take.performed += StartTake;
        _controls.Player.Take.canceled += StopTake;
        _controls.Player.DownPlattform.performed += StartDownPlatfform;
        _controls.Player.DownPlattform.canceled += StopDownPlatfform;
        _controls.Player.UpPlattform.performed += StartUpPlatfform;
        _controls.Player.UpPlattform.canceled += StopUpPlatfform;


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
        CoolDownSystem();
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
    private void StartTake(InputAction.CallbackContext context)
    {
        IsTake = true;
    }
    private void StopTake(InputAction.CallbackContext context)
    {
        IsTake = false;
    }
    private void StartUpPlatfform(InputAction.CallbackContext context)
    {
        UpPt = true;
    }
    private void StopUpPlatfform(InputAction.CallbackContext context)
    {
        UpPt = false;
    }
    private void StartDownPlatfform(InputAction.CallbackContext context)
    {
        DownPt = true;
    }
    private void StopDownPlatfform(InputAction.CallbackContext context)
    {
        DownPt = false;
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
        _controls.Player.Take.performed -= StartTake;
        _controls.Player.Take.canceled -= StopTake;
        _controls.Player.DownPlattform.performed -= StartDownPlatfform;
        _controls.Player.DownPlattform.canceled -= StopDownPlatfform;
        _controls.Player.UpPlattform.performed -= StartUpPlatfform;
        _controls.Player.UpPlattform.canceled -= StopUpPlatfform;
        _controls.Player.Disable();
    }
    private void CoolDownSystem()
    {
        if (_timeAttackReady >= _coolDown)
        {
            _animator.SetTrigger("_isAttack");
            AudioManager.instance.Play("Attack");
            _timeAttackReady = 0;
        }
    }

}
