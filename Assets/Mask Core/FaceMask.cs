using UnityEngine;

public class FaceMask : MaskListener
{
  public Masks thisMask;

  public override void MaskChange(Masks mask)
  {
    Debug.Log(mask);
  }

  public void OnToggleMask()
  {
    Masks currentMasks = mask.selected_masks;
    currentMasks ^= thisMask;
    mask.ChangeMask(currentMasks);
  }
}
