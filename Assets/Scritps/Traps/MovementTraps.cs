using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTraps : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Transform[] m_Way;

    public int _index = 0;
    [SerializeField] private int _speedMove;

    private void Awake()
    {

    }
    private void Start()
    {
        _gameManager = GameManager.instance;
        
    }

    private void Update()
    {
       

        IfActivateEvente();



    }
    void IfActivateEvente()
    {
        if (m_Way.Length == 0) return;
        else WatPointsMove();
    }

    void WatPointsMove()
    {

        if (_gameManager.ActivateTrapSnife())
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Way[_index].transform.position, _speedMove * Time.deltaTime);


            if (Vector2.Distance(transform.position, m_Way[_index].transform.position) < 0.01f)

            {
                _index += 1 % m_Way.Length;
            }
            if (_index >= m_Way.Length)
            {
                _index = 0;
            }
        }

    }


}
