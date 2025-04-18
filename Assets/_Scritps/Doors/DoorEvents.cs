using UnityEngine;

public class DoorEvents : MonoBehaviour
{


    [SerializeField] private GameObject Door;
    [SerializeField] private Animator animatorEntranceDoor;
    [SerializeField] private string _FindTag;
    public string FindTag { get => _FindTag; set => _FindTag = value; }

    private int _idOpenDoor;


    private void OnEnable()
    {
        _idOpenDoor = Animator.StringToHash("_OpenDoor");
        Door = GameObject.FindWithTag("RespawnDoor");
        animatorEntranceDoor = Door.GetComponent<Animator>();
    }
    void Start()
    {

    
    }
  
    public void DoorOut()
    {
        animatorEntranceDoor.SetTrigger(_idOpenDoor);
    }
}
