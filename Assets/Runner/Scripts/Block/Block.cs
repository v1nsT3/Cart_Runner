using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _force = 2f;

    public bool IsBroken { get; private set; } = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Break()
    {
        IsBroken = true;
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.up * _force, ForceMode.Impulse);
    }
}
