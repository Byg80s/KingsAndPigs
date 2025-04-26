using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BoxDisplazamentOptions : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private PlayerController m_playerController;
    [SerializeField] private GameObject m_pointDetect;
    [SerializeField] private RaycastHit2D hitRigth, hitLeft;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _distanceToGround;
    [SerializeField] private LayerMask m_layerMaskPlayer;
    [SerializeField] private LayerMask m_layerMaskGround;

    private Vector2 front;
    private Vector2 back;
    [SerializeField] private bool _isPushed;

    private void Start()
    {
        m_pointDetect = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody2D>();
        m_playerController = FindAnyObjectByType<PlayerController>();
        front = Vector2.right;
        back = Vector2.left;

    }
    private void FixedUpdate()
    {
        CheckIgPushPlayerButton();

    }


    private void CheckIgPushPlayerButton()
    {

        _isPushed = m_playerController.IsPushed;

        if (m_pointDetect == null)return;
        {
          
            hitRigth = Physics2D.Raycast(transform.position, front, _distanceToPlayer, m_layerMaskPlayer);
            hitLeft = Physics2D.Raycast(transform.position, back, _distanceToPlayer, m_layerMaskPlayer);

            if (!_isPushed&&( hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRigth.collider != null && hitRigth.collider.CompareTag("Player")))
            {
                Debug.DrawLine(transform.position, m_pointDetect.transform.position, Color.blue);
                m_rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }


            else
            {
                m_rb.constraints = RigidbodyConstraints2D.None;
            }



        }

    }
}
