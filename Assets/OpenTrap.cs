using System.Collections;
using UnityEngine;

public class OpenTrap : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [Header("Components Open Ground")]
    [SerializeField] private GameObject m_GroundTrap;
    [SerializeField] private Animator m_animGround;
    [SerializeField] private SpriteRenderer _spGround;

    [Header("Portal")]

    [SerializeField] private GameObject _Portal;
    [SerializeField] private Animator _animPortal;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private int _idClosePortal;


    //Variables

    [Header("Parameters")]
    [SerializeField] private float _timeForOpen;
    [SerializeField] private bool _openTheTrap;
    private int _idOpenTrap;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_GroundTrap = GameObject.FindGameObjectWithTag("FalseGround");
        m_animGround = m_GroundTrap.GetComponent<Animator>();
        _idOpenTrap = Animator.StringToHash("_isOpen");
        _spGround = GetComponent<SpriteRenderer>();

        _Portal = GameObject.FindGameObjectWithTag("Portal");
       _animPortal = _Portal.GetComponent<Animator>();
        _idClosePortal = Animator.StringToHash("_isTake");


        _gameManager =GameManager.instance;


    }

    // Update is called once per frame
    void Update()
    {
        Animations();
    }
    private void Animations()
    {
        m_animGround.SetBool(_idOpenTrap, _openTheTrap);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor(_timeForOpen));
            ActivatePortal();

        }


    }
    IEnumerator OpenDoor(float time)
    {
        _openTheTrap = false;
        yield return new WaitForSeconds(time);
        {
            _openTheTrap = true;
            _spGround.enabled = false;
        }
    }
    private void ActivatePortal()
    {
        _animPortal.SetTrigger(_idClosePortal);
    }


}
