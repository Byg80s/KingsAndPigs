using UnityEngine;

public class HurtBoxEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("HitBox"))
        {
            GetComponentInParent<EnemyControler>().KnockBack();
            GetComponentInParent<EnemyControler>().ActualLife--;
            Debug.Log("Is damage recibe");
        }






       

    }
}
