using UnityEngine;
using System.Collections;

public class BlockGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject block;

    public GameObject GetBlock()
    {
        return block;
    }

    public void SetBlock(GameObject _block)
    {
        block = _block;
    }

    public void ResetBlock()
    {
        block = null;
    }
}
