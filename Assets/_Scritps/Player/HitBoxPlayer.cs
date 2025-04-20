using System.Collections;
using UnityEngine;

public class HitBoxPlayer : MonoBehaviour
{

    [SerializeField] private Animator m_anim;


    private void Start()
    {
        m_anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Switch"))

            m_anim.SetBool("_isPushButton",true);
        

     
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Switch"))

            StartCoroutine(TimeAnimation());
    }

    IEnumerator TimeAnimation()
    {
        yield return new WaitForSeconds(3);
        m_anim.SetBool("_isPushButton", false);
    }


}
