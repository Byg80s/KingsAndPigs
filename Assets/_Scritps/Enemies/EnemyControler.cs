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
    private bool _PlayerDetected;
    private bool _PlayerIsInFirstZone;
    private bool _PlayerIsInSecondZone;

    [Header("Pefabs drop")]
    [SerializeField] private GameObject PrefabBox;
    [SerializeField] private GameObject PrefabKey;
    [SerializeField] private GameObject PrefabPotionHeal;
    [SerializeField] private GameObject PrefabPotionMana;


    //Values
    [Header("Parameters Movement")]
    [SerializeField] private float _speed;
    private int _direction = 1;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private int waitTiemAtack;
    [SerializeField] private bool movementFoPhysics;
    [SerializeField] private bool _isWaitMovement;
    [SerializeField] private int timeWaitPatrol;
    [Header("Parameters Player Detection")]
    [Tooltip("This parameter is only for check the enemy follow the player")]
    [SerializeField] private bool checkPlayerDetection;
    [Tooltip("This parameter is only for check the enemy is atacking the player")]
    [SerializeField] private bool _isAtacking;
    [Tooltip("This parameter is for change distance change follow player")]
    [SerializeField] private float _distanceChangeFollowPlayer;
    [Tooltip("This parameter is only for Boos,you need chanche type enemy in the inspector, this use for change to range atack type 01")]
    [SerializeField] private float _radiusDetectPlayerForAtackRangeType01;
    [Tooltip("This parameter is only for Boos,you need chanche type enemy in the inspector, this use for change to range atack type 02")]
    [SerializeField] private float _radiusDetectPlayerForAtackRangeType02;

    [Tooltip("Is radius range for detect the player")]
    [SerializeField] private float _radiusDetectPlayer;
    [Tooltip("Is radius range for enemy atacks")]
    [SerializeField] private float _radiusAtackPlayer;
    [Tooltip("Is de distance to detect the Waiponts")]
    [SerializeField] private float _distanceChange;
    [Header("Parameters Push Action")]

    [Header("Parameters Wall detection")]

    [Header("Select Enemy type and Dropeds")]
    [SerializeField] private EnemiesTypes TypeEnemie;
    [SerializeField] private EnemyEstates EnemyStates;
    [SerializeField] private EnemiesTypeDrop TypeDrop;
    private bool _isIntancied = false;


    [Header("Live system")]
    [SerializeField] private int _actualLife;
    public int ActualLife { get => _actualLife; set => _actualLife = value; }

    [SerializeField] private int _currentLife;

    //only for check and need drop
    private bool _isDead;

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
    private int _idDead;
    private int _isMove;


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
    [SerializeField] private InstantiateBombs _makeBomb;


    //Layers
    [Header("Layers")]
    [SerializeField] private LayerMask GroundLayer, PlayerLayer;


    //Comportament Enemie
    [Header("Parameters WayPoints")]
    [SerializeField] private float _speedMove;
    [Tooltip("If active this variable, the enemy ignore de MoveTowars to waypoints")]
    [SerializeField] private bool _ignoreWayPoints;
    [Tooltip("WayPoints for MoveTowars")]
    [SerializeField] private Transform[] m_Way;
    public int _index;
    private bool _flip;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }



    //START
    void Start()
    {
        _makeBomb = FindAnyObjectByType<InstantiateBombs>();
        _idSpeed = Animator.StringToHash("_speed");
        _idGround = Animator.StringToHash("_isGround");
        _idFall = Animator.StringToHash("_isWall");
        _idKnock = Animator.StringToHash("_knockback");
        _idPsuh = Animator.StringToHash("_isPush");
        _idAtack = Animator.StringToHash("_isAtack");
        _idDead = Animator.StringToHash("_isDeath");
        _isMove = Animator.StringToHash("_move");
        _index = 0;
        m_colliderChildren[0].enabled = false;
        m_colliderChildren[1].enabled = true;
    }

    void Update()
    {
        Animations();
        if (_actualLife > 0) EnemiIaNoPhysics();
        Dead();
        DropItemIfDeath();

    }
    private void FixedUpdate()
    {

        if (_isNocked) return;
        ChangeRigidBodyType();
        CheckColision();
        StatesOfAnimationEnemy();
        if (m_PlayerTransform != null && _actualLife > 0) EnemiIaWithPhysic();// || _actualLife > 0

    }
    void Dead()
    {

        if (_actualLife <= 0) StartCoroutine(DeathDestroy());
    }
    IEnumerator DeathDestroy()
    {
        m_animator.SetBool(_idDead, true);
        m_colliderChildren[0].enabled = false;
        m_colliderChildren[1].enabled = false;
        m_rb.bodyType = RigidbodyType2D.Static;
        _isDead = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
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
            _PlayerIsInFirstZone = Physics2D.OverlapCircle(transform.position, _radiusDetectPlayerForAtackRangeType01, PlayerLayer);
            _PlayerIsInSecondZone = Physics2D.OverlapCircle(transform.position, _radiusDetectPlayerForAtackRangeType02, PlayerLayer);


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

    //KnockBack
    public void KnockBack()
    {
        StartCoroutine(WaitKnock(_knockDuration));
        m_rb.linearVelocity = new Vector2(_KnockForce.x * -_direction, _KnockForce.y);
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

    void FollowPlayerNoPhysics()
    {
        if (m_PlayerTransform != null)//|| _actualLife > 0)
        {


            if ((m_PlayerTransform.position.x > transform.position.x && _flip) || (m_PlayerTransform.position.x < transform.position.x && !_flip))// add && _flip
            {

                Flip();

            }


            switch (TypeEnemie)
            {
                case EnemiesTypes.Melee:
                    if (_PlayerDetected && _isGrounded && _actualLife > 0)
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_PlayerTransform.position.x, transform.position.y), _speedMove * Time.deltaTime);

                    break;
                case EnemiesTypes.Ranged:
                    if (_PlayerDetected && _actualLife > 0)

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

    }
    void WayPointsUsePhysics()
    {
        switch (TypeEnemie)
        {
            case EnemiesTypes.Melee:

                if (m_Way.Length == 0) return;

                if (_isWaitMovement) return;

                Vector3 target = m_Way[_index].position;
                Vector3 direction = target - transform.position;

                if (direction.magnitude < _distanceChange)
                {
                    StartCoroutine(TimeWaitAfterMove(timeWaitPatrol));
                    return;
                }

                Vector3 velocity = direction.normalized * _speedMove;
                m_rb.linearVelocity = new Vector2(velocity.x, m_rb.linearVelocity.y);

                if (velocity.x > 0 && _flip) Flip();
                else if (velocity.x < 0 && !_flip) Flip();

                break;
            case EnemiesTypes.Ranged:

                //this need check
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
    void StatesOfAnimationEnemy()
    {
        switch (EnemyStates)
        {
            case EnemyEstates.wait:
                m_animator.SetBool(_idGround, true);
                m_animator.SetBool(_isMove, false);
                break;
            case EnemyEstates.patrol:
                m_animator.SetBool(_idGround, false);
                m_animator.SetBool(_isMove, true);
                break;

        }
    }

    IEnumerator TimeWaitAfterMove(float time)
    {

        _isWaitMovement = true;
        EnemyStates = EnemyEstates.wait;
        m_rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(time);

        _index = (_index + 1) % m_Way.Length;
        _isWaitMovement = false;
        EnemyStates = EnemyEstates.patrol;
    }



    void FollowPlayerWithPhysics()
    {
        switch (TypeEnemie)
        {
            case EnemiesTypes.Melee:
                if (m_PlayerTransform != null) 
                {
                    Vector3 PlayerPos = m_PlayerTransform.position;
                    Vector2 DirectionFollow = (PlayerPos - transform.position);
                    #region OldCode
                    /*    if (DirectionFollow.magnitude < _distanceChangeFollowPlayer)//!_isNocked &&
                        {
                            Flip();
                            PlayerPos = m_Way[_index].position;
                            DirectionFollow = (PlayerPos - transform.position);
                        }
                    // Physic movement

                    // Vector3 VelocityNeedForGoPlayer = DirectionFollow.normalized * _speedMove * Time.fixedDeltaTime;
                    //  m_rb.linearVelocity = new Vector3(VelocityNeedForGoPlayer.x, m_rb.linearVelocity.y);
                    */
                    #endregion

                    m_rb.linearVelocity = new Vector2(DirectionFollow.x * _speedMove * Time.fixedDeltaTime,0).normalized;
                }
                break;
            case EnemiesTypes.Boss:

                if (m_PlayerTransform != null)
                {


                    float direction = Mathf.Sign(m_PlayerTransform.position.x - transform.position.x);//transform.position.normalized);
                    m_rb.linearVelocity = new Vector2(direction * _speedMove, m_rb.linearVelocity.y);



                    if (_PlayerIsInFirstZone)
                    {
                        _makeBomb.activeBombs = true;
                    }
                    else
                    {
                        _makeBomb.activeBombs = false;
                    }
                }
                break;
        }

    }


    void DropItemIfDeath()
    {


        switch (TypeDrop)
        {

            case EnemiesTypeDrop.Box:
                if (_actualLife <= 0 && PrefabBox != null)
                {
                    StartCoroutine(TimeDropBox(1));
                }

                break;


            case EnemiesTypeDrop.Key:
                if (_actualLife <= 0 && PrefabBox != null)
                {
                    StartCoroutine(TimeDropKey(1));
                }
                break;

            case EnemiesTypeDrop.PotionHealth:
                if (_actualLife <= 0 && PrefabBox != null)
                {
                    StartCoroutine(TimeDropPotionHeal(1));
                }
                break;

            case EnemiesTypeDrop.PotionMana:
                if (_actualLife <= 0 && PrefabBox != null)
                {
                    StartCoroutine(TimerDropPotionMana(1));
                }
                break;

            case EnemiesTypeDrop.Null: break;

        }
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
        if (TypeEnemie == EnemiesTypes.Boss)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radiusDetectPlayerForAtackRangeType01);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _radiusDetectPlayerForAtackRangeType02);
        }


        Gizmos.color = new Color(1f, 0f, 0, 1f);
        Gizmos.DrawCube(m_colliderChildren[0].bounds.center, m_colliderChildren[0].bounds.size);
        Gizmos.color = new Color(0f, 1f, 0, 1f);
        Gizmos.DrawCube(m_colliderChildren[1].bounds.center, m_colliderChildren[1].bounds.size);

    }
    IEnumerator TimeDropBox(float time)
    {

        yield return new WaitForSeconds(time);
        if (!_isIntancied)
            Instantiate(PrefabBox, transform.position, Quaternion.identity);
        _isIntancied = true;



    }
    IEnumerator TimeDropKey(float time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator TimeDropPotionHeal(float time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator TimerDropPotionMana(float time)
    {
        yield return new WaitForSeconds(time);
    }

}




