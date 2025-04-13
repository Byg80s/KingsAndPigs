using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    //Components
    [SerializeField] private Transform m_transform;
    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Animator m_animator;


    //Values
    [Header("Parameters Movement")]
    [SerializeField] private float _speed;
    private int _direction = 1;

    [Header("Parameters Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _extraJumps;
    [SerializeField] private int _counterExtraJumps;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJumped;

    [Header("Parameters and Raycast ground detection")]

    [SerializeField] private bool wallDetected;
    [SerializeField] private float rayWall;

    // Raycast Variables
    RaycastHit2D LfootRay;
    RaycastHit2D RfootRay;

    // Id Animations
    private int _idSpeed;
    private int _idGround;
    private int _idFall;
    //Layers
    [Header("Layers")]
    [SerializeField] private LayerMask GroundLayer;


    //RayCast Ground
    [Header("parameters and Raycast ground detection")]
    [SerializeField] private Transform Lfoot;
    [SerializeField] private Transform Rfoot;
    [SerializeField] private float rayGround;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ginput = GetComponent<GatherInput>();
   //    m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();

    }

    //START
    void Start()
    {
      
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
        _idFall = Animator.StringToHash("_isWall");

        Lfoot = GameObject.Find("LFoot").GetComponent<Transform>();
        Rfoot = GameObject.Find("RFoot").GetComponent<Transform>();

        _counterExtraJumps = _extraJumps;


    }

    void Update()
    {
        Animations();

    }
    private void FixedUpdate()
    {

        CheckColision();
        Move();
        Jump();

    }


    // Animations Player
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
        m_animator.SetBool(_idGround, _isGrounded);
        m_animator.SetBool(_idFall, wallDetected);
    }

    //Check Ground
    private void CheckColision()
    {
        HandleWall();
        HandleGround();

    }

    private void HandleGround()
    {
        LfootRay = Physics2D.Raycast(Lfoot.position, Vector2.down, rayGround, GroundLayer);
        RfootRay = Physics2D.Raycast(Rfoot.position, Vector2.down, rayGround, GroundLayer);
        if (LfootRay || RfootRay)
        {
            _isGrounded = true;
            _counterExtraJumps = _extraJumps;
            _canDoubleJumped = false;
        }
        else
        {
            _isGrounded = false;
        }
    }
    private void HandleWall()
    {
        wallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * _direction, rayWall, GroundLayer);
    }

    //Movement directional

    private void Move()
    {
        Flip();
        m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, m_rb.linearVelocity.y);

    }

    // Flip Player
    private void Flip()
    {
        if (m_ginput.ValueX * _direction < 0)
        {

            m_transform.localScale = new Vector2(-m_transform.localScale.x, m_transform.localScale.y);
            _direction *= -1;
        }

    }
    //Jump
    private void Jump()
    {
        if (m_ginput.IsJumping)
        {
            if (_isGrounded)
            {

                m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, _jumpForce);
                _canDoubleJumped = true;
            }

            else if (_counterExtraJumps > 0 && _canDoubleJumped)
            {

                m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, _jumpForce);
                if (_canDoubleJumped)
                    _counterExtraJumps--;

            }



        }

        m_ginput.IsJumping = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_transform.position, new Vector2(m_transform.position.x + (rayWall * _direction), m_transform.position.y));
    }




}
