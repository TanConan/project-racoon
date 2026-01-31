using UnityEngine;

public class Pause : MonoBehaviour, InputSystem.IUIActions
{
    private InputSystem inputSystem;
    private InputSystem.UIActions UIActions;

    public RectTransform pauseOverlay;

    void Awake()
    {
        inputSystem = new();
        UIActions = inputSystem.UI;
        UIActions.AddCallbacks(this);
    }

    public void OnCancel(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }

    public void OnNavigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }

    public void OnRightClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }

    public void OnSubmit(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
    }

    public void UIContinue()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UISettings()
    {

    }

    public void UIQuit()
    {
        Application.Quit();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            UIActions.Enable();
            pauseOverlay.gameObject.SetActive(true);
        }
        else
        {
            UIActions.Disable();
            pauseOverlay.gameObject.SetActive(false);
        }
    }
}
