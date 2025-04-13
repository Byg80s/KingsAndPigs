using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Controler")]
    [SerializeField] private PlayerController _playerControler;
    public PlayerController PlayerControler { get => _playerControler; }

    [Header("Variables")]
    [SerializeField] private int _cristalCollected;
    public int CristalCollected { get => _cristalCollected; }



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddCristals()=>_cristalCollected++;

}
