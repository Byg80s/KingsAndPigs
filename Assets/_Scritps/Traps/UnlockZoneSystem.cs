using UnityEngine;

public class UnlockZoneSystem : MonoBehaviour
{
    [Header("Parameters for open zone")]
    [SerializeField] private float _timeSpeedUnlock;
    [SerializeField] private float _positionInX;
    [SerializeField] private float _positionInY;
    [Tooltip("Array of Switches need for open")]
    [SerializeField] private SwitchActions[] switches;
    [SerializeField] private bool _open;
    [SerializeField] DirectionList _directions;



    // Update is called once per frame
    void Update()
    {
        
        CheckSwitches();
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
    public void CheckSwitches()
    {

        foreach (var switches2 in switches)
        {
            if (!switches2.IsActivated) return;

        }

        OpenDoor();
    }
    void OpenDoor()
    {
        _open = true;
        Debug.Log("Door Open");
        // animate
       
    }
  
}
