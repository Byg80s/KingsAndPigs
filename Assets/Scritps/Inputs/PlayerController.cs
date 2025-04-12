using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private GatherInput m_ginput;
    private Transform m_transform;
    [SerializeField]  private float _speed;

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

        //Movement directional
        m_rb.linearVelocity = new Vector2(_speed * m_ginput.ValueX, m_rb.linearVelocity.y*Time.deltaTime);
    }
}
