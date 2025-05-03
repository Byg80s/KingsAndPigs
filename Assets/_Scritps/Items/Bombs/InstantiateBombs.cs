using System;

using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateBombs : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private BoxCollider2D Area;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [SerializeField] private float timeSelected;
    [SerializeField] private float newTime;
    [SerializeField] private int timeBomsSpawn;
    [SerializeField] private int numberOfBombs;
    [SerializeField] private bool _activeBombs;
   
    public bool activeBombs { get => _activeBombs; set => _activeBombs = value; }

    [SerializeField] private bool _isActivated = false;
    public bool IsActivated { get => _isActivated; set => _isActivated = value; }

     Vector2 SpawnPos;
    private float randomX;
    private float randomY;

    private void Start()
    {
        
      
    }
    private void Update()
    {
        Spawm();
    }
  
    void Spawm()
    {
        Vector2 center = Area.bounds.center;
        Vector2 size = Area.bounds.size;

        if (_activeBombs && !_isActivated)
        {          
            for (int i = 0; i < numberOfBombs; i++)
            {
                
                randomX = Random.Range(-size.x/2f, size.x/2f);
                randomY= Random.Range(-size.y / 2f, size.y / 2f);
                SpawnPos =  center + new Vector2(randomX, randomY);
                Instantiate(prefab, SpawnPos, Quaternion.identity);                                
            }
            _isActivated = true;
        }
        if (!_activeBombs)
        {
            _isActivated = false; 
        }
    }
    void TimerCount()
    {
        timeSelected -= Time.deltaTime;
        if (timeSelected <= 0)
        {
            timeSelected = newTime;
        }
    }
}
