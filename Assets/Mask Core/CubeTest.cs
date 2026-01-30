using UnityEngine;

public class CubeTest : MaskListener
{
    public MeshRenderer meshRenderer;

    public override void MaskChange(Masks mask)
    {
        meshRenderer.enabled = !mask.HasFlag(Masks.DISAPPEAR);
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
