using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBomb : MonoBehaviour
{
    private PlayerController m_playerControler;
    private Animator m_anim;
    private int _idXplode;
    private AnimatorStateInfo state;
    private void Start()
    {
        m_anim = GetComponentInParent<Animator>();
        m_playerControler = FindAnyObjectByType<PlayerController>();
        state = m_anim.GetCurrentAnimatorStateInfo(0);

    }
    private void Update()
    {
        Animations();
    }
    void Animations()
    {
        _idXplode = Animator.StringToHash("Xplote");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("EarthWave"))
        {
            m_anim.SetTrigger(_idXplode);
            m_playerControler.KnockBack();
            StartCoroutine(DestroyThisBomb());
        }
        if (collision.CompareTag("Player") && (state.IsName("Xplote")))
        {
            m_playerControler.KnockBack();

        }
    }

    IEnumerator DestroyThisBomb()
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
