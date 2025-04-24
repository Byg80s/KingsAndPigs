using UnityEngine;

public class SwitchActions : MonoBehaviour
{
    [SerializeField] private bool ActivateTimeDiscount;
    [SerializeField] private bool _isActivated = false;
    // Fault add a sound [SerializeField]
    public bool IsActivated { get => _isActivated; set => _isActivated = value; }
    [SerializeField] private bool _AnimationWork;
    [Tooltip("Name of Animator set")]
    [SerializeField] private string nameAnimator;
    [Tooltip("The name Object needed for activate")]
    [SerializeField] private string nameCollider;

    [Tooltip("Need the door for open")]
    [SerializeField] private UnlockZoneSystem door;
    private Animator m_anim;


    //This is for future options time

    private int numberDesactivation;
    private float timeDesactivate, newTime;
    private int _idSwitchOn;

    //need animator


    private void Start()
    {


        newTime = timeDesactivate;
        m_anim = GetComponent<Animator>();
        _idSwitchOn = Animator.StringToHash(nameAnimator);//"_isActived");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameCollider) && !_isActivated)//"HitBox"
        {
            _isActivated = true;
            AviableAnimation();
            door.CheckSwitches();
            Debug.Log("Activate: " + tag);
            // optional: animation, sound, etc.

        }



    }
    void AviableAnimation()
    {
        if ((_AnimationWork))
        {
            m_anim.SetBool(_idSwitchOn, _isActivated);
        }
    }

}
