using UnityEngine;

public class HurtBoxPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {

            GetComponentInParent<PlayerController>().KnockBack();
            GetComponentInParent<PlayerController>().CurrentLife--;


        }
    }
   
}
