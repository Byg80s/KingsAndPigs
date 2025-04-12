using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Transform m_transform;
    private Animator m_animator;


    //Values
    [Header("Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;   
    [SerializeField] private float rayLength;
    [SerializeField] private int _extraJumps;
    [SerializeField] private int _counterExtraJumps;

    private int _direction = 1;
    private int _idSpeed;
    private int _idGround;
    [Header("Layers and Raycast distance detection")]
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform Lfoot, Rfoot;
    [SerializeField] private bool _isGrounded;
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ginput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
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


        Move();
        Jump();
        CheckGound();

    }


    // Animations Player
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
        m_animator.SetBool(_idGround, _isGrounded);
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
                m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, _jumpForce);

            if (_counterExtraJumps > 0)
            {

                m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, _jumpForce);
                _counterExtraJumps--;
            }

        }

        m_ginput.IsJumping = false;

    }

    private void CheckGound()
    {
        RaycastHit2D LfootRay = Physics2D.Raycast(Lfoot.position, Vector2.down, rayLength, GroundLayer);
        RaycastHit2D RfootRay = Physics2D.Raycast(Rfoot.position, Vector2.down, rayLength, GroundLayer);
        if (LfootRay || RfootRay)
        {
            _isGrounded = true;
            _counterExtraJumps = _extraJumps;
        }
        else
        {
            _isGrounded = false;
        }
    }


}
