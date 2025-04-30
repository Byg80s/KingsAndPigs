using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LigthGlobalOptions : MonoBehaviour
{
   [SerializeField] private Light2D ligthOptions;
    [SerializeField] private float _intensityData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ligthOptions = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ligthOptions.intensity = _intensityData;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& ligthOptions!=null)
        {
            ligthOptions.intensity = _intensityData;
            Debug.Log("Player detected");

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ligthOptions != null)
        {
            ligthOptions.intensity = _intensityData;
            Debug.Log("Player detected");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ligthOptions != null)
        {
            ligthOptions.intensity = _intensityData;
            Debug.Log("Player detected");

        }
    }
}
