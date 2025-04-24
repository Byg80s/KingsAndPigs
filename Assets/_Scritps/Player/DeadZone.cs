using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _player = other.gameObject.GetComponent<PlayerController>();


        SpawnAreaDeath();



    }

    void SpawnAreaDeath()
    {
        GameManager.instance.ReSpawnPlayer();
        _player.Died();

    }

}
