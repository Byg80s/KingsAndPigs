using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [Header("Components")]
    //Components
    private Rigidbody2D m_rb;
    private Animator m_animator;
    private GameObject playerFollow;
    [SerializeField] private Collider2D[] m_colliderChildren;
    private GameObject _PlayerPosition;
    private Transform m_PlayerTransform;
    private Collider2D _PlayerDetected;




    //Values
    [Header("Parameters Movement")]
    [SerializeField] private float _speed;
    private int _direction = 1;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private int waitTiemAtack;
    [SerializeField] private bool movementFoPhysics;

    [Header("Parameters Player Detection")]
    [Tooltip("This parameter is only for check the enemy follow the player")]
    [SerializeField] private bool checkPlayerDetection;
    [SerializeField] private bool _isAtacking;
    [SerializeField] private float _distanceChangeFollowPlayer;
    [Tooltip("Is radius range for detect the player")]
    [SerializeField] private float _radiusDetectPlayer;
    [Tooltip("Is radius range for enemy atacks")]
    [SerializeField] private float _radiusAtackPlayer;
    [Tooltip("Is de distance to detect the Waiponts")]
    [SerializeField] private float _distanceChange;
    [Header("Parameters Push Action")]

    [Header("Parameters Wall detection")]

    [Header("Select Enemy type")]
    [SerializeField] private EnemiesTypes TypeEnemie;
    [SerializeField] private EnemyEstates EnemyStates;


    [Header("Live system")]
    [SerializeField] private int _actualLife;
    [SerializeField] private int _currentLife;

    // Raycast Variables
    RaycastHit2D LfootRay;
    RaycastHit2D RfootRay;

    // Id Animations
    private int _idSpeed;
    private int _idGround;
    private int _idFall;
    private int _idKnock;
    private int _idPsuh;
    private int _idAtack;


    //RayCast Ground
    [Tooltip("This are the gameobjects for check distance of ground")]
    [Header("parameters ground detection")]
    [SerializeField] private Transform Lfoot;
    [SerializeField] private Transform Rfoot;
    [SerializeField] private float RayGround;
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


    }

    //Comportament Enemie
    [Header("Parameters WayPoints")]
    [SerializeField] private float _speedMove;
    [Tooltip("If active this variable, the enemy ignore de MoveTowars to waypoints")]
    [SerializeField] private bool _ignoreWayPoints;
    [Tooltip("WayPoints for MoveTowars")]
    [SerializeField] private Transform[] m_Way;
    public int _index;
    private bool _flip;





    //START
    void Start()
    {
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
        _idFall = Animator.StringToHash("_isWall");
        _idKnock = Animator.StringToHash("_knockback");
        _idPsuh = Animator.StringToHash("_isPush");
        _idAtack = Animator.StringToHash("_isAtack");
        _index = 0;
        m_colliderChildren[0].enabled = false;
        m_colliderChildren[1].enabled = true;
    }

    void Update()
    {
        Animations();
        EnemiIaNoPhysics();
    }
    private void FixedUpdate()
    {

        if (_isNocked) return;
        ChangeRigidBodyType();
        CheckColision();
        if (m_PlayerTransform != null) EnemiIaWithPhysic();
        //  KnockBack();
    }
    void ChangeRigidBodyType()
    {
        if (movementFoPhysics)
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            m_rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Animations Player
    private void Animations()
    {
        m_animator.SetBool(_idGround, _isGrounded);
    }

    //Check Ground
    private void CheckColision()
    {
        HandleGround();
        DetectedPlayer();
    }
    private void HandleGround()
    {
        LfootRay = Physics2D.Raycast(Lfoot.position, Vector2.down, RayGround, GroundLayer);
        RfootRay = Physics2D.Raycast(Rfoot.position, Vector2.down, RayGround, GroundLayer);

        if (LfootRay || RfootRay)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    void DetectedPlayer()
    {
        if (_PlayerPosition != null)
        {

            _isAtacking = Physics2D.OverlapCircle(transform.position, _radiusAtackPlayer, PlayerLayer);

            _PlayerDetected = Physics2D.OverlapCircle(transform.position, _radiusDetectPlayer, PlayerLayer);
            if (_PlayerDetected)
            {
                checkPlayerDetection = true;
                //     Debug.Log("Is detected");
            }
            else
            {
                checkPlayerDetection = false;
                //   Debug.Log("No detcted");
            }
        }

        _PlayerPosition = GameObject.FindGameObjectWithTag("Player");
        if (_PlayerPosition != null)
        {
            m_PlayerTransform = _PlayerPosition.transform;
        }
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
        m_animator.SetTrigger(_idKnock);
    }

    void EnemiIaWithPhysic()
    {

        if (!checkPlayerDetection && movementFoPhysics)
        {
            WayPointsUsePhysics();

        }
        if (checkPlayerDetection && movementFoPhysics)
        {
            FollowPlayerWithPhysics();
        }

    }
    void EnemiIaNoPhysics()
    {

        if (!checkPlayerDetection && !movementFoPhysics) WayPointsNoUsePhysics();

        if (checkPlayerDetection) FollowPlayerNoPhysics();


        Atack();
    }

    void Atack()
    {
        if (_isAtacking)
        {
            m_colliderChildren[0].enabled = true;
            m_animator.SetTrigger(_idAtack);
        }
        else
        {
            m_colliderChildren[0].enabled = false;
        }

    }

    void FollowPlayerNoPhysics
        ()
    {
        if (m_PlayerTransform != null)
            if ((m_PlayerTransform.position.x > transform.position.x && _flip) || (m_PlayerTransform.position.x < transform.position.x && !_flip))// add && _flip
            {

                Flip();

            }
            else
            {
                //   movementFoPhysics = true;
            }

        switch (TypeEnemie)
        {
            case EnemiesTypes.Melee:
                if (_PlayerDetected && _isGrounded)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_PlayerTransform.position.x, transform.position.y), _speedMove * Time.deltaTime);
                break;
            case EnemiesTypes.Ranged:
                if (_PlayerDetected)

                    transform.position = Vector2.MoveTowards(transform.position, m_PlayerTransform.position, _speedMove * Time.deltaTime);
                break;
            case EnemiesTypes.Flying:
                break;
            case EnemiesTypes.Stealth:
                break;
            case EnemiesTypes.Boss:
                break;
            default:
                break;
        }

    }
    void WayPointsUsePhysics()
    {
        if (m_Way.Length == 0) return;

        Vector3 WayPonits = m_Way[_index].position;
        Vector3 direction = (WayPonits - transform.position);

        if (direction.magnitude < _distanceChange)
        {
            Flip();
            _index = (_index + 1) % m_Way.Length;
            WayPonits = m_Way[_index].position;
            direction = (WayPonits - transform.position);
        }

        // Physic movement
        Vector3 VelocityNeed = direction.normalized * _speedMove;
        m_rb.linearVelocity = new Vector3(VelocityNeed.x, m_rb.linearVelocity.y); // conserva velocidad vertical si usas gravedad
    }
    void FollowPlayerWithPhysics()
    {

        Vector3 PlayerPos = m_PlayerTransform.position;
        Vector3 DirectionFollow = (PlayerPos - transform.position);

        if (DirectionFollow.magnitude < _distanceChangeFollowPlayer)
        {
            Flip();
            PlayerPos = m_Way[_index].position;
            DirectionFollow = (PlayerPos - transform.position);
        }

        // Physic movement
        Vector3 VelocityNeedForGoPlayer = DirectionFollow.normalized * _speedMove*Time.fixedDeltaTime;
        m_rb.linearVelocity = new Vector3(VelocityNeedForGoPlayer.x, m_rb.linearVelocity.y);

    }
    void WayPointsNoUsePhysics()
    {
        //no physics
        if (m_Way.Length == 0) return;
        transform.position = Vector2.MoveTowards(transform.position, m_Way[_index].transform.position, _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, m_Way[_index].transform.position) < 0.01f)
        {

            Flip();


            _index += 1 % m_Way.Length;
        }
        if (_index >= m_Way.Length)
        {
            _index = 0;
        }




    }
    //IEnumerators

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
        Gizmos.DrawWireSphere(transform.position, _radiusDetectPlayer);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusAtackPlayer);

        Gizmos.color = new Color(1f, 0f, 0, 1f);
        Gizmos.DrawCube(m_colliderChildren[0].bounds.center, m_colliderChildren[0].bounds.size);
        Gizmos.color = new Color(0f, 1f, 0, 1f);
        Gizmos.DrawCube(m_colliderChildren[1].bounds.center, m_colliderChildren[1].bounds.size);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {

            //  m_colliderChildren[1].isTrigger = true;
            Debug.Log("Is damage recibe");
            KnockBack();
            _actualLife--;
            _actualLife -= _currentLife;
            //  _currentLife += _actualLife;
            if (_actualLife <= 0)
                Destroy(gameObject);

        }


        //This is ok
        if (collision.CompareTag("HurtBox"))
        {



            m_colliderChildren[0].isTrigger = true;
            Debug.Log("done damage");

        }
        if (m_colliderChildren[0].isTrigger)
        {
            //   collision.GetComponentInParent<PlayerController>().KnockBack();

        }

    }
}




