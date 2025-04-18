using Unity.VisualScripting;
using UnityEngine;

public class DesactivateTraps : MonoBehaviour
{
    [SerializeField] private bool ActivateTimeDiscount;
    [SerializeField] private int numberDesactivation;
    [SerializeField] private bool numberDeactivation;
    [SerializeField] private float timeDesactivate, newTime;


    private void Start()
    {
        newTime = timeDesactivate;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.DesactivationTraps[numberDesactivation] = true;
        }

    }
    private void Update()
    {
        DesactivateButton();
    }

    //If ActivateTimeButton is enable in the inspector  the counter is run. If no is active the button no is desactivated
    void DesactivateButton()
    {
        if (ActivateTimeDiscount)
        {
            if (GameManager.instance.DesactivationTraps[numberDesactivation])
            {
                Debug.Log("Is activate");
                timeDesactivate -= Time.deltaTime;
            }
            else if (timeDesactivate <= 0)
            {
                GameManager.instance.DesactivationTraps[numberDesactivation] = false;
                timeDesactivate = newTime;
                Debug.Log("Is desactivate");
            }
        }
    }
}
