using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Base class for handling pointer press input events.
/// </summary>
public abstract class PressInputBase : MonoBehaviour
{
    protected InputAction m_PressAction;

    protected virtual void Awake()
    {
        // Create a new input action for pointer press.
        m_PressAction = new InputAction("touch", binding: "<Pointer>/press");

        // Callback when the pointer press is initiated.
        m_PressAction.started += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPressBegan(device.position.ReadValue());
            }
        };

        // Callback when the pointer press is ongoing.
        m_PressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        // Callback when the pointer press is canceled or stopped.
        m_PressAction.canceled += _ => OnPressCancel();
    }

    protected virtual void OnEnable()
    {
        m_PressAction.Enable();
    }

    protected virtual void OnDisable()
    {
        m_PressAction.Disable();
    }

    protected virtual void OnDestroy()
    {
        m_PressAction.Dispose();
    }

    // Called when the pointer press is ongoing.
    protected virtual void OnPress(Vector3 position) { }

    // Called when the pointer press is initiated.
    protected virtual void OnPressBegan(Vector3 position) { }

    // Called when the pointer press is canceled or stopped.
    protected virtual void OnPressCancel() { }
}
