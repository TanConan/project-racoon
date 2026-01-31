using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MaskOverlay : MaskListener
{
    public Volume volume;
    private Vignette vignette;

    [Header("Vignette")]
    public float vignetteSpeed;
    public float vignetteOff;
    public float vignetteOn;

    [Header("Overlays")]
    public List<RectTransform> RedBlueMaskOverlays;
    public Image crosshair;

    public override void MaskChange(ActiveMasks activeMasks)
    {
        RedBlueMaskOverlays.ForEach(o => o.gameObject.SetActive(activeMasks.HasFlag(ActiveMasks.RedBlueMask)));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity.Override(Mathf.MoveTowards(
            vignette.intensity.value,
            MaskStore.SelectedActiveMasks == ActiveMasks.NONE ? vignetteOff : vignetteOn,
            vignetteSpeed * Time.deltaTime
        ));
        ChangeCrosshairAlpha();
    }

    private void ChangeCrosshairAlpha()
    {
        Color color = crosshair.color;
        float wantedAlpha = GetForwardWeight(Vector3.forward, Camera.main.transform.forward, 25f, 35f);
        color.a = Mathf.MoveTowards(color.a, wantedAlpha, 2f * Time.deltaTime);
        crosshair.color = color;
    }

    private static float GetForwardWeight(Vector3 forward, Vector3 direction, float angleMin, float angleMax)
    {
        float angle = Vector3.Angle(direction, forward);

        if (angle <= angleMin) return 0f;
        if (angle >= angleMax) return 1f;

        return Mathf.InverseLerp(angleMin, angleMax, angle);
    }
}
