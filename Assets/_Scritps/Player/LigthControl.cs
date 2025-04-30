using UnityEngine;

public class LigthControl : MonoBehaviour
{
    [SerializeField] private float _intensityGlobalLigth;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GlobalLigth(_intensityGlobalLigth);
        }
    }
}
