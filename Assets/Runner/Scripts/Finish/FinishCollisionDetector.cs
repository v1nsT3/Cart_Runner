using UnityEngine;
using UnityEngine.Events;

public class FinishCollisionDetector : MonoBehaviour
{
    public event UnityAction<Cart> Detected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cart cart))
        {
            Detected?.Invoke(cart);
        }
    }
}
