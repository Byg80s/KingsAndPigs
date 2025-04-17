using Unity.VisualScripting;
using UnityEngine;

public class DesactivateTraps : MonoBehaviour
{
    private bool Desactivate;
    [SerializeField] private int numberDesactivation ;
    [SerializeField] private bool numberDeactivation ;
    [SerializeField] private float timeDesactivate,newTime;


    private void Start()
    {
         newTime=timeDesactivate;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Activado"); GameManager.instance.DesactivationTraps[numberDesactivation] = true;
           
        }
  
    }
    private void Update()
    {
        DesactivateButton();

    }
    void DesactivateButton()
    {
      
        if(GameManager.instance.DesactivationTraps[numberDesactivation])
            timeDesactivate -= Time.deltaTime;
       
        if(timeDesactivate<=0 )
        {
            GameManager.instance.DesactivationTraps[numberDesactivation]=false;
            timeDesactivate = newTime;
        }

    }
}
