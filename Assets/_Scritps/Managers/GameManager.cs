using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CinemachineCamera virtualCamera;
    //Player Controler
    #region PLAYER SETTINGS
    [Header("Player Settings")]
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private Transform _PlayerRespawnPosition;
    [SerializeField] private Transform _PlayerExitLevelPoint;
    [SerializeField] private PlayerController PlayerControler;
    [SerializeField] internal bool blockInputs;

    [Header("Live Player")]
    [Tooltip("This is the maxim lifes")]
    [SerializeField] internal int ActualLife;
    [Tooltip("This is the current life in this moment")]
    [SerializeField] internal int CurrentLive;
    [SerializeField] internal int NumberOfLives;
    [SerializeField] private int _timeRespawn;
    public int TimeRespawn => _timeRespawn;
    [SerializeField] private float _timeBlockInputsRespawn;
    public float TimeBlockInputsRespawn { get => _timeBlockInputsRespawn; set => _timeBlockInputsRespawn = value; }
    #endregion
    #region ENEMY SETTINGS
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

    [Tooltip("Global light")]
    [SerializeField] private Light2D ligthOptions;

    [Header("Respawn parameters")]
    [SerializeField] internal bool _hasCheckPointActive;
    [SerializeField] internal Vector3 _checkPointPosition;

    #region UI
    [Header("Ui control")]
    [SerializeField] private Slider sliderLive;
    #endregion
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }
    private void Start()
    {
        
    }
    private void Update()
    {
      
    }
    public void ReSpawnPlayer()
    {
        if (_hasCheckPointActive) _PlayerRespawnPosition.position = _checkPointPosition;
        StartCoroutine(RespawnPlayerCorotineIfDie(TimeRespawn));

    }
    public void ExitDoor() => StartCoroutine(RespawnPlayerCorotineIfExit(TimeRespawn));

    IEnumerator RespawnPlayerCorotineIfDie(int time)
    {
        yield return new WaitForSeconds(time);
        RespawnPlayer(_PlayerPrefab, _PlayerRespawnPosition, "Player");
        PlayerControler.CurrentLife = ActualLife;
        //Check this
        PlayerControler.maxLife = NumberOfLives;
    }

    IEnumerator RespawnPlayerCorotineIfExit(int time)
    {
        yield return new WaitForSeconds(time);
        RespawnPlayer(_PlayerPrefab, _PlayerExitLevelPoint, "Player");
        PlayerControler.CurrentLife = CurrentLive;
    }
    private void RespawnPlayer(GameObject Prefab, Transform PointRespawn, string Prefabtag)
    {
        GameObject newPlayer = Instantiate(Prefab, PointRespawn.position, Quaternion.identity);
        virtualCamera.Follow = newPlayer.transform;
        newPlayer.name = Prefabtag;
        PlayerControler = newPlayer.GetComponent<PlayerController>();
    }
    public void AddCristals() => _cristalCollected++;
    public bool CrystalsHaveRandomLook() => CrystalsHaveRandomLook1;
    // public bool BlockInputs() => blockInputs;
    public float TimerInputsBlockRespawn() => TimeBlockInputsRespawn;
    public void GlobalLigth(float ligth)
    {
        ligthOptions.intensity = ligth;
    }
    public void LifeSystemMaxHealth(int health)
    {
        sliderLive.maxValue = health;
        sliderLive.value = health;

    }
    public void LifeSystem(int health)
    {
        sliderLive.value = health;
    }



}
