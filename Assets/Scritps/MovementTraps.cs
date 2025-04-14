using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementTraps : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Transform[] m_Way;

    public int _index = 0;

    private void Awake()
    {

    }
    private void Start()
    {
        _gameManager = GameManager.instance;
        //  _index = 0;
        // _Trap= GameObject.Find("TrapSnife").GetComponent<Transform>();
        // _index = _gameManager.IndexWaipointTrapSnife;
        _gameManager.IndexWaipointTrapSnife = _index;
        // transform.position = m_WaitpointFirst[_index].transform.position;


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
            transform.position = Vector2.MoveTowards(transform.position, m_Way[_gameManager.IndexWaipointTrapSnife].transform.position, _gameManager.MoveSpeed * Time.deltaTime);


            if (Vector2.Distance(transform.position, m_Way[_gameManager.IndexWaipointTrapSnife].transform.position) < 0.01f)

            {
                _gameManager.IndexWaipointTrapSnife += 1 % m_Way.Length;
            }
            if (_gameManager.IndexWaipointTrapSnife >= m_Way.Length)
            {
                _gameManager.IndexWaipointTrapSnife = 0;
            }
        }





    }


}
