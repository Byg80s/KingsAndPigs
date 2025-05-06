using System.Collections;
using Unity.Collections;
using UnityEngine;

public class ActiveTextShow : MonoBehaviour
{
    [SerializeField] private GameObject TextActive;
    private bool _activeText = true;

    void Start()
    {        
        Desactivate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(tag);       
       if(_activeText) StartCoroutine(ActivateShowText());

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }

    void Activate()
    {
        TextActive.SetActive(true);
    }
    private void Desactivate()
    {
        TextActive.SetActive(false);        
    }
    IEnumerator ActivateShowText()
    {
        Activate();
        yield return new WaitForSeconds(3);
        Desactivate();
        _activeText = false;
    }

}
