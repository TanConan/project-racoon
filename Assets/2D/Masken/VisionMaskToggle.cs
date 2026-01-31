using UnityEngine;
using UnityEngine.Tilemaps;

public class VisionMaskToggle : MaskListener
{
    private void Start()
    {
        ToggleVisibility(MaskStore.SelectedActiveMasks);
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        ToggleVisibility(activeMasks);
    }

    private void ToggleVisibility(ActiveMasks activeMasks)
    {
        GetComponent<TilemapRenderer>().enabled = activeMasks.HasFlag(ActiveMasks.VisionMask);
    }
}
