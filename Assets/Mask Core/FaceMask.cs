using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FaceMask : MaskListener
{
  public Masks thisMask;

  private MeshRenderer _meshRenderer;

  public override void Awake()
  {
    base.Awake();
    _meshRenderer = GetComponent<MeshRenderer>();
  }

  public override void MaskChange(Masks mask)
  {
    _meshRenderer.enabled = (mask & thisMask) == thisMask;
  }

  public void OnToggleMask()
  {
    Masks currentMasks = mask.selected_masks;
    currentMasks ^= thisMask;
    mask.ChangeMask(currentMasks);
  }
}
