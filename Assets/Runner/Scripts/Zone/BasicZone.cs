using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BasicZoneCollisionDetector))]
public class BasicZone : MonoBehaviour
{
    [SerializeField] private int _ammount;
    [SerializeField] private TMP_Text _ammountText;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Color _reachedColor;
    [SerializeField] private Block _blockTemplate;

    private BasicZoneCollisionDetector _detector;
    private List<Block> _blocks = new List<Block>();
    private float _delayPerSec = 0.05f;

    private void Awake()
    {
        _detector = GetComponent<BasicZoneCollisionDetector>();
    }

    private void OnEnable()
    {
        _detector.Detected += OnCollisionDetected;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnCollisionDetected;
    }

    private void Start()
    {
        for (int i = 0; i < _ammount; i++)
        {
            Block block = Instantiate(_blockTemplate);
            block.gameObject.SetActive(false);
            _blocks.Add(block);
        }

        _ammountText.text = "+" + _ammount.ToString();
    }

    private void OnCollisionDetected(Cart cart)
    {
        StartCoroutine(AddBlockDelay(cart));
        SetColor();
    }

    private IEnumerator AddBlockDelay(Cart cart)
    {
        foreach (Block block in _blocks)
        {
            yield return new WaitForSeconds(_delayPerSec);
            block.gameObject.SetActive(true);
            cart.AddBlock(block);
            yield return null;
        }
    }

    private void SetColor()
    {
        var main = _particle.main;
        main.startColor = _reachedColor;
    }
}
