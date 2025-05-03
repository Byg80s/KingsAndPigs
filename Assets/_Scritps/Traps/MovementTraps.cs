using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTraps : MonoBehaviour
{

    [SerializeField] private Transform[] m_Way;
    [SerializeField] private bool _Active;
    [SerializeField] private int _index;
    [SerializeField] private float _speedMove;
    [SerializeField] private int _idActivated;
    [SerializeField] private bool needAnimation;
    [SerializeField] private bool _fixInPlattform = false;
    private PlayerController _playerController;
    private Animator m_anim;
    public int index { get => _index; set => _index = value; }

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        index = 0;
        _idActivated = Animator.StringToHash("_isActived");
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {

        IfActivateEvente();

        if (needAnimation) Animations();


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")   && _fixInPlattform)
        {
            collision.gameObject.transform.parent = transform;
        }
        //NEED CHECK THIS
        if (collision.gameObject.CompareTag("Box") && !_playerController.IsPushed)
        {
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _fixInPlattform)
        {
            collision.gameObject.transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Box") && _playerController.IsPushed) 
        {
            collision.gameObject.transform.parent = null;
        }

    }
}
