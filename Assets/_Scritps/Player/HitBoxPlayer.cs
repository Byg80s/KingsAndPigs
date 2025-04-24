using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HitBoxPlayer : MonoBehaviour
{

    [SerializeField] private Animator m_anim;
    PlayerController m_playerController;

    private void Start()
    {
        m_anim = GetComponentInParent<Animator>();
        m_playerController = GetComponentInParent<PlayerController>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Switch"))
        {
            m_anim.SetBool("_isPushButton", true);

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Switch"))
        {
            StartCoroutine(TimeAnimation());

        }
        
    }
    IEnumerator TimeAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        m_anim.SetBool("_isPushButton", false);
     
    }


}
