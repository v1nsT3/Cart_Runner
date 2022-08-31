using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BlockCollisionDetector))]
[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    private BlockCollisionDetector _detector;
    private Rigidbody _rigidbody;
    private float _force = 2f;

    public bool IsBroken { get; private set; } = false;

    public event UnityAction<Block> Broken;

    private void Awake()
    {
        _detector = GetComponent<BlockCollisionDetector>();
    }

    private void OnEnable()
    {
        _detector.Detected += OnColissionDetected;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnColissionDetected;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Break()
    {
        IsBroken = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.up * _force, ForceMode.Impulse);
        Broken?.Invoke(this);
    }

    private void OnColissionDetected(Cart cart)
    {
        if (IsBroken == false)
            cart.AddBlock(this);
    }
}
