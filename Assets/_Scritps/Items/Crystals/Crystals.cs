using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crystals : MonoBehaviour
{

    //References
    [Header("Components")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [Header("Color Crystals")]
    [SerializeField] private CrystalsType _crystalType;
    [SerializeField] internal int pointsColor;
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
        PointsColors();
    }
    private void Update()
    {

    }
    private int PointsColors()
    {
        switch (_crystalType)
        {
            case CrystalsType.Cyan:
                pointsColor = 100;

                break;
            case CrystalsType.Green:
                pointsColor = 200;
                break;
            case CrystalsType.Red:
                pointsColor = 300;
                break;
            case CrystalsType.Orange:
                pointsColor = 400;
                break;
            case CrystalsType.Purple:
                pointsColor = 500;
                break;
        }
        return pointsColor;
    }
    private void SetRandomCristals()
    {
        if (!_gameManager.CrystalsHaveRandomLook())
        {
            UpdateDiamondType();
            return;
        }
        var RandomCristals = Random.Range(0, 7);
        _animator.SetFloat(_idCristalsBlendtree, RandomCristals);
    }


    private void UpdateDiamondType()
    {
        _animator.SetFloat(_idCristalsBlendtree, (int)_crystalType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            m_rb.simulated = false;
            if (_crystalType == CrystalsType.Cyan) _gameManager.AddCristals();
            _animator.SetTrigger(_idAnim);
            AudioManager.instance.Play("Gems");
            _gameManager._PointsRecibed +=pointsColor;

        }

    }


}
