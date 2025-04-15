using UnityEngine;

public class DoorEvents : MonoBehaviour
{


    [SerializeField] private GameObject entranceDoor;
    [SerializeField] private Animator animatorEntranceDoor;
    private int _idOpenDoor;

    private void OnEnable()
    {
        _idOpenDoor = Animator.StringToHash("_OpenDoor");
        entranceDoor = GameObject.FindWithTag("EntranceDoor");
        animatorEntranceDoor = entranceDoor.GetComponent<Animator>();
    }
    void Start()
    {

    
    }

    public void DoorOut()
    {
        animatorEntranceDoor.SetTrigger(_idOpenDoor);
    }
}
