using UnityEngine;

public class BlockWrecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Block block))
        {
            if(block.IsBroken == false)
                block.Break();
        }
    }
}
