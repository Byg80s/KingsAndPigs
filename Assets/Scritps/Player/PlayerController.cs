using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Parameters Wall detection")]

    [SerializeField] private bool _wallDetected;
    [SerializeField] private bool _canWallDesliced;
    [SerializeField] private float _rayWall;
    [SerializeField] private float _speedDeslice;
    [SerializeField] private Vector2 _wallJumpForce;
    [SerializeField] private bool _isWallJumping;
    [SerializeField] private float _wallTimeDetection;



    // Raycast Variables
    RaycastHit2D LfootRay;
    RaycastHit2D RfootRay;

    // Id Animations
    private int _idSpeed;
    private int _idGround;
    private int _idFall;
    private int _idKnock;


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
    [SerializeField] private LayerMask GroundLayer;


    //Other experimental variables
    [SerializeField] private bool _isPotion;

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
        _idKnock = Animator.StringToHash("_knockback");

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
        if (_isNocked) return;
        CheckColision();
        Move();
        Jump();

    }


    // Animations Player
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
        m_animator.SetBool(_idGround, _isGrounded);
        m_animator.SetBool(_idFall, _wallDetected);
    }

    //Check Ground
    private void CheckColision()
    {



        //NEW MECHANICS, WHEN PLAYER TAKE A ESPECIAL OBJECT IS POSIBLE ACTIVATE HANDLEWALL AND DESLICE
        HandleWall();
        HandleWallDeslice();
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
    private void HandleWallDeslice()
    {
        _canWallDesliced = _wallDetected;


        if (!_canWallDesliced) return;
        _canDoubleJumped = false;

        _speedDeslice = m_ginput.Value.y < 0 ? 1 : 0.5f;
        m_rb.linearVelocity = new Vector2(m_rb.linearVelocityX, m_rb.linearVelocityY * _speedDeslice);

    }
    private void HandleWall()
    {
        _wallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * _direction, _rayWall, GroundLayer);
    }

    //Movement directional

    private void Move()
    {

        if (_wallDetected && !_isGrounded) return;
        if (_isWallJumping) return;
        Flip();
        m_rb.linearVelocity = new Vector2(_speed * m_ginput.Value.x, m_rb.linearVelocity.y);






    }

    // Flip Player
    private void Flip()
    {
        if (m_ginput.Value.x * _direction < 0)
        {
            HandleDirection();
        }

    }

    private void HandleDirection()
    {
        m_transform.localScale = new Vector2(-m_transform.localScale.x, m_transform.localScale.y);
        _direction *= -1;
    }

    //Jump
    private void Jump()
    {
        if (m_ginput.IsJumping)
        {
            if (_isGrounded)
            {

                m_rb.linearVelocity = new Vector2(_speed * m_ginput.Value.x, _jumpForce);
                _canDoubleJumped = true;
            }
            else if (_wallDetected) WallJump();

            else if (_counterExtraJumps > 0 && _canDoubleJumped) DoubleJump();





        }

        m_ginput.IsJumping = false;

    }
    private void WallJump()
    {
        _isWallJumping = true;
        m_rb.linearVelocity = new Vector2(_wallJumpForce.x * -_direction, _wallJumpForce.y);
        HandleDirection();
        StartCoroutine(WaitReturnTime(_wallTimeDetection));
    }

    private void DoubleJump()
    {

        m_rb.linearVelocity = new Vector2(_speed * m_ginput.Value.x, _jumpForce);
        if (_canDoubleJumped)
            _counterExtraJumps--;
    }
    public void KnockBack()
    {
        StartCoroutine(WaitKnock(_knockDuration));
        m_rb.linearVelocity = new Vector2(_KnockForce.x * -_direction, _KnockForce.y);
        m_animator.SetTrigger(_idKnock);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_transform.position, new Vector2(m_transform.position.x + (_rayWall * _direction), m_transform.position.y));
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
        _isNocked=false;
        _isCanNocked = true;
    }


}
