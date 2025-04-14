using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private PlayerController _playerControler;
    public PlayerController PlayerControler { get => _playerControler; }

    [Header("Cristals Manager")]
    [SerializeField] private bool _CrystalsHaveRandomLook;
    public bool CrystalsHaveRandomLook1 { get => _CrystalsHaveRandomLook; }

    [SerializeField] private int _cristalCollected;
    public int CristalCollected => _cristalCollected;
    [SerializeField] private int _CrystalsHaveRandom;


    [Header("Parameters and WaitPoint Traps")]
   


    [SerializeField] private bool _ActivateTrapSnife;
    public bool ActivateTrapSnife1 { get => _ActivateTrapSnife; }
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; }

    [SerializeField] private int _indexWaipointTrapSnife;
    public int IndexWaipointTrapSnife { get => _indexWaipointTrapSnife; set => _indexWaipointTrapSnife = value; }

    [Header("Activate Traps")]
    [SerializeField] private int x1;

    [Header("Event Open Ground")]
    [SerializeField] private int x;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    public void AddCristals() => _cristalCollected++;
    public bool CrystalsHaveRandomLook() => CrystalsHaveRandomLook1;
    public bool ActivateTrapSnife() => ActivateTrapSnife1;
    public int NumbersOfWayPoints() => IndexWaipointTrapSnife;




}
