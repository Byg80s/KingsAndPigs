using System;
using Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crystals : MonoBehaviour
{

    //References
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    private int _idAnim, _idCristalsBlendtree;



    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _idAnim = Animator.StringToHash("_pickedCristal");
        _idCristalsBlendtree = Animator.StringToHash("_CristalsAnimation");
    }


    private void Start()
    {
        _gameManager = GameManager.instance;
        SetRandomCristals();
    }
    private void Update()
    {

    }
    private void SetRandomCristals()
    {
        var RandomCristals = Random.Range(0, 7);
        _animator.SetFloat(_idCristalsBlendtree,RandomCristals);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
       //     _spriteRenderer.enabled = false;
            m_rb.simulated = false;
            _gameManager.AddCristals();
            _animator.SetTrigger(_idAnim);
        }

    }
}
