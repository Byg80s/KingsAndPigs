using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _deadZone;
    [SerializeField] private int _selectRespawnArea;
    private void Start()
    {
        _gameManager = GameManager.instance;
        _deadZone = _gameManager.IsDeadZone;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _player = other.gameObject.GetComponent<PlayerController>();
       
        SelectAreaRespawn();
        StartCoroutine(BlockInputRespawn(_gameManager.TimeBlockInputsRespawn));



    }

    void SelectAreaRespawn()
    {
        switch (_selectRespawnArea)
        {
            case 0:
                GameManager.instance.ReSpawnPlayer();
                _player.Died();
                break;
            case 1:
                GameManager.instance.ExitDoor();
                _player.ExitLevel();
                break;
       
          
        }


    }

    IEnumerator BlockInputRespawn(float time)
    {
        _gameManager.blockInputs = true;
        yield return new WaitForSeconds(time);
        _gameManager.blockInputs=false;
    }
}
