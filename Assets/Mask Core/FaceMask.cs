using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshRenderer))]
public class FaceMask : MaskListener
{
  [FormerlySerializedAs("thisMaskName")] [FormerlySerializedAs("thisMask")] public ActiveMasks thisActiveMasks;

  private MeshRenderer _meshRenderer;

  public override void Awake()
  {
    base.Awake();
    _meshRenderer = GetComponent<MeshRenderer>();
  }

  public override void MaskChange(ActiveMasks activeMasks)
  {
    _meshRenderer.enabled = (activeMasks & thisActiveMasks) == thisActiveMasks;
  }

  public void OnToggleMask()
  {
    ActiveMasks currentActiveMasks = mask.SelectedActiveMasks;
    currentActiveMasks ^= thisActiveMasks;
    mask.ChangeMask(currentActiveMasks);
  }
}
