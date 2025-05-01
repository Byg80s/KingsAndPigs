using UnityEngine;

public class BombTake : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _ThrowOffset;
    [SerializeField] private bool _isThrow;


  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
