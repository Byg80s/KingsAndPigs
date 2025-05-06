using UnityEngine;

public class MachineLaser : MonoBehaviour
{
    private Transform _playerLocation;
    [SerializeField] private float _speedRotate;




    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (transform.rotation.z < 45f) transform.Rotate(0, 0, 1 * _speedRotate * Time.deltaTime);

        //check this
        else transform.Rotate(0, 0, -1 * _speedRotate * Time.deltaTime);
    }
}
