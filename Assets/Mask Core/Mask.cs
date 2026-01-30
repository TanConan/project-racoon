using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Mask", menuName = "Scriptable Objects/Mask")]
public class Mask : ScriptableObject
{
  public Masks selected_masks { get; private set; }

  public UnityEvent<Masks> maskChangedEvent = new();

  public void ChangeMask(Masks mask)
  {
    selected_masks = mask;
    maskChangedEvent.Invoke(selected_masks);
  }
}
