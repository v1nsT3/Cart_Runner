using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FinishHandler _finishHandler;

    private void OnEnable()
    {
        _finishHandler.Finished += OnFinished;
    }

    private void OnDisable()
    {
        _finishHandler.Finished -= OnFinished;
    }

    private void OnFinished()
    {
        _animator.speed = 0;
    }
}
