using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointsBlock = new List<Transform>();

    private float _durationMoveToFinishPoint = 0.2f;
    private float _delayFinishPerSec = 1f;
    private FinishCollisionDetector _detector;
    private Cart _cart = null;

    public event UnityAction Finished;

    private void Awake()
    {
        _detector = GetComponentInChildren<FinishCollisionDetector>();
    }

    private void OnEnable()
    {
        _detector.Detected += OnFinished;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnFinished;
    }

    private void OnFinished(Cart cart)
    {
        _cart = cart;
        StartCoroutine(FinishDelay());
    }

    private IEnumerator FinishDelay()
    {
        yield return new WaitForSeconds(_delayFinishPerSec);
        StartCoroutine(MoveBlockToFinishPointDelay());
        Finished?.Invoke();
    }

    private IEnumerator MoveBlockToFinishPointDelay()
    {
        int indexPoint = 0;

        for (int i = _cart.Cells.Count; i > 0; i--)
        {
            if (_cart.Cells[i - 1].Block != null)
            {
                Transform blockTransform = _cart.Cells[i - 1].Block.Transform;
                blockTransform.DOMove(_pointsBlock[indexPoint].position, _durationMoveToFinishPoint);
                blockTransform.DORotateQuaternion(_pointsBlock[indexPoint].rotation, _durationMoveToFinishPoint);
                blockTransform.DOScale(_pointsBlock[indexPoint].localScale, _durationMoveToFinishPoint);
                _cart.Cells[i - 1].Clear();
                indexPoint++;
                yield return new WaitForSeconds(0.03f);
            }

            yield return null;
        }
    }
}
