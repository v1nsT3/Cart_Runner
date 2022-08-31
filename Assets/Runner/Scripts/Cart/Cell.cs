using UnityEngine;

[System.Serializable]
public class Cell : IReadOnlyCell
{
    [SerializeField] private Block _block;
    [SerializeField] private Transform _blockTransform;

    public Cell(Transform transform)
    {
        _blockTransform = transform;
        _block = null;
    }

    public bool IsCellEmpty => _block == null;

    public ITransformable Block => _block;

    public void Add(Block block)
    {
        _block = block;
        _block.transform.SetParent(_blockTransform, true);
        _block.transform.position = _blockTransform.position;
        _block.transform.rotation = _blockTransform.rotation;
        block.Broken += OnBlockBroken;
    }

    private void OnBlockBroken(Block block)
    {
        block.Broken -= OnBlockBroken;
        Clear();
    }

    public void Clear()
    {
        _block.transform.parent = null;
        _block = null;
    }
}
