using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private static readonly int _idIsActive = Animator.StringToHash("_isActived");
    [SerializeField] private Animator m_Anim;
    [SerializeField] private bool _isActive;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isActive)return;

        if (collision.CompareTag("Player"))
            ActiveCheckPoint();
    }

    private void ActiveCheckPoint()
    {
       _isActive = true;
        m_Anim.SetTrigger(_idIsActive);
        GameManager.instance._hasCheckPointActive = true;
        GameManager.instance._checkPointPosition = transform.position;
    }

}
