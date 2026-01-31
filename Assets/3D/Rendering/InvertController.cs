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
    private float currentIntensity = 0f;

    public void SetNegative(bool enabled)
    {
        if (rendererData == null || invertMaterial == null) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(enabled));
    }

    private IEnumerator FadeRoutine(bool targetActive)
    {
        float targetValue = targetActive ? 1.0f : 0.0f;

        while (!Mathf.Approximately(currentIntensity, targetValue))
        {
            currentIntensity = Mathf.MoveTowards(currentIntensity, targetValue, fadeSpeed * Time.deltaTime);

            if (invertMaterial != null)
                invertMaterial.SetFloat("_InvertIntensity", currentIntensity);

            if (crtMaterial != null)
                crtMaterial.SetFloat("_InvertIntensity", currentIntensity);

            yield return null;
        }
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        SetNegative(activeMasks.HasFlag(ActiveMasks.TwinMask));
    }
}