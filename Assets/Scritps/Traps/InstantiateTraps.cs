using UnityEngine;
using UnityEngine.VFX;

public class InstantiateTraps : MonoBehaviour
{

    [SerializeField] private GameObject _TrapPrefab;
    [SerializeField] private float _x,_y;

    private void Start()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Respawn();
            Destroy(gameObject, 0.2f);
        }

    }
    void Respawn()
    {
        Vector2 newPosition= new Vector2(_x, _y);
        Instantiate(_TrapPrefab,newPosition,Quaternion.identity);
    }
}
