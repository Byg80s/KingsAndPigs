using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoxDisplazamentOptions : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private PlayerController m_playerController;
    [SerializeField] private GameObject m_pointDetect;
    private Rigidbody2D PlayerRb;
    [SerializeField] private RaycastHit2D hitRigth, hitLeft, hitDown;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _distanceToGround;
    [SerializeField] private float pullSpeed;
    [SerializeField] private LayerMask m_layerMaskPlayer;
    [SerializeField] private LayerMask m_layerMaskGround;


    public float pullForce = 10f;


    private Vector2 front;
    private Vector2 back;
    private Vector2 down;
    [SerializeField] private bool _isPushed;
    [SerializeField] private bool _isTaked;
    internal bool takeBox=false;

    private void Start()
    {
        m_pointDetect = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody2D>();
        m_playerController = FindAnyObjectByType<PlayerController>();
        front = Vector2.right;
        back = Vector2.left;
        down = Vector2.down;
        PlayerRb = m_pointDetect.gameObject.GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        //  if (!_isTaked)
        CheckIsPushPlayerButton();
        // if (!_isPushed)
        CheckIsTakePlayerButton();
        hitDown = Physics2D.Raycast(transform.position, down, _distanceToGround, m_layerMaskGround);

    }


    private void CheckIsPushPlayerButton()
    {

        _isPushed = m_playerController.IsPushed;

        if (m_pointDetect == null) return;
        {
            CheckHitLayerMaskPlayer();

            if ((!_isPushed || _isTaked) && (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRigth.collider != null && hitRigth.collider.CompareTag("Player")))
            {
                Debug.DrawLine(transform.position, m_pointDetect.transform.position, Color.blue);
                if (hitDown) m_rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                else m_rb.constraints = RigidbodyConstraints2D.None;
            }
            else m_rb.constraints = RigidbodyConstraints2D.None;
        }

    }
    private void CheckIsTakePlayerButton()
    {
        _isTaked = m_playerController.IsTake;

        if (m_pointDetect == null) return;

        CheckHitLayerMaskPlayer();

        if ((_isTaked) && (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRigth.collider != null && hitRigth.collider.CompareTag("Player")))
        {

            transform.parent = m_playerController.transform;
            m_rb.constraints = RigidbodyConstraints2D.None;
            m_rb.bodyType = RigidbodyType2D.Kinematic;

            Debug.Log("is take");
            takeBox=true;

        }
        else
        {
            transform.parent = null;
            m_rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("No take");
            takeBox=false;
        }

    }

    private void CheckHitLayerMaskPlayer()
    {
        hitRigth = Physics2D.Raycast(transform.position, front, _distanceToPlayer, m_layerMaskPlayer);
        hitLeft = Physics2D.Raycast(transform.position, back, _distanceToPlayer, m_layerMaskPlayer);
        Collider2D player = Physics2D.OverlapCircle(transform.position, _distanceToPlayer, m_layerMaskPlayer);

    }





}
