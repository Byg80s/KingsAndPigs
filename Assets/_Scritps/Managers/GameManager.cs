using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using Unity.Android.Gradle;
using UnityEngine.Rendering.Universal;

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
    [Tooltip("This is the maxim lifes")]
    [SerializeField] private int _ActualLife;
    public int ActualLife { get => _ActualLife; set => _ActualLife = value; }
    [Tooltip("This is the current life in this moment")]
    [SerializeField] private int _CurrentLive;
    public int CurrentLive { get => _CurrentLive; set => _CurrentLive = value; }
    [SerializeField] private int _NumberOfLives;
    public int NumberOfLives { get => _NumberOfLives; set => _NumberOfLives = value; }

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
    /*
        [Header("Activate Traps")]
        [SerializeField] private bool[] _DesactivationTraps;
        public bool[] DesactivationTraps { get => _DesactivationTraps; set => _DesactivationTraps = value; }

        [Header("Event Open Ground")]
        [SerializeField] private int x;
    */
    #endregion
    [Tooltip("Global light")]
    [SerializeField] private Light2D ligthOptions;


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
        RespawnPlayer(_PlayerPrefab, _PlayerRespawnPoint, "Player");
        _playerControler.CurrentLife = ActualLife;
        //Check this
        _playerControler.maxLife = NumberOfLives;
    }

    IEnumerator RespawnPlayerCorotineIfExit(int time)
    {
        yield return new WaitForSeconds(time);
        RespawnPlayer(_PlayerPrefab, _PlayerExitLevelPoint, "Player");
        _playerControler.CurrentLife = CurrentLive;
    }
    private void RespawnPlayer(GameObject Prefab, Transform PointRespawn, string Prefabtag)
    {
        GameObject newPlayer = Instantiate(Prefab, PointRespawn.position, Quaternion.identity);
        virtualCamera.Follow = newPlayer.transform;
        newPlayer.name = Prefabtag;
        _playerControler = newPlayer.GetComponent<PlayerController>();
    }
    public void AddCristals() => _cristalCollected++;
    public bool CrystalsHaveRandomLook() => CrystalsHaveRandomLook1;
    // public bool ActivateTrapSnife() => ActivateTrapSnife1;
    public int NumbersOfWayPoints() => IndexWaipointTrapSnife;
    public bool BlockInputs() => blockInputs;
    public float TimerInputsBlockRespawn() => TimeBlockInputsRespawn;
    public bool DeadZoneActivate() => IsDeadZone;
    public void GlobalLigth(float ligth)
    {
        ligthOptions.intensity = ligth;
    }
  


}
