using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime *rotationSpeed);
    }
}
