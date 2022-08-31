using UnityEngine;

public interface IReadOnlyCell 
{
    public ITransformable Block { get; }
    public void Clear();
}
