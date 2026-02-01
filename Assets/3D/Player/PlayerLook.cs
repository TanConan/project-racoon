using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour, InputSystem.I_3DPlayerActions
{
    public MaskStore maskStore;

    [Header("Look Settings")]
    [SerializeField]
    private float sensitivity = 0.1f;

    [SerializeField] private float yLookLimit = 50f;

    [SerializeField] private float xLookLimit = 30f;

    [Header("Flaky Light")] public Light LampLight;
    public AnimationCurve flashCurve; // Eine Kurve von 1 nach 0 (Decay)
    public float flashDuration = 0.1f;
    public int minFlashes = 3;
    public int maxFlashes = 8;
    public float pauseMin = 0.05f;
    public float pauseMax = 0.25f;


    [Header("Zoom Settings")]
    [SerializeField]
    private float fovChangeSpeed;

    [SerializeField] private float fovNormal;

    [SerializeField] private float fovScreen;

    [SerializeField] private AnimationCurve fovCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private InputSystem._3DPlayerActions _3DPlayerActions;

    private Vector2 deltaPointer;
    private float fovInterpolate;
    private float horizontalRotation;
    private InputSystem inputSystem;
    private bool isScreenFov;
    private ActiveMasks unlockedMasks;
    private float verticalRotation;
    private float wantedFOV;

    private void Awake()
    {
        inputSystem = new InputSystem();
        _3DPlayerActions = inputSystem._3DPlayer;
        _3DPlayerActions.AddCallbacks(this);
        wantedFOV = fovNormal;
        // Debug unlocking all masks
        // unlockedMasks = ActiveMasks.RedBlueMask | ActiveMasks.TwinMask;
    }

    private void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            _3DPlayerActions.Enable();
        else
            _3DPlayerActions.Disable();

        var mouseX = deltaPointer.x * sensitivity;
        horizontalRotation += mouseX;

        var mouseY = deltaPointer.y * sensitivity;
        verticalRotation -= mouseY;

        horizontalRotation = Mathf.Clamp(horizontalRotation, -xLookLimit, xLookLimit);
        verticalRotation = Mathf.Clamp(verticalRotation, -yLookLimit, yLookLimit);

        var currentRotation = transform.localEulerAngles;
        currentRotation.x = verticalRotation;
        currentRotation.y = horizontalRotation;
        transform.localEulerAngles = currentRotation;

        fovInterpolate += fovChangeSpeed * Time.deltaTime;
        var curveValue = fovCurve.Evaluate(fovInterpolate);

        Camera.main.fieldOfView = Mathf.Lerp(wantedFOV == fovNormal ? fovScreen : fovNormal, wantedFOV, curveValue);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out FaceMask mask))
            {
                mask.OnToggleMask();
            }
            else if (LevelManager.Instance.CurrentLevelId == 0 && hit.collider.name == "Screen")
            {
                LevelManager.Instance.NextLevel();
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
        if (!unlockedMasks.HasFlag(ActiveMasks.TwinMask)) return;
        maskStore.ToggleMask(ActiveMasks.TwinMask);
    }

    public void OnResetLightEffect(InputAction.CallbackContext context)
    {
        StartCoroutine(FlickerRoutine());
    }

    public void OnChangeFov(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        wantedFOV = isScreenFov ? fovNormal : fovScreen;
        isScreenFov = !isScreenFov;
        fovInterpolate = 0;
    }

    private IEnumerator FlickerRoutine()
    {
        var amountFlashes = Random.Range(minFlashes, maxFlashes);

        for (var i = 0; i < amountFlashes; i++)
        {
            var elapsed = 0f;
            while (elapsed < flashDuration)
            {
                elapsed += Time.deltaTime;
                var normalizedTime = elapsed / flashDuration;
                LampLight.intensity = flashCurve.Evaluate(normalizedTime);
                yield return null;
            }

            LampLight.intensity = 0;

            yield return new WaitForSeconds(Random.Range(pauseMin, pauseMax));
        }
    }

    private IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(5);
        Tutorial.Instance.Show(TutorialText.ZOOM);
        Tutorial.Instance.Show(TutorialText.WASD);
        Tutorial.Instance.Show(TutorialText.RESET);
    }

    public void UnlockMask(ActiveMasks newMask)
    {
        unlockedMasks |= newMask;
    }
}