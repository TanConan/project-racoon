using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class InvertController : MaskListener
{
  public UniversalRendererData rendererData;
  public string featureName = "Negative Effect";
  public Material crtMaterial;

  [Header("Transition Settings")]
  public Material invertMaterial;
  public float fadeSpeed = 2.0f;

  private Coroutine fadeCoroutine;
  private float currentIntensityInvert = 0f;
  private float currentIntensityPow = 1f;

  public void OnDisable()
  {
    currentIntensityInvert = 0f;

    if (invertMaterial != null)
    {
      invertMaterial.SetFloat("_InvertIntensity", 0f);
      invertMaterial.SetFloat("_Power", 1f);
    }
    if (crtMaterial != null) crtMaterial.SetFloat("_InvertIntensity", 0f);
  }

  public void SetNegative(bool enabled)
  {
    if (rendererData == null || invertMaterial == null) return;

    if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
    fadeCoroutine = StartCoroutine(FadeRoutine(enabled));
  }

  private IEnumerator FadeRoutine(bool targetActive)
  {
    float targetValueInvert = targetActive ? 1.0f : 0.0f;
    float targetValuePow = targetActive ? 10.0f : 1.0f;

    while (!Mathf.Approximately(currentIntensityInvert, targetValueInvert) || !Mathf.Approximately(currentIntensityPow, targetValuePow))
    {
      currentIntensityInvert = Mathf.MoveTowards(currentIntensityInvert, targetValueInvert, fadeSpeed * Time.deltaTime);
      currentIntensityPow = Mathf.MoveTowards(currentIntensityPow, targetValuePow, 10 * fadeSpeed * Time.deltaTime);

      if (invertMaterial != null)
      {
        invertMaterial.SetFloat("_InvertIntensity", currentIntensityInvert);
        invertMaterial.SetFloat("_Power", currentIntensityPow);
      }

      if (crtMaterial != null)
        crtMaterial.SetFloat("_InvertIntensity", currentIntensityInvert);


      yield return null;
    }
  }

  public override void MaskChange(ActiveMasks activeMasks)
  {
    SetNegative(activeMasks.HasFlag(ActiveMasks.TwinMask));
  }
}