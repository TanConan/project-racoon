using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
  Vector2 DeltaPointer;

  public void LookAround(InputAction.CallbackContext context)
  {
    DeltaPointer = context.ReadValue<Vector2>();
  }

  private void Update()
  {
    this.transform.Rotate(new Vector3(0f, DeltaPointer.x, 0f), Space.World);
    this.transform.Rotate(new Vector3(-DeltaPointer.y, 0f, 0f), Space.Self);
  }
}
