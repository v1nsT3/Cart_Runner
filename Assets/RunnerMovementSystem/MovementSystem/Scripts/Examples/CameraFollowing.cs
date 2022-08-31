using UnityEngine;
using DG.Tweening;

namespace RunnerMovementSystem.Examples
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private FinishHandler _finishHandler;
        [SerializeField] private Transform _finishCameraPoint;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [Space(15)]
        [SerializeField] private Transform _target;
        [SerializeField] private float _height;
        [SerializeField] private float _distance;
        [SerializeField] private float _lookAngle;

        private float _durationMoveToFinishPoint = 1.5f;
        private Vector3 _targetPosition;
        private bool _isFinished = false;

        private void OnEnable()
        {
            _finishHandler.Finished += OnFinished;
        }

        private void OnDisable()
        {
            _finishHandler.Finished -= OnFinished;
        }

        private void LateUpdate()
        {
            if (_isFinished == true)
                return;

            _targetPosition = _target.position;
            _targetPosition -= _target.forward * _distance;
            _targetPosition += Vector3.up * _height;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

            var targetRotation = Quaternion.LookRotation(_target.forward, Vector3.up);
            targetRotation.eulerAngles = new Vector3(_lookAngle, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void OnFinished()
        {
            _isFinished = true;
            transform.DOMove(_finishCameraPoint.position, _durationMoveToFinishPoint);
            transform.DORotateQuaternion(_finishCameraPoint.rotation, _durationMoveToFinishPoint);
        }
    }
}