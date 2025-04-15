using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraObject : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [Header("Cinemachine Components")]
    [SerializeField] private CinemachineCamera _cineMachineCamera;
    [SerializeField] private CinemachinePositionComposer _Composer;
    [SerializeField] private GameObject _firstCamera;


    //extra, is no for this script, is only for check
    [Header("Paramaters Object")]
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField]private string _name;
    [SerializeField] private float _TargetOffSetY;
    [SerializeField] private float _TargetOffSetX;
    [SerializeField] private float _TimeOffset;
    [SerializeField] private float _TimeBlockInputs;
    [SerializeField] private bool isRenderNeed;
    private bool _enabled;



    private void Start()
    {
        _gameManager = GameManager.instance;
        _Composer =_cineMachineCamera.GetComponent<CinemachinePositionComposer>();
        _gameObject = GameObject.FindGameObjectWithTag(_name);

      
        _SpriteRenderer= _gameObject.GetComponent<SpriteRenderer>();
        _SpriteRenderer.enabled = false;
        


    }

    private void Update()
    {
        EnableCamera();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(TimeOffSet(_TimeOffset));



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        _Composer.TargetOffset.y = 0f;
        _Composer.TargetOffset.x = 0f;
        Destroy(_firstCamera);
        */

    }
    private void EnableCamera()
    {
        if (_enabled)
        {
            _Composer.TargetOffset.y = _TargetOffSetX;
            _Composer.TargetOffset.x = _TargetOffSetY;
            if (isRenderNeed)
            {
                _SpriteRenderer.enabled = true;
            }
        
            Debug.Log("IS ENABLE. " + _SpriteRenderer.enabled);
            StartCoroutine(UnblockInput(_TimeBlockInputs));
        }
      
    }
    IEnumerator TimeOffSet(float time)
    {
        yield return new WaitForSeconds(time);
        _enabled = true;
        _gameManager.blockInputs = true;
    }
    IEnumerator UnblockInput(float time)
    {
        yield return new WaitForSeconds(_TimeBlockInputs);
        _gameManager.blockInputs = false;
        _Composer.TargetOffset.y = 0f;
        _Composer.TargetOffset.x = 0f;
        Destroy(_firstCamera);

    }


}
