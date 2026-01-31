using UnityEngine;

public class TwinMask : MaskListener
{
  public static bool TwinMaskOn;


  public override void MaskChange(ActiveMasks activeMasks)
  {
    if (activeMasks.HasFlag(ActiveMasks.TwinMask) == TwinMaskOn) return;

    TwinMaskOn = !TwinMaskOn;
    // Twin mask changed
    Debug.Log("twin mask on: " + TwinMaskOn);
    var twin = GameObject.FindWithTag("Twin");
    if (twin == null)
    {
      Debug.Log("twin not found");
      return;
    }

    twin.GetComponent<GridMovement>().isMovementActive = TwinMaskOn;
    LevelManager.Instance.Player.GetComponent<GridMovement>().isMovementActive = !TwinMaskOn;
  }
}