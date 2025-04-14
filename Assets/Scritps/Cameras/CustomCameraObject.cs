using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraObject : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cineMachineCamera;
    [SerializeField] private CinemachinePositionComposer _Composer;
    [SerializeField] private GameObject _firstCamera;
    

    //extra, is no for this script, is only for check

    [SerializeField] private GameObject _Portal;
    [SerializeField] private Animator _animPortal;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private int _idClosePortal;
    


    private void Start()
    {
       _Composer =_cineMachineCamera.GetComponent<CinemachinePositionComposer>();
        _Portal = GameObject.FindGameObjectWithTag("Portal");
       

        _SpriteRenderer=_Portal.GetComponent<SpriteRenderer>();
        _SpriteRenderer.enabled = false;
    


    }

    private void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _Composer.TargetOffset.y = 3f;
        _Composer.TargetOffset.x = 3f;
        _SpriteRenderer.enabled = true;

      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _Composer.TargetOffset.y = 0f;
        _Composer.TargetOffset.x = 0f;
        Destroy(_firstCamera);

    }
  
}
