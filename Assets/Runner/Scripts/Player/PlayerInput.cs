using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        float speed = Input.GetAxis("Mouse X") * -1 * _rotationSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, speed, 0);
    }
}
