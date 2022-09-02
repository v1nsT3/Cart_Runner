using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(BlockCollisionDetector))]
[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour, ITransformable
{
    private BlockCollisionDetector _detector;
    private Rigidbody _rigidbody;
    private float _force = 4f;
    private float _durationDecreaseScale = 4f;

    public bool IsBroken { get; private set; } = false;

    public Transform Transform => transform;

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
        _rigidbody.AddForce(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)) * Random.Range(0, _force), ForceMode.VelocityChange);
        _rigidbody.AddTorque(new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)) * Random.Range(0, _force), ForceMode.VelocityChange);
        transform.DOScale(Vector3.zero, _durationDecreaseScale);
        Broken?.Invoke(this);
    }

    private void OnColissionDetected(Cart cart)
    {
        if (IsBroken == false)
            cart.AddBlock(this);
    }
}
