using UnityEngine;

public class HurtBoxEnemy : MonoBehaviour
{
    private EnemyControler m_EnemyControler;
    private void Awake()
    {
        m_EnemyControler=GetComponentInParent<EnemyControler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("HitBox"))
        {
            m_EnemyControler.KnockBack();
            m_EnemyControler.ActualLife--;
            m_EnemyControler.LifeSystem(m_EnemyControler.ActualLife);
            Debug.Log("Is damage recibe");
        } 
    }
}
