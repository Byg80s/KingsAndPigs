using UnityEngine;

public class Crystals : MonoBehaviour
{

    //References
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;



    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        _gameManager = GameManager.instance;
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteRenderer.enabled = false;
            m_rb.simulated = false;
            _gameManager.AddCristals();
        }

    }
}
