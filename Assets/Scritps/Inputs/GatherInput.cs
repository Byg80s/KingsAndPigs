using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    //Variables


    private Controls _controls;


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
        _controls.Player.Enable();
    }

    // METHODS FOR CONTROL

    private void StartMove(InputAction.CallbackContext context)
    {      
        _valueX=context.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _valueX = 0f;
    }
    //DISABLE CONTROLS SYSTEM PLAYER
    private void OnDisable()
    {
        _controls.Player.Move.performed -= StartMove;
        _controls.Player.Move.canceled -= StopMove;
        _controls.Player.Disable();
    }
}
