using UnityEngine;

public class Gheiser : MonoBehaviour
{

    [SerializeField] private float _timeStartCicle;
    [SerializeField] private float _timeRestartCicle;
    [SerializeField] private float _particlesLong;
    private float _timeStep;
    private float _valueStart = 0;
    private float _valueInCilce;
    private ParticleSystem _particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timeStep = 0;

        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeStep += Time.deltaTime;
      //  Debug.Log(_timeStep);
        StartParticles();
    }
    void StartParticles()
    {
        var ParticlesPropierties = _particleSystem.main;
        ParticlesPropierties.startSpeed = _valueStart;

        if (_timeStep > _timeStartCicle)
        {
            _valueInCilce = _particlesLong;
            _valueStart = _valueInCilce;

        }
        if (_timeStep >= _timeRestartCicle)
        {
            _timeStep = 0;
            _valueStart = 0;

        }
    }
}
