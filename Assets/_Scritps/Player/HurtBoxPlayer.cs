using UnityEngine;

public class HurtBoxPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBoxEnemy"))
        {
            GameManager.instance.CurrentLife--;
            GameManager.instance.NumberOfLives--;


        }
    }
   
}
