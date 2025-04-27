using Unity.Cinemachine;
using UnityEngine;
using System.Collections;


public class SwitchActions : MonoBehaviour
{
    [Header("Cinemachine Components")]
    [SerializeField] private bool _activePointOfView = false;
    [SerializeField] private CinemachineCamera _cineMachineCamera;
    [SerializeField] private CinemachinePositionComposer _Composer;
    [Tooltip("Drop the GameObject activate the event")]
    [SerializeField] private GameObject _zoneActivateCamPos;
    [SerializeField] private float _TargetOffSetY;
    [SerializeField] private float _TargetOffSetX;

    [Header("Parameter switch")]
    [Tooltip("Active this, if you need the camera view the object")]
    [SerializeField] private bool _activeCam = false;
    //  [SerializeField] private bool ActivateTimeDiscount;
    [SerializeField] private bool _isActivated = false;
    [SerializeField] private bool _FlipAnimation = false;
    public bool IsActivated { get => _isActivated; set => _isActivated = value; }
    [SerializeField] private bool _AnimationWork;
    [Tooltip("Name of Animator set")]
    [SerializeField] private string nameAnimator;
    [Tooltip("The name Object needed for activate")]
    [SerializeField] private string nameCollider;

    [Tooltip("Need the door for open")]
    [SerializeField] private UnlockZoneSystem door;

    private Animator m_anim;
    // Fault add a sound [SerializeField]

    //This is for future options time

    private int numberDesactivation;
    private float timeDesactivate, newTime;
    private int _idSwitchOn;

    //need animator


    private void Start()
    {

        newTime = timeDesactivate;
        m_anim = GetComponent<Animator>();
        _idSwitchOn = Animator.StringToHash(nameAnimator);

    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameCollider) && !_isActivated)
        {

            _isActivated = true;
            AviableAnimation();
            door.CheckSwitches();
            Debug.Log("_isActivated: " + tag);
            // optional: animation, sound, etc.
            _activePointOfView = true;

            if (!_activeCam) return; StartCoroutine(TimeOfGoView(1.5f));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(nameCollider) && _isActivated)
        {
            _isActivated = false;
            Debug.Log("Desactivate: " + _isActivated);
        }
    }
    private void AviableAnimation()
    {
        if ((_AnimationWork))
        {
            m_anim.SetBool(_idSwitchOn, _isActivated);
        }
    }
    private void EnableCamera()
    {
        if (_activePointOfView && _Composer != null)
        {
            _Composer.TargetOffset.y = _TargetOffSetX;
            _Composer.TargetOffset.x = _TargetOffSetY;
            StartCoroutine(TimeOfCameraActivated(2));
        }
    }
    IEnumerator TimeOfCameraActivated(float time)
    {
        yield return new WaitForSeconds(time);
        _Composer.TargetOffset.y = 0f;
        _Composer.TargetOffset.x = 0f;
        Destroy(_zoneActivateCamPos);

    }
    IEnumerator TimeOfGoView(float time)
    {
        yield return new WaitForSeconds(time);
        EnableCamera();
    }

}
