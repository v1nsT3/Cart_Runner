using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] private List<Cell> _blocksPositions = new List<Cell>();

    public IReadOnlyList<IReadOnlyCell> Cells => _blocksPositions;

    private void Start()
    {
        foreach (Transform blockPos in GetComponentInChildren<Transform>())
        {
            Cell cell = new Cell(blockPos);
            _blocksPositions.Add(cell);
        }
    }

    public void AddBlock(Block block)
    {
        foreach (var cell in _blocksPositions)
        {
            if (cell.IsCellEmpty)
            {
                cell.Add(block);
                return;
            }
        }
    }
}
