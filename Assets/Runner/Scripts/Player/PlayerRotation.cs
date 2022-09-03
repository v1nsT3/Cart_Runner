using RunnerMovementSystem;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _stickMan;
    [SerializeField] private RoadSegment _targetSegment;
    [SerializeField] private float _border;

    private float _maxOffset;
    private Vector3 _futurePosition;
    private float _distance;

    private float _futureOffset => _targetSegment.GetOffsetByPosition(_futurePosition);
    private float _currentOffset => _targetSegment.GetOffsetByPosition(_stickMan.position);
    private bool _isOnRoad => _futureOffset >= -_maxOffset && _futureOffset <= _maxOffset;

    private void Start()
    {
        _maxOffset = _targetSegment.Width - _border;
        _distance = Vector3.Distance(transform.position, _stickMan.position);
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * -1;
        float angle = mouseX * _rotationSpeed * Time.deltaTime;

        Quaternion _futureRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        _futurePosition = transform.position - (_futureRotation * new Vector3(0, 0, _distance));

        if (_isOnRoad == true && Input.GetKey(KeyCode.Mouse0))
            Rotate(_futureRotation, _rotationSpeed);
        else
            BackRotate();
    }

    private void Rotate(Quaternion targetRotation, float speed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private void BackRotate()
    {
        Quaternion rotation = _targetSegment.GetRotationAtDistance(_targetSegment.GetClosestDistanceAlongPath(transform.position)) * Quaternion.Euler(0, 0, 90f);
        float speed = Mathf.Lerp(_rotationSpeed, 0, Mathf.Abs(_maxOffset / _currentOffset));
        Rotate(rotation, speed);
    }
}
