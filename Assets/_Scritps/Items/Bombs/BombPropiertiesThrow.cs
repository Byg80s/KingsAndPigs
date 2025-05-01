using UnityEngine;

public class BombPropiertiesThrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _Offset;
    [SerializeField] private bool _isThrow;
    

    void Start()
    {
        
        if (_isThrow)
        {
            var Direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(Direction * _speed, ForceMode2D.Impulse);
        }
        transform.Translate(_Offset);
    }

    void Update()
    {
        if (!_isThrow) transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            if (gameObject != null) Destroy(gameObject);
        }
    }

}
