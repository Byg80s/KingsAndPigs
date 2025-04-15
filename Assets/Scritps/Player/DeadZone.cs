using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private bool _deadZone;
    private void Start()
    {
        _gameManager = GameManager.instance;
        _deadZone = _gameManager.IsDeadZone;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _player = other.gameObject.GetComponent<PlayerController>();
        _player.Died();
        GameManager.instance.ReSpawnPlayer();
        StartCoroutine(BlockInputRespawn(_gameManager.TimeBlockInputsRespawn));



    }
    IEnumerator BlockInputRespawn(float time)
    {
        _gameManager.blockInputs = true;
        yield return new WaitForSeconds(time);
        _gameManager.blockInputs=false;
    }
}
