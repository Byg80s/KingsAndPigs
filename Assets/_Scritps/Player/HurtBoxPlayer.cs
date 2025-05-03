using UnityEngine;

public class HurtBoxPlayer : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemyControler _enemyTypes;
    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _enemyTypes = FindAnyObjectByType<EnemyControler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            _playerController.KnockBack();
            if (_playerController.IsNocked) _playerController.CurrentLife--;
            GameManager.instance.LifeSystem(_playerController.CurrentLife);
        }
      


    }

}
