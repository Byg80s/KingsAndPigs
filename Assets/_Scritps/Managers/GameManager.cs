using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CinemachineCamera virtualCamera;
    //Player Controler
    #region PLAYER SETTINGS
    [Header("Player Settings")]
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private Transform _PlayerRespawnPoint;
    [SerializeField] private Transform _PlayerExitLevelPoint;
    [SerializeField] private PlayerController _playerControler;
    [SerializeField] private bool _isPushAction;
    public bool IsPushAction { get => _isPushAction; set => _isPushAction = value; }

    [Header("Live Player")]
    [SerializeField] private int _CurrentLife;
    public int CurrentLife { get => _CurrentLife; set => _CurrentLife = value; }
    [SerializeField] private int _ActualLife;
    public int ActualLife { get => _ActualLife; set => _ActualLife = value; }


    public PlayerController PlayerControler => _playerControler;
    [SerializeField] private int _timeRespawn;
    public int TimeRespawn => _timeRespawn;
    [SerializeField] private bool _blockInputs;
    public bool blockInputs { get => _blockInputs; set => _blockInputs = value; }
    [SerializeField] private float _timeBlockInputsRespawn;
    public float TimeBlockInputsRespawn { get => _timeBlockInputsRespawn; set => _timeBlockInputsRespawn = value; }

    [Header("Enemy Settings")]
    [SerializeField] private bool _detectedPlayerIsGround;
    public bool DetectedPlayerIsGround { get => _detectedPlayerIsGround; set => _detectedPlayerIsGround = value; }

    #endregion
    #region CRISTALS MANAGER
    [Header("Cristals Manager")]
    [SerializeField] private bool _CrystalsHaveRandomLook;
    public bool CrystalsHaveRandomLook1 { get => _CrystalsHaveRandomLook; }

    [SerializeField] private int _cristalCollected;
    public int CristalCollected => _cristalCollected;
    [SerializeField] private int _CrystalsHaveRandom;
    #endregion  
    #region TRAPS PARAMETERS
    //Traps
    [Header("Parameters and WaitPoint Traps")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; }

    [SerializeField] private int _indexWaipointTrapSnife;
    public int IndexWaipointTrapSnife { get => _indexWaipointTrapSnife; set => _indexWaipointTrapSnife = value; }

    [SerializeField] private bool isDeadZone;
    public bool IsDeadZone { get => isDeadZone; set => isDeadZone = value; }

    [Header("Activate Traps")]
    [SerializeField] private bool[] _DesactivationTraps;
    public bool[] DesactivationTraps { get => _DesactivationTraps; set => _DesactivationTraps = value; }

    [Header("Event Open Ground")]
    [SerializeField] private int x;
    #endregion


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }
    private void Update()
    {

    }
    public void ReSpawnPlayer() => StartCoroutine(RespawnPlayerCorotineIfDie(TimeRespawn));
    public void ExitDoor() => StartCoroutine(RespawnPlayerCorotineIfExit(TimeRespawn));

    IEnumerator RespawnPlayerCorotineIfDie(int time)
    {
        yield return new WaitForSeconds(time);
        GameObject newPlayer = Instantiate(_PlayerPrefab, _PlayerRespawnPoint.position, Quaternion.identity);
        virtualCamera.Follow = newPlayer.transform;
        newPlayer.name = "Player";
        _playerControler = newPlayer.GetComponent<PlayerController>();
        CurrentLife = ActualLife;

    }

    IEnumerator RespawnPlayerCorotineIfExit(int time)
    {
        yield return new WaitForSeconds(time);
        GameObject newPlayer = Instantiate(_PlayerPrefab, _PlayerExitLevelPoint.position, Quaternion.identity);
        virtualCamera.Follow = newPlayer.transform;
        newPlayer.name = "Player";
        _playerControler = newPlayer.GetComponent<PlayerController>();
    }
    public void AddCristals() => _cristalCollected++;
    public bool CrystalsHaveRandomLook() => CrystalsHaveRandomLook1;
    // public bool ActivateTrapSnife() => ActivateTrapSnife1;
    public int NumbersOfWayPoints() => IndexWaipointTrapSnife;

    public bool BlockInputs() => blockInputs;
    public float TimerInputsBlockRespawn() => TimeBlockInputsRespawn;
    public bool DeadZoneActivate() => IsDeadZone;


}
