using UnityEngine;

public class RespawnInDoorArea : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
  

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (!other.CompareTag("Player")) return;

        _player = other.gameObject.GetComponent<PlayerController>();
        SpawnAreaRespawn();

    }
    void SpawnAreaRespawn()
    {
        GameManager.instance.ExitDoor();
        GameManager.instance.CurrentLive=_player.CurrentLife;
        _player.ExitLevel();

    }
}
