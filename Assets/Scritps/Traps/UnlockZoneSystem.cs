using UnityEngine;

public class UnlockZoneSystem : MonoBehaviour
{
    [Header("Parameters for open zone")]
    [SerializeField] private float _timeSpeedUnlock;
    [SerializeField] private bool _activeParticles;
    [SerializeField] private float _positionInX;
    [SerializeField] private float _positionInY;
    [SerializeField] private int _indexOfNeedSwitchDesactivate;
    [SerializeField] private bool _open;
    [SerializeField] DirectionList _directions;

    // Update is called once per frame
    void Update()
    {
        StatesDesactivateSwitches();
        if (_open) Unlock();
    }


    void Unlock()
    {


        switch (_directions)
        {
            case DirectionList.Down:
                transform.Translate(Vector2.down * _timeSpeedUnlock * Time.deltaTime);
                if (transform.position.y < _positionInY)
                {

                    Destroy(gameObject);
                }
                break;

            case DirectionList.Up:
                transform.Translate(Vector2.up * _timeSpeedUnlock * Time.deltaTime);
                if (transform.position.y < _positionInY)
                {

                    Destroy(gameObject);
                }

                break;
            case DirectionList.Right:
                transform.Translate(Vector2.right * _timeSpeedUnlock * Time.deltaTime);
                if (transform.position.x < _positionInX)
                {

                    Destroy(gameObject);
                }

                break;
            case DirectionList.Left:
                transform.Translate(Vector2.left * _timeSpeedUnlock * Time.deltaTime);
                if (transform.position.x < _positionInX)
                {

                    Destroy(gameObject);
                }
                break;


        }
    }
    void StatesDesactivateSwitches()
    {

        for (int i = 0; i < GameManager.instance.DesactivationTraps.Length ; i++)
        {
            if (GameManager.instance.DesactivationTraps[_indexOfNeedSwitchDesactivate - 1] == true) 
            { 
                Debug.Log("Its Unlock");
                _open = true;
            }
            else
            {
                _open = false;
            }
        }
    }
}
