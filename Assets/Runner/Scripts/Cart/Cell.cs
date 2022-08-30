using UnityEngine;

[System.Serializable]
public class Cell 
{
    [SerializeField] private Block _block;
    [SerializeField] private Transform _blockTransform;

    public Cell(Transform transform)
    {
        _blockTransform = transform;
        _block = null;
    }

    public bool IsCellEmpty => _block == null;

    public Transform Transform => _blockTransform;

    public void Add(Block block)
    {
        _block = block;
        _block.transform.SetParent(_blockTransform, true);
        _block.transform.position = _blockTransform.position;
        _block.transform.rotation = _blockTransform.rotation;
    }

    public void Clear()
    {
        _block = null;
    }
}
