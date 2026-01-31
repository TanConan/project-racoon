using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDebug : MonoBehaviour, InputSystem.IDebugActions
{
  public Mask mask;

  private InputSystem inputSystem;
  private InputSystem.DebugActions debugActions;

  void Awake()
  {
    inputSystem = new();
    debugActions = inputSystem.Debug;
    debugActions.AddCallbacks(this);
    debugActions.Enable();
  }

  public void OnMask0(InputAction.CallbackContext context)
  {
    if (!context.performed) { return; }
    mask.ChangeMask(ActiveMasks.NONE);
  }

  public void OnMask1(InputAction.CallbackContext context)
  {
    if (!context.performed) { return; }
    mask.ChangeMask(mask.SelectedActiveMasks ^ ActiveMasks.RedBlueMask);
  }

  public void OnMask2(InputAction.CallbackContext context)
  {
    if (!context.performed) { return; }
    mask.ChangeMask(mask.SelectedActiveMasks ^ ActiveMasks.FIND);
  }

}
