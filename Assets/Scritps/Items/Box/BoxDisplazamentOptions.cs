using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BoxDisplazamentOptions : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Collider2D HitBox;
    [SerializeField] private float _forcePush;



    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
       m_collider = 
        HitBox = GetComponentInChildren<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HitBox.gameObject.CompareTag("Box") && GameManager.instance.IsPushAction)

        {
            Debug.Log("TRUE");
          // m_rb.MovePosition(m_rb.position+new Vector2(transform.position.x,transform.position.y)*_forcePush*Time.deltaTime);
          m_rb.bodyType=RigidbodyType2D.Dynamic;

        }
        else
            m_rb.bodyType=RigidbodyType2D.Static;
        

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
    }

}
