using UnityEngine;

public class HurtBoxPlayer : MonoBehaviour
{
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController= GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            //GetComponentInParent<PlayerController>().
            _playerController.KnockBack();
         if(_playerController.IsNocked  ) _playerController.CurrentLife--;

           

        }
    }
   
}
