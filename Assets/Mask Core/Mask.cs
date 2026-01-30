using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Mask", menuName = "Scriptable Objects/Mask")]
public class Mask : ScriptableObject
{
    private Masks selected_masks;

    public UnityEvent<Masks> maskChangedEvent = new();

    public void ChangeMask(Masks mask)
    {
        selected_masks = mask;
        maskChangedEvent.Invoke(selected_masks);
    }
}
