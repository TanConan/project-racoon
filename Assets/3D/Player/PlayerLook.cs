using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour, InputSystem.I_3DPlayerActions
{
    private InputSystem inputSystem;
    private InputSystem._3DPlayerActions _3DPlayerActions;

    Vector2 deltaPointer;
    float verticalRotation = 0f;
    float horizontalRotation = 0f;

    public MaskStore maskStore;
    ActiveMasks unlockedMasks;

    [Header("Look Settings")]
    [SerializeField]
    private float sensitivity = 0.1f;
    [SerializeField]
    private float yLookLimit = 50f;
    [SerializeField]
    private float xLookLimit = 30f;

    [Header("Zoom Settings")]
    [SerializeField]
    private float fovChangeSpeed;
    [SerializeField]
    private float fovNormal;
    [SerializeField]
    private float fovScreen;
    [SerializeField]
    private AnimationCurve fovCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private float wantedFOV;
    private bool isScreenFov = false;
    private float fovInterpolate = 0;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out FaceMask mask))
            {
                mask.OnToggleMask();
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        deltaPointer = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnToggleMask0(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!unlockedMasks.HasFlag(ActiveMasks.RedBlueMask)) return;
        maskStore.ToggleMask(ActiveMasks.RedBlueMask);
    }

    public void OnToggleMask1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!unlockedMasks.HasFlag(ActiveMasks.FIND)) return;
        maskStore.ToggleMask(ActiveMasks.FIND);
    }

    void Awake()
    {
        inputSystem = new();
        _3DPlayerActions = inputSystem._3DPlayer;
        _3DPlayerActions.AddCallbacks(this);
        wantedFOV = fovNormal;
        // TODO REMOVE
        unlockedMasks = ActiveMasks.RedBlueMask | ActiveMasks.FIND;
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _3DPlayerActions.Enable();
        }
        else
        {
            _3DPlayerActions.Disable();
        }

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

        fovInterpolate += fovChangeSpeed * Time.deltaTime;
        float curveValue = fovCurve.Evaluate(fovInterpolate);

        Camera.main.fieldOfView = Mathf.Lerp(wantedFOV == fovNormal ? fovScreen : fovNormal, wantedFOV, curveValue);
    }

    public void UnlockMask(ActiveMasks newMask)
    {
        unlockedMasks |= newMask;
    }

    public void OnChangeFov(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        wantedFOV = isScreenFov ? fovNormal : fovScreen;
        isScreenFov = !isScreenFov;
        fovInterpolate = 0;
    }

}
