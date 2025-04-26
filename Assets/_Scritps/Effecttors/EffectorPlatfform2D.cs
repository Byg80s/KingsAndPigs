using UnityEngine;

public class EffectorPlatfform2D : MonoBehaviour
{

    [SerializeField] private PlatformEffector2D _effector;
    [SerializeField] private GatherInput m_ginput;
    [SerializeField] private GameObject _player;
    private void Awake()
    {
        
    }
    void Start()
    {
        _effector = gameObject.GetComponent<PlatformEffector2D>();
        m_ginput = _player.GetComponent<GatherInput>();

    }

    void Update()
    {
        if(_effector != null && m_ginput!=null)   UpPlattdorm();
    }
    void DownPlattdorm()
    {

        if ( m_ginput.DownPt)
        {
            //Debug.Log("Down is Press: "+m_ginput.DownPt);
            _effector.rotationalOffset = 180;
         
        }


    }
    void UpPlattdorm()
    {
        if (m_ginput.IsJumping)
        {
            _effector.rotationalOffset = 0;
          //  Debug.Log("Up is Press: " + m_ginput.UpPt);
        }

    }
  
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_effector != null && m_ginput != null) DownPlattdorm();
        
        }
    }

}
