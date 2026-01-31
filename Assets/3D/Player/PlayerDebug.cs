using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDebug : MonoBehaviour, InputSystem.IDebugActions
{
  public MaskStore maskStore;

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
    maskStore.ChangeMask(ActiveMasks.NONE);
  }

  public void OnMask1(InputAction.CallbackContext context)
  {
    if (!context.performed) { return; }
    maskStore.ChangeMask(maskStore.SelectedActiveMasks ^ ActiveMasks.RedBlueMask);
  }

  public void OnMask2(InputAction.CallbackContext context)
  {
    if (!context.performed) { return; }
    maskStore.ChangeMask(maskStore.SelectedActiveMasks ^ ActiveMasks.FIND);
  }

}
