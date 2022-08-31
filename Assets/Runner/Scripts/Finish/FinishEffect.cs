using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FinishHandler))]
public class FinishEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects = new List<ParticleSystem>();

    private FinishHandler _finishHandler;

    private void Awake()
    {
        _finishHandler = GetComponent<FinishHandler>();
    }

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
        foreach (var effect in _effects)
        {
            effect.Play();
        }
    }
}
