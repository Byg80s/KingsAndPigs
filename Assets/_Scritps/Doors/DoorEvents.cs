using UnityEngine;

public class DoorEvents : MonoBehaviour
{


    [SerializeField] private GameObject DoorRespawn;
    [SerializeField] private Animator animatorRespawnDoor;
    [SerializeField] private GameObject DoorOpen;
    [SerializeField] private Animator animatorOpenDoor;

    // [SerializeField] private Animator animatorCloseDoor;
    // [SerializeField] private GameObject DoorClose;
    private int _idRespawnDoor;
    private int _idOpenDoor;
    private int _idCloseDoor;


    private void OnEnable()
    {
        _idRespawnDoor = Animator.StringToHash("_RespawnDoor");
        DoorRespawn = GameObject.FindGameObjectWithTag("RespawnDoor");
        animatorRespawnDoor = DoorRespawn.GetComponent<Animator>();
        _idOpenDoor = Animator.StringToHash("_RespawnDoor");
        DoorOpen = GameObject.FindGameObjectWithTag("EntranceDoor");
        animatorOpenDoor = DoorOpen.GetComponent<Animator>();
        //  _idCloseDoor = Animator.StringToHash("_RespawnDoor");

        //DoorClose = GameObject.FindGameObjectWithTag("ExirDoor");
        //    animatorCloseDoor = DoorClose.GetComponent<Animator>();


    }
    void Start()
    {


    }

    public void RespawnDoor()
    {
        animatorRespawnDoor.SetTrigger(_idRespawnDoor);
    }
    /*   public void CloseDoor()
     {
         animatorCloseDoor.SetTrigger(_idCloseDoor);
     }     
    */
    public void OpenDoor()
    {
        animatorOpenDoor.SetTrigger(_idOpenDoor);
    }
}
