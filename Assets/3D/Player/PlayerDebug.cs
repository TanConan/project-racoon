using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDebug : MonoBehaviour
{
  public Mask mask;
  public void PutOnMask0(InputAction.CallbackContext context)
  {
    mask.ChangeMask(Masks.NONE);
  }

  public void PutOnMask1(InputAction.CallbackContext context)
  {
    mask.ChangeMask(mask.selected_masks ^ Masks.DISAPPEAR);
  }

  public void PutOnMask2(InputAction.CallbackContext context)
  {
    mask.ChangeMask(mask.selected_masks ^ Masks.FIND);
  }

}
