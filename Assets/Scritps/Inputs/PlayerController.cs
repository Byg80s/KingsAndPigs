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
    [SerializeField] private float _speed;
    [SerializeField] private int _direction = 1;
    [SerializeField] private int _idSpeed;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ginput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        _idSpeed = Animator.StringToHash("_speed");
    }

    void Update()
    {
        Animations();
        Flip();
    }
    private void FixedUpdate()
    {
  
    
        Move();

    }
    private void Move()
    {
        //Movement directional
        m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, m_rb.linearVelocity.y * Time.deltaTime);

    }
    private void Flip()
    {
        if (m_ginput.ValueX * _direction < 0)
        {

            m_transform.localScale = new Vector2(-m_transform.localScale.x, m_transform.localScale.y);
            _direction *= -1;
        }

    }
    private void Animations()
    {
        m_animator.SetFloat(_idSpeed, Mathf.Abs(m_rb.linearVelocityX));
    }
}
