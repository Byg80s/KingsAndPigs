using UnityEngine;

public class HitBoxEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBox"))
        {
            collision.GetComponentInParent<PlayerController>().KnockBack();
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
     
        if (collision.CompareTag("HurtBox"))
        {
            collision.GetComponentInParent<PlayerController>().KnockBack();
            collision.GetComponentInParent<PlayerController>().CurrentLife--;
            GameManager.instance.LifeSystem(collision.GetComponentInParent<PlayerController>().CurrentLife);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBox"))
        {
            collision.GetComponentInParent<PlayerController>().KnockBack();

        }

    }
}
