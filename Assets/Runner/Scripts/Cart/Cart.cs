using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private List<Cell> _blocksPositions = new List<Cell>();

    private void Start()
    {
        foreach (Transform blockPos in GetComponentInChildren<Transform>())
        {
            Cell cell = new Cell(blockPos);
            _blocksPositions.Add(cell);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Block block))
        {
            AddBlock(block);
        }
    }

    private void AddBlock(Block block)
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
