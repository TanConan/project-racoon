using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InvertController : MaskListener
{
  public UniversalRendererData rendererData;
  public string featureName;

  public void SetNegative(bool enabled)
  {
    if (rendererData == null) return;

    foreach (var feature in rendererData.rendererFeatures)
    {
      if (feature.name == featureName)
      {
        feature.SetActive(enabled);
      }
    }
  }

  public override void MaskChange(ActiveMasks activeMasks)
  {
    SetNegative(activeMasks.HasFlag(ActiveMasks.TwinMask));
  }
}
