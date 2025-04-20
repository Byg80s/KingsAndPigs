using UnityEngine;

public class HitBoxEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("HurtBox"))
        {
            collision.GetComponentInParent <PlayerController>().KnockBack();
        }
        Debug.Log("The Player Have Damage");

    }
}
