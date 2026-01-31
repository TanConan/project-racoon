using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    }
}
