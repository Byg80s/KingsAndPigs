using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BoxDisplazamentOptions : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Collider2D HitBox;




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

            Debug.Log("Detected");

        }
     

        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
    }

}
