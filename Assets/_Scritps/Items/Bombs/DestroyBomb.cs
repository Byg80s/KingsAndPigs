using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBomb : MonoBehaviour
{
    private CinemachineCamera _camShake;
    private float timeShake = 0;
    private PlayerController m_playerControler;
    private Animator m_anim;
    private int _idXplode;
    private AnimatorStateInfo state;

    private void Awake()
    {

    }
    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_playerControler = FindAnyObjectByType<PlayerController>();
        state = m_anim.GetCurrentAnimatorStateInfo(0);



    }
    private void Update()
    {
        timeShake += Time.deltaTime;
        Animations();
    }
    void Animations()
    {
        _idXplode = Animator.StringToHash("Xplote");
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Ground")
        {
            m_anim.SetTrigger(_idXplode);
            StartCoroutine(Shake());
            StartCoroutine(DestroyThisBomb());          
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            m_anim.SetTrigger(_idXplode);
            m_playerControler.KnockBack();
            m_playerControler.CurrentLife--;
            StartCoroutine(DestroyThisBomb());
        }

    }



    IEnumerator Shake()
    {
        FindAnyObjectByType<CameraShake>().ShakeCamera(2f, 1f);
        yield return new WaitForSeconds(1f);
        FindAnyObjectByType<CameraShake>().ShakeCamera(0f, 0f);
    }

    IEnumerator DestroyThisBomb()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject != null)
            Destroy(gameObject);
    }
}
