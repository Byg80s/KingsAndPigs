using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Components")]
    //Components
    [SerializeField] private Transform m_transform;
    [SerializeField] private Collider2D[] m_colliderChildren;

    //Detectbox
    [Header("Parameters Detect boxes")]
    //    private Collider2D[] m_DetectBox;
    private DistanceJoint2D m_joint;
    [Tooltip("Select Layer box")]
    [SerializeField] private LayerMask _layerPull;
    [Tooltip("Distance detect a box")]
    [SerializeField] private float distanceToBox;
    [Tooltip("Force need for move the box")]
    [SerializeField] private float _pushForce;
    private GameObject heldObject;
    private Rigidbody2D heldRb;
    private bool _noIsNull;

    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Animator m_animator;

    [Tooltip("Life Player")]
    [SerializeField] private int _currentLife;
    public int CurrentLife { get => _currentLife; set => _currentLife = value; }
    [Tooltip("Max Lifes Of Player")]
    [SerializeField] private int _maxLife;
    public int maxLife { get => _maxLife; set => _maxLife = value; }

    [SerializeField] private int _actualLife;
    public int actualLife { get => _actualLife; set => _actualLife = value; }

    //Values
    [Header("Parameters Movement")]

    [SerializeField] private float _speed;
    [Tooltip("Define the speed run player")]
    [SerializeField] private float _normalSpeed;
    [Tooltip("block or unlook the movement of player")]
    [SerializeField] private bool _canMove;
    public bool canMove { get => _canMove; set => _canMove = value; }

    [Tooltip("Time of delay of block movement")]
    [SerializeField] private float _moveDelay;
    private int _direction = 1;

    [Header("Parameters Jump")]
    [Tooltip("Force of Jump Player")]
    [SerializeField] private float _jumpForce;
    [Tooltip("Number of jumps extra")]
    [SerializeField] private int _extraJumps;
    [SerializeField] private int _counterExtraJumps;

    //chech this
    [SerializeField] private bool _isGrounded;
    public   bool IsGrounded { get => _isGrounded; }
    [Tooltip("Permit doubleJump")]
    [SerializeField] private bool _canDoubleJumped;
    private bool _permitJumper;

    [Header("Parameters Actions Push And Pull")]
    [SerializeField] private bool _isPushed;
    public bool IsPushed { get => _isPushed; set => _isPushed = value; }

    [SerializeField] private bool _isPull;
    [Tooltip("This paramemeter is for define de radius for detect the objects posible take")]
    [SerializeField] private float _radiusDetectPull;

    [Header("Parameters Wall detection")]
    [SerializeField] private bool _wallDetected;
    [SerializeField] private bool _canWallDesliced;
    [SerializeField] private float _rayWall;
    [SerializeField] private float _speedDeslice;
    [SerializeField] private Vector2 _wallJumpForce;
    [SerializeField] private bool _isWallJumping;
    [SerializeField] private float _wallTimeDetection;
    [SerializeField] private bool _isMove;




    // Raycast Variables
    RaycastHit2D LfootRay;
    RaycastHit2D RfootRay;

    // Id Animations
    private int _idSpeed;
    private int _idGround;
    private int _idFall;
    private int _idKnock;
    private int _idPsuh;
    private int _idAttack;
    private int _idSwitch;


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
    [Header("Dead VFX")]
    [SerializeField] private GameObject DeathVfx;
    [Header("Player Inside Door VFX")]
    [SerializeField] private GameObject IndoorVfx;

    private bool attack;


    private void Awake()
    {

        m_rb = GetComponent<Rigidbody2D>();
        m_ginput = GetComponent<GatherInput>();
        m_animator = GetComponent<Animator>();
        _canMove = false;
        StartCoroutine(BlockMovement(_moveDelay));


    }

    //START
    void Start()
    {
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
        _idFall = Animator.StringToHash("_isWall");
        _idKnock = Animator.StringToHash("_knockback");
        _idPsuh = Animator.StringToHash("_isPush");
        _idAttack = Animator.StringToHash("_isAttack");
        _idSwitch = Animator.StringToHash("_isPushButton");

        m_colliderChildren[0].enabled = false;
        m_colliderChildren[1].enabled = false;

        _counterExtraJumps = _extraJumps;
        _permitJumper = true;
        m_joint = GetComponent<DistanceJoint2D>();
        m_joint.enabled = false;
        //  m_DetectBox = GetComponent<Collider2D[]>();
        heldObject = GetComponent<GameObject>();
        heldObject = GameObject.FindGameObjectWithTag("Box");
        heldRb = heldObject.GetComponentInParent<Rigidbody2D>();

    }


    void Update()
    {
        Animations();
        DamageNeedDead();

    }
    private void FixedUpdate()
    {
        if (!_canMove) return;
        if (_isNocked) return;
        CheckColision();
        //Block Player Movement
        BlockInputs();
        Move();
        if (!_isPushed) Jump();
    }


    // Animations Player
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
        m_animator.SetBool(_idGround, _isGrounded);
        m_animator.SetBool(_idFall, _wallDetected);
        m_animator.SetBool(_idPsuh, _isPushed);
        Attack();

    }

    //Check Ground
    private void CheckColision()
    {
        HandleWall();
        HandleWallDeslice();
        HandleGround();
        HandleObjects();
        if (_isGrounded) PushAndPullObjects();

    }


    //Detected ground and configure double jump
    private void HandleGround()
    {
        LfootRay = Physics2D.Raycast(Lfoot.position, Vector2.down, rayGround, GroundLayer);
        RfootRay = Physics2D.Raycast(Rfoot.position, Vector2.down, rayGround, GroundLayer);

        if (LfootRay || RfootRay)
        {
            _isGrounded = true;
            _counterExtraJumps = _extraJumps;
            _canDoubleJumped = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    #region WallDetect 
    //If wall is detected is posible jump in wall and deslice slowly
    private void HandleWallDeslice()
    {
        _canWallDesliced = _wallDetected;
        if (!_canWallDesliced) return;
        _canDoubleJumped = false;
        _speedDeslice = m_ginput.Value.y < 0 ? 1 : 0.5f;
        m_rb.linearVelocity = new Vector2(m_rb.linearVelocityX, m_rb.linearVelocityY * _speedDeslice);
    }
    // If press Z button Pad detecte the wall
    private void HandleWall()
    {
        if (m_ginput.IsWall)
            _wallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * _direction, _rayWall, GroundLayer);
        else _wallDetected = false;
    }
    #endregion
    //Movement directional

    private void Move()
    {
        if (!_canMove) return;
        if (_wallDetected && !_isGrounded) return;
        if (_isWallJumping) return;
        Flip();
        m_rb.linearVelocity = new Vector2(_speed * m_ginput.Value.x, m_rb.linearVelocity.y);
    }

    // Flip Player
    private void Flip()
    {
        if (m_ginput.Value.x * _direction < 0)//&& !GameManager.instance.blockInputs)
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
        if (m_ginput.IsJumping && _permitJumper)
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
    public void Attack()
    {

        AnimatorStateInfo state = m_animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Attack"))
            attack = true;
        else
            attack = false;
        if (attack)
        {
            m_colliderChildren[1].enabled = true;
        }
        else
        {
            m_colliderChildren[1].enabled = false;
        }

    }
    /// <summary>Pull Object </summary>
    private void HandleObjects()
    {
        /* 
       if (m_ginput.IsTake)
       {
           RaycastHit2D HitTake = Physics2D.Raycast(transform.position, transform.localScale, distanceToBox, _layerPull);
           if (HitTake.collider != null)
           {

               if (heldObject.CompareTag("Box"))
               {
                   Debug.DrawLine(transform.position, heldObject.transform.position, Color.red);
                   heldRb.bodyType = RigidbodyType2D.Dynamic;
                   heldRb.AddForce(transform.position * _pushForce);
               }
           }


       }

       /*      if (m_ginput.IsTake)
           {
               m_DetectBox = Physics2D.OverlapCircleAll(transform.position, _radiusDetectPull);
               foreach (var detectForPull in m_DetectBox)
               {
                   if (detectForPull.CompareTag("Box"))
                   {
                       m_joint.connectedBody = detectForPull.attachedRigidbody;
                       m_joint.enabled = true;
                       _isPull = true;
                       break;
                   }
               }
           }

           else
           {
               m_joint.enabled = false;
               _isPull = false;
           }
       */

    }


    private void PushAndPullObjects()
    {


        if (m_ginput.Push)
        {
            _isPushed = true;
            /*   RaycastHit2D HitPush = Physics2D.Raycast(transform.position, transform.localScale, distanceToBox, _layerPull);

               if (HitPush.collider != null)
               {
                   _noIsNull = true;
                   if (heldObject.CompareTag("Box"))
                   {
                       Debug.DrawLine(transform.position, heldObject.transform.position, Color.blue);
                       heldRb.bodyType = RigidbodyType2D.Dynamic;
                       heldRb.AddForce(transform.position * _pushForce);
                   }
            */
        }
        else
        {
           _isPushed = false;
            //heldRb.bodyType = RigidbodyType2D.Static;
        }

        #region OLD PUSH METHOD
        /*  if (m_ginput.Push)

          {
              _isPushed = true;

              m_colliderChildren[0].enabled = true;
              m_colliderChildren[0].isTrigger = false;
              GameManager.instance.IsPushAction = true;
          }
          else
          {
              _isPushed = false;
              m_colliderChildren[0].enabled = false;
              m_colliderChildren[0].isTrigger = true;
              GameManager.instance.IsPushAction = false;
          }
        */

        #endregion
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
        // _isCanNocked = false;
        yield return new WaitForSeconds(time);
        _isNocked = false;
        //_isCanNocked = true;
    }
    IEnumerator BlockMovement(float time)
    {
        yield return new WaitForSeconds(time);
        _canMove = true;
    }
    public void Died()
    {

        GameObject DeathVfxPrefab = Instantiate(DeathVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
    public void ExitLevel()
    {
        GameObject InDoorVfxPrefab = Instantiate(IndoorVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void BlockInputs()
    {
        if (GameManager.instance.blockInputs && _isGrounded)
        {
            m_rb.linearVelocity = Vector2.zero;
            m_rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
        }
        /////////////////////// check this /////////////////////
    }
    void ActualNumberOfLife()
    {
        if (CurrentLife <= 0) maxLife--;
    }
    private void DamageNeedDead()
    {
        if (CurrentLife <= 0)// GameManager.instance.CurrentLife <= 0)
        {

            GameManager.instance.ReSpawnPlayer();
            CurrentLife = actualLife;
            ActualNumberOfLife();
            Died();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0, 0.7f);
        Gizmos.DrawCube(m_colliderChildren[0].bounds.center, m_colliderChildren[0].bounds.size);
        Gizmos.color = new Color(0f, 1f, 0, 0.7f);
        Gizmos.DrawCube(m_colliderChildren[1].bounds.center, m_colliderChildren[1].bounds.size);
        Gizmos.color = new Color(1f, 0, 0, 0.7f);
        Gizmos.DrawCube(m_colliderChildren[2].bounds.center, m_colliderChildren[2].bounds.size);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Watter"))
        {
            _speed = 1f;
            _permitJumper = false; //POSIBLE IN FUTURE CHANGE THIS FOR DOWN JUMPFORCE
            //NEDD ADD ANIMATION SWINNING AND CONTER FOR DEAD IF MORE TIME IN WATER
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _speed = _normalSpeed;
        _permitJumper = true;
    }

}
