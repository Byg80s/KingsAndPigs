using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BoxCollider2D _col;
    [SerializeField] private LayerMask _collisionMask;
    private Transform _rotate;
    private LineRenderer _rayLine;
    private EnemyControler _enemyControler;

    [Header("Timers")]
    private float _time;
    [SerializeField] private float _resetTime;
    [Tooltip("Set time need for respawn laser")]
    [SerializeField] private float _maxTimeForReset;
    [Tooltip("Distane Ray, now in configurate internal")]
    [SerializeField] private float _maxDistance;

    [Header("Parameters")]

    [SerializeField] private bool _activeLaser = false, _activeTimer = false; //then erase _activeLaser, because use de enemy script boss
    [SerializeField] private float _speed;
    Vector2 SizeCollider;
    private float _colliderX = 0;
    private float _colliderY = 0;



    void Start()
    {
        _rotate = GetComponent<Transform>();
        _enemyControler = FindAnyObjectByType<EnemyControler>();
        _rayLine = GetComponent<LineRenderer>();
        _col = GetComponent<BoxCollider2D>();
        TakeDataCollider();
    }

    void Update()
    {
        if (_activeLaser) TimerCount(_activeLaser);
        CastRay();
        TimerReset(_maxTimeForReset);
    }

    private void TimerCount(bool Active)
    {
        if (Active)

            _time += Time.deltaTime;
        _maxDistance++;
        SizeCollider.x += _colliderX;
        _col.size = SizeCollider;
        if (_maxDistance >= 21.5f)
        {
            _activeLaser = false;
            _time = 0;
            _activeTimer = true;
        }
        //check this

        /* if (_resetTime >= 10)
             _maxDistance = 0f;
          _resetTime = 0;
        }*/
    }
    private void TimerReset(float maxTime)
    {
        if (_activeTimer) _resetTime += Time.deltaTime;
        if (_resetTime >= maxTime)
        {
            _maxDistance = 0;
            _resetTime = 0;
            _activeTimer=false;

        }

    }
    private void TakeDataCollider()
    {
        SizeCollider = _col.size;
        SizeCollider.x = _colliderX;
        SizeCollider.y = _colliderY;
        _maxDistance = 0f;
    }
    private void CastRay()
    {
        Vector2 laserOrigin = transform.position;
        Vector2 laserDirection = transform.right; // Cambia a .up o .down según orientación
        Vector3 endPosition;

        RaycastHit2D hit = Physics2D.Raycast(laserOrigin, laserDirection, _maxDistance, _collisionMask);


        if (hit.collider != null) endPosition = hit.point;
        else endPosition = laserOrigin + laserDirection * _maxDistance;


        _rayLine.SetPosition(0, laserOrigin);
        _rayLine.SetPosition(1, endPosition);
    }
}
