using UnityEngine;

public abstract class MaskListener : MonoBehaviour
{
  protected static MaskStore MaskStore;

  public virtual void Awake()
  {
    if (MaskStore == null)
    {
      MaskStore = Resources.Load<MaskStore>("MaskStore");
    }
  }

  void OnEnable()
  {
    MaskStore.maskChangedEvent.AddListener(MaskChange);
  }

  void OnDisable()
  {
    MaskStore.maskChangedEvent.RemoveListener(MaskChange);
  }

  public abstract void MaskChange(ActiveMasks activeMasks);
}
