using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Mask", menuName = "Scriptable Objects/Mask")]
public class MaskStore : ScriptableObject
{
    public ActiveMasks SelectedActiveMasks { get; private set; }

    public UnityEvent<ActiveMasks> maskChangedEvent = new();

    public void ChangeMask(ActiveMasks activeMasks)
    {
        SelectedActiveMasks = activeMasks;
        maskChangedEvent.Invoke(SelectedActiveMasks);
    }

    public void ToggleMask(ActiveMasks toggleMask)
    {
        SelectedActiveMasks ^= toggleMask;
        maskChangedEvent.Invoke(SelectedActiveMasks);
    }

    void OnDisable()
    {
        SelectedActiveMasks = ActiveMasks.NONE;
    }
}
