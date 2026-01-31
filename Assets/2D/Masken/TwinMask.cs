using UnityEngine;

public class TwinMask : MaskListener
{
    public override void MaskChange(ActiveMasks activeMasks)
    {
        TwinMaskLogic();
    }

    public void TwinMaskLogic()
    {
        var twin = GameObject.FindWithTag("Twin");
        if (twin == null) return;

        twin.GetComponent<GridMovement>().isMovementActive = MaskStore.SelectedActiveMasks.HasFlag(ActiveMasks.TwinMask);
        LevelManager.Instance.Player.GetComponent<GridMovement>().isMovementActive = !MaskStore.SelectedActiveMasks.HasFlag(ActiveMasks.TwinMask);
    }
}