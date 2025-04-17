using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [Header("Components")]
    //Components
    [SerializeField] private Transform m_transform;
    private Transform m_PlayerTransform;
    [SerializeField] private Collider2D m_collider;
    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Animator m_animator;


    //Values
    [Header("Parameters Movement")]
    [SerializeField] private float _speed;
    private int _direction = 1;
    [SerializeField] private bool _isGrounded;


    [Header("Parameters Player Detection")]
    [SerializeField] private bool _PlayerDetected;
    [SerializeField] private float _playerRay;
    [SerializeField] private float _distance;
    [SerializeField] private bool _fly;
    [Header("Parameters Push Action")]
    [SerializeField] private bool _isPushed;
    [Header("Parameters Wall detection")]

    [SerializeField] private bool _wallDetected;

    [SerializeField] private bool _canWallDesliced;
    [SerializeField] private float _rayWall;
    [SerializeField] private float _speedDeslice;
    [SerializeField] private Vector2 _wallJumpForce;
    [SerializeField] private bool _isWallJumping;
    [SerializeField] private float _wallTimeDetection;
    [SerializeField] private bool _isMove;
    [SerializeField] private EnemiesTypes TypeEnemie;


    // Raycast Variables
    RaycastHit2D LfootRay;
    RaycastHit2D RfootRay;

    // Id Animations
    private int _idSpeed;
    private int _idGround;
    private int _idFall;
    private int _idKnock;
    private int _idPsuh;


    //RayCast Ground
    [Header("parameters ground detection")]
    [SerializeField] private Transform Lfoot;
    [SerializeField] private Transform Rfoot;

    [SerializeField] private float rayGround;
    //Knock Settings
    [Header("parameters Knock")]
    [SerializeField] private bool _isNocked;
    [SerializeField] private bool _isCanNocked;
    [SerializeField] private Vector2 _KnockForce;
    [SerializeField] private float _knockDuration;

    //Layers
    [Header("Layers")]
    [SerializeField] private LayerMask GroundLayer, PlayerLayer;
    private void Awake()
    {

        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_collider.GetComponentInChildren<Collider>();


    }

    //Comportament Enemie
    [Header("Parameters WayPoints")]
    [SerializeField] private bool _ignoreWayPoints;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Transform[] m_Way;

    public int _index = 0;
    [SerializeField] private int _speedMove;
    private bool _flip;
    private float distanceToPlayer;
    private void OnEnable()
    {
        m_PlayerTransform=GameObject.FindWithTag("Player").GetComponent<Transform>();

    }
    //START
    void Start()
    {
        _gameManager = GameManager.instance;
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
        _idFall = Animator.StringToHash("_isWall");
        _idKnock = Animator.StringToHash("_knockback");
        _idPsuh = Animator.StringToHash("_isPush");

    }

    void Update()
    {
        //    Animations();
    }
    private void FixedUpdate()
    {
        if (_isNocked) return;
        CheckColision();
        EnemiIA();
        Move();
    }


    // Animations Player
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
        m_animator.SetBool(_idGround, _isGrounded);
        m_animator.SetBool(_idFall, _wallDetected);
        m_animator.SetBool(_idPsuh, _isPushed);
    }

    //Check Ground
    private void CheckColision()
    {
        HandleGround();
        DetectedPlayer();
    }



    private void HandleGround()
    {
        LfootRay = Physics2D.Raycast(Lfoot.position, Vector2.down, rayGround, GroundLayer);
        RfootRay = Physics2D.Raycast(Rfoot.position, Vector2.down, rayGround, GroundLayer);

        if (LfootRay || RfootRay)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }




    //Movement directional

    private void Move()
    {

        if (_wallDetected && !_isGrounded) return;
        if (_isWallJumping) return;

        // Flip();
    }
    void DetectedPlayer()
    {

        _PlayerDetected = Physics2D.OverlapCircle(m_transform.position, _playerRay, PlayerLayer);

    }

    // Flip Enemy


    private void Flip()
    {
        _flip = !_flip;
        Vector2 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    //Jump
    public void KnockBack()
    {
        StartCoroutine(WaitKnock(_knockDuration));
        m_rb.linearVelocity = new Vector2(_KnockForce.x * -_direction, _KnockForce.y);
        m_animator.SetTrigger(_idKnock);

    }


    void EnemiIA()
    {
        if (TypeEnemie == 0)
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {



        if (_PlayerDetected)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_PlayerTransform.position.x, transform.position.y), _speedMove * Time.deltaTime);
       //     transform.position = Vector2.MoveTowards(transform.position, m_PlayerTransform.position, _speedMove * Time.deltaTime);

            if ((m_PlayerTransform.position.x > transform.position.x && !_flip) || (m_PlayerTransform.position.x < transform.position.x && _flip))
            {
                Flip();
            }
        }

        else
        {
            WatPointsMove();
        }

    }
    void WatPointsMove()
    {


        if (m_Way.Length == 0) return;


        transform.position = Vector2.MoveTowards(transform.position, m_Way[_index].transform.position, _speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, m_Way[_index].transform.position) < 0.01f)

        {

            _index += 1 % m_Way.Length;

        }
        if (_index >= m_Way.Length)
        {
            _index = 0;
        }
    }

    //IEnumerators
    IEnumerator WaitReturnTime(float time)
    {
        _isWallJumping = true;
        yield return new WaitForSeconds(time);
        _isWallJumping = false;
    }
    IEnumerator WaitKnock(float time)
    {
        _isNocked = true;
        _isCanNocked = false;
        yield return new WaitForSeconds(time);
        _isNocked = false;
        _isCanNocked = true;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _playerRay);

    }
}
