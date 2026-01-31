using UnityEngine;

public class CubeTest : MaskListener
{
    public MeshRenderer meshRenderer;

    public override void MaskChange(ActiveMasks activeMasks)
    {
        meshRenderer.enabled = !activeMasks.HasFlag(ActiveMasks.RedBlueMask);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
