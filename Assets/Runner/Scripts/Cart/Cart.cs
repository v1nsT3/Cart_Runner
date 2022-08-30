using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] private List<Cell> _blocksPositions = new List<Cell>();

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
            if(block.IsBroken == false)
                AddBlock(block);
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
