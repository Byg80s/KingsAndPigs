using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   
    [SerializeField] private PlayerController _playerControler;
    public PlayerController PlayerControler { get => _playerControler; }

    [Header("Cristals Manager")]
    [SerializeField] private bool _CrystalsHaveRandomLook;
    [SerializeField] private int _cristalCollected;
    public int CristalCollected  => _cristalCollected;

    public bool CrystalsHaveRandomLook1 { get => _CrystalsHaveRandomLook; }

    [SerializeField] private int _CrystalsHaveRandom;

    

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddCristals()=>_cristalCollected++;
    public bool CrystalsHaveRandomLook() => CrystalsHaveRandomLook1;

}
