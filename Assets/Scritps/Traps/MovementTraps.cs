using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTraps : MonoBehaviour
{

    [SerializeField] private Transform[] m_Way;
    [SerializeField] private bool _Active;
    [SerializeField] private int _index;
    [SerializeField] private int _speedMove;
    [SerializeField] private int _idActivated;
    [SerializeField] private bool needAnimation;
    private Animator m_anim;
    public int index { get => _index; set => _index = value; }

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        index = 0;
        _idActivated = Animator.StringToHash("_isActived");
    }

    private void Update()
    {
        IfActivateEvente();
        if (needAnimation)
        {
            Animations();
        }

    }
    void Animations()
    {
        m_anim.SetBool(_idActivated, _Active);
    }
    void IfActivateEvente()
    {
        if (m_Way.Length == 0) return;
        else WatPointsMove();
    }

    void WatPointsMove()
    {
        if (_Active)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Way[index].transform.position, _speedMove * Time.deltaTime);

            if (Vector2.Distance(transform.position, m_Way[index].transform.position) < 0.01f)

            {
                index += 1 % m_Way.Length;
            }
            if (index >= m_Way.Length)
            {
                index = 0;
            }
        }
    }

}
