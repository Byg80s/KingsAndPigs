using System;

using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateBombs : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
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


    private void Update()
    {
        Spawm();
    }
  
    void Spawm()
    {
        if (_activeBombs && !_isActivated)
        {          
            for (int i = 0; i < numberOfBombs; i++)
            {

                randomX = Random.Range(minX, maxX);
                randomY= Random.Range(minY, maxY);
                SpawnPos = new Vector2(randomX, randomY);
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
