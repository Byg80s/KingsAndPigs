using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Transform m_transform;
    [SerializeField] private float _speed;
    [SerializeField] private int _direction = 1;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ginput = GetComponent<GatherInput>();
        m_transform = GetComponent<Transform>();
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        Flip();
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
}
