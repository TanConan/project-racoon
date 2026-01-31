using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FaceMask : MaskListener
{
  public ActiveMasks thisMask;

  private MeshRenderer _meshRenderer;

  public override void Awake()
  {
    base.Awake();
    _meshRenderer = GetComponent<MeshRenderer>();
  }

  public override void MaskChange(ActiveMasks mask)
  {
    _meshRenderer.enabled = (mask & thisMask) == thisMask;
  }

  public void OnToggleMask()
  {
    ActiveMasks currentMasks = mask.SelectedActiveMasks;
    currentMasks ^= thisMask;
    mask.ChangeMask(currentMasks);
  }
}
