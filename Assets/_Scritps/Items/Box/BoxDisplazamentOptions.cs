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

    [SerializeField] private FixedJoint2D m_fixedJoint;

    private Vector2 front;
    private Vector2 back;
    [SerializeField] private bool _isPushed;
    [SerializeField] private bool _isTaked;

    private void Start()
    {
        m_pointDetect = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody2D>();
        m_playerController = FindAnyObjectByType<PlayerController>();
        m_fixedJoint = GetComponent<FixedJoint2D>();
        front = Vector2.right;
        back = Vector2.left;
        m_fixedJoint.enabled = false;

    }
    private void FixedUpdate()
    {
        if (!_isTaked) CheckIsPushPlayerButton();
    //    if (!_isPushed) CheckIsTakePlayerButton();
    }


    private void CheckIsPushPlayerButton()
    {

        _isPushed = m_playerController.IsPushed;

        if (m_pointDetect == null) return;
        {
            CheckHitLayerMaskPlayer();

            if (!_isPushed && (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRigth.collider != null && hitRigth.collider.CompareTag("Player")))
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
    private void CheckIsTakePlayerButton()
    {
        _isTaked = m_playerController.IsTake;

        if (m_pointDetect == null) return;
        {
            CheckHitLayerMaskPlayer();
            if ((_isPushed || !_isTaked) && (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRigth.collider != null && hitRigth.collider.CompareTag("Player")))
            {
                Debug.DrawLine(transform.position, m_pointDetect.transform.position, Color.yellow);
                m_fixedJoint.enabled = false;
            }


            else
            {


                Debug.Log(_isTaked);
                // m_rb.constraints = RigidbodyConstraints2D.None;
                if (!_isPushed && _isTaked)
                {
                    m_fixedJoint.enabled = true;
                    m_fixedJoint.connectedBody = this.GetComponent<Rigidbody2D>();
                }
            }

        }
    }
    private void CheckHitLayerMaskPlayer()
    {
        hitRigth = Physics2D.Raycast(transform.position, front, _distanceToPlayer, m_layerMaskPlayer);
        hitLeft = Physics2D.Raycast(transform.position, back, _distanceToPlayer, m_layerMaskPlayer);
    }
}
