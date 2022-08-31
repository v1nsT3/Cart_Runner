using RunnerMovementSystem;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _stickMan;
    [SerializeField] private RoadSegment _targetSegment;

    private float _maxOffset = 3.5f;

    private float _currentOffset => _targetSegment.GetOffsetByPosition(_stickMan.position);
    private bool _isOnRoad => _currentOffset >= -_maxOffset && _currentOffset <= _maxOffset;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * -1;
        float speed = mouseX * _rotationSpeed * Time.deltaTime;

        //Quaternion quaternion = _cyl.rotation;
        //quaternion *= Quaternion.Euler(0, speed, 0);
        //Vector3 vector3 = _cyl.position;
        //vector3 += quaternion * new Vector3(0, 0, Vector3.Distance(transform.position, _stickMan.position));
        //_pos = vector3;

        //float offset = _targetSegment.GetOffsetByPosition(_pos);

        //Debug.Log(offset);

        //if (offset >= -_maxOffset && offset <= _maxOffset)
        //{
        //    //targetRotation = transform.rotation * Quaternion.Euler(0, speed, 0);
        //}
        //else
        //{
        //    transform.rotation *= Quaternion.Euler(0, offset < -1 ? -1 : 1 * _rotationSpeed * Time.deltaTime, 0);
        //}

        if (_isOnRoad == false)
        {
            transform.rotation *= Quaternion.Euler(0, _currentOffset < -1 ? -1 : 1 * _rotationSpeed * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            transform.rotation *= Quaternion.Euler(0, speed, 0);
        }
    }
}
