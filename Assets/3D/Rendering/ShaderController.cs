using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ShaderController : MaskListener
{
  public UniversalRendererData rendererData;
  public string invertFeatureName;
  public string redBlueFeatureName;

  [Header("Transition Settings")]
  public Material crtMaterial;
  public Material invertMaterial;
  public Material redBlueMaterial;
  public float fadeSpeed = 2.0f;

  private Coroutine fadeCoroutineInvert;
  private Coroutine fadeCoroutineRB;
  private float currentIntensityInvert = 0f;
  private float currentIntensityPow = 1f;
  private float currentIntensityRB = 0f;

  public void OnDisable()
  {
    currentIntensityInvert = 0f;
    currentIntensityPow = 1f;
    currentIntensityRB = 0f;

    if (invertMaterial != null)
    {
      invertMaterial.SetFloat("_InvertIntensity", 0f);
      invertMaterial.SetFloat("_Power", 1f);
    }
    if (crtMaterial != null) crtMaterial.SetFloat("_InvertIntensity", 0f);
    if (redBlueMaterial != null) redBlueMaterial.SetFloat("_RedBlueIntensity", 0f);
  }

  public void SetNegative(bool enabled)
  {
    if (rendererData == null || invertMaterial == null || crtMaterial == null) return;

    if (fadeCoroutineInvert != null) StopCoroutine(fadeCoroutineInvert);
    fadeCoroutineInvert = StartCoroutine(FadeRoutineInvert(enabled));
  }

  public void SetRedBlue(bool enabled)
  {
    if (rendererData == null || redBlueMaterial == null) return;

    if (fadeCoroutineRB != null) StopCoroutine(fadeCoroutineRB);
    fadeCoroutineRB = StartCoroutine(FadeRoutineRB(enabled));
  }

  private IEnumerator FadeRoutineInvert(bool targetActive)
  {
    float targetValueInvert = targetActive ? 1.0f : 0.0f;
    float targetValuePow = targetActive ? 10.0f : 1.0f;

    while (!Mathf.Approximately(currentIntensityInvert, targetValueInvert)
        || !Mathf.Approximately(currentIntensityPow, targetValuePow)
    )
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

  private IEnumerator FadeRoutineRB(bool targetActive)
  {
    float targetValueRB = targetActive ? 5.0f : 0.0f;

    while (!Mathf.Approximately(currentIntensityRB, targetValueRB))
    {
      currentIntensityRB = Mathf.MoveTowards(currentIntensityRB, targetValueRB, 10 * fadeSpeed * Time.deltaTime);
      
      if (redBlueMaterial != null)
        redBlueMaterial.SetFloat("_RedBlueIntensity", currentIntensityRB);

      yield return null;
    }
  }

  public override void MaskChange(ActiveMasks activeMasks)
  {
    SetNegative(activeMasks.HasFlag(ActiveMasks.TwinMask));
    SetRedBlue(activeMasks.HasFlag(ActiveMasks.RedBlueMask));
  }
}