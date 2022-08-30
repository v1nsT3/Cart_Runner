using UnityEngine;
using UnityEngine.Events;

public class BasicZoneCollisionDetector : MonoBehaviour
{
    public event UnityAction<Cart> Detected;

    private bool _isDetected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isDetected == true)
            return;

        if (other.TryGetComponent(out Cart cart))
        {
            Detected?.Invoke(cart);
            _isDetected = true;
        }
    }
}
