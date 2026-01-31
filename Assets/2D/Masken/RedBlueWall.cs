using UnityEngine;

public class RedBlueWall : MaskListener
{

    public bool isRedwall;
    
    void Start()
    {
        FlipWallStatus(isRedwall ? ActiveMasks.RedBlueMask : ActiveMasks.NONE);
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        FlipWallStatus(activeMasks);
    }

    private void FlipWallStatus(ActiveMasks activeMasks)
    {
        gameObject.SetActive(activeMasks.HasFlag(ActiveMasks.RedBlueMask) ?  isRedwall : !isRedwall);
    }
}
