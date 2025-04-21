using UnityEngine;

public class DoorEvents : MonoBehaviour
{


    [SerializeField] private GameObject DoorRespawn;
    [SerializeField] private GameObject DoorClose;
    [SerializeField] private GameObject DoorOpen;
    [SerializeField] private Animator animatorRespawnDoor;
    [SerializeField] private Animator animatorCloseDoor;
    [SerializeField] private Animator animatorOpenDoor;
    [SerializeField] private string _FindTag;
    public string FindTag { get => _FindTag; set => _FindTag = value; }

    private int _idRespawnDoor;
    private int _idOpenDoor;
    private int _idCloseDoor;


    private void OnEnable()
    {
        _idRespawnDoor = Animator.StringToHash("_RespawnDoor");
        _idOpenDoor = Animator.StringToHash("_RespawnDoor");
        _idCloseDoor = Animator.StringToHash("_RespawnDoor");
        DoorRespawn = GameObject.FindGameObjectWithTag("RespawnDoor");
        DoorClose = GameObject.FindGameObjectWithTag("ExirDoor");
        DoorOpen = GameObject.FindGameObjectWithTag("EntranceDoor");
        animatorRespawnDoor = DoorRespawn.GetComponent<Animator>();
        animatorCloseDoor = DoorClose.GetComponent<Animator>();
        animatorOpenDoor = DoorOpen.GetComponent<Animator>();

    }
    void Start()
    {


    }

    public void RespawnDoor()
    {
        animatorRespawnDoor.SetTrigger(_idRespawnDoor);
    }
    public void CloseDoor()
    {
        animatorCloseDoor.SetTrigger(_idCloseDoor);
    }
    public void OpenDoor()
    {
        animatorOpenDoor.SetTrigger(_idOpenDoor);
    }

}
