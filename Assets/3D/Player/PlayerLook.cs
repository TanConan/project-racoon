using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
  Vector2 deltaPointer;
  float verticalRotation = 0f;
  float horizontalRotation = 0f;

  [Header("Settings")]
  [SerializeField]
  private float sensitivity = 0.1f;
  [SerializeField]
  private float yLookLimit = 50f;
  [SerializeField]
  private float xLookLimit = 30f;

  public void LookAround(InputAction.CallbackContext context)
  {
    deltaPointer = context.ReadValue<Vector2>();
  }

  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  private void Update()
  {
    float mouseX = deltaPointer.x * sensitivity;
    horizontalRotation += mouseX;

    float mouseY = deltaPointer.y * sensitivity;
    verticalRotation -= mouseY;

    horizontalRotation = Mathf.Clamp(horizontalRotation, -xLookLimit, xLookLimit);
    verticalRotation = Mathf.Clamp(verticalRotation, -yLookLimit, yLookLimit);

    Vector3 currentRotation = transform.localEulerAngles;
    currentRotation.x = verticalRotation;
    currentRotation.y = horizontalRotation;
    transform.localEulerAngles = currentRotation;
  }
}
