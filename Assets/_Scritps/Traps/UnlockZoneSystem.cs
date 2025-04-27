using UnityEngine;

public class UnlockZoneSystem : MonoBehaviour
{
    [Header("Parameters for open zone")]
    [SerializeField] private float _timeSpeedUnlock;
    [SerializeField] private float _positionInX;
    [SerializeField] private float _positionInY;
    [Tooltip("Array of Switches need for open")]
    [SerializeField] private SwitchActions[] switches;
    [Tooltip("Select 0 to choose open a door, select 1 to choose enable GameObject")]
    [SerializeField] private int _selectOption;
    [SerializeField] private bool _open;
    [SerializeField] private bool _show;
    [SerializeField] DirectionList _directions;
    [SerializeField] private GameObject _ObjectMakeVisible;
    //    [Tooltip("Input tag of GameObject new show")]
    //   [SerializeField] private string _tagGameObject;


    private void Start()
    {

        SelectGameObejct();
    }
    // Update is called once per frame
    void Update()
    {

        if (_open) Unlock();

    }
    private void Unlock()
    {
        //this code change for animator of GameObject

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

        if (_selectOption == 0) OpenDoor();
        else if (_selectOption == 1) ActiveGameObject();
       

    }
    private void OpenDoor()
    {
        _open = true;
        // animate
    }
    private void ActiveGameObject()
    {
        _show = true;
        _ObjectMakeVisible.SetActive(true);
    }
    private void SelectGameObejct()
    {
        switch (_selectOption)
        {
            case 0:
                break;
            case 1:             
                if (_ObjectMakeVisible == null)
                {
                    Debug.Log("is null");
                }
                _ObjectMakeVisible.SetActive(false);
               break;
        }

    }
   

}
