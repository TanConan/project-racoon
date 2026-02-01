using System.Collections;
using UnityEngine;

public class LampBehaviour : MonoBehaviour
{
  [Header("Zeit-Intervalle (Sekunden)")] public float pauseDurschnitt = 20f;

  public float pauseAbweichung = 5f;

  [Header("Flacker-Einstellungen")] public int minBlitze = 3;

  public int maxBlitze = 8;

  [Tooltip("Dauer eines einzelnen Aus-Zustands")]
  public float speedMin = 0.05f;

  public float speedMax = 0.25f;
  private Light _light;

  private void Start()
  {
    _light = GetComponent<Light>();

    if (_light != null)
      StartCoroutine(FlickerRoutine());
    else
      Debug.LogError("Keine 'Light' Komponente auf " + gameObject.name + " gefunden!");
  }

  private IEnumerator FlickerRoutine()
  {
    while (true)
    {
      var naechstePause = pauseDurschnitt + Random.Range(-pauseAbweichung, pauseAbweichung);
      yield return new WaitForSeconds(naechstePause);
      var blitze = Random.Range(minBlitze, maxBlitze);

      for (var i = 0; i < blitze; i++)
      {
        _light.enabled = false;
        yield return new WaitForSeconds(Random.Range(speedMin, speedMax));
        _light.enabled = true;
        yield return new WaitForSeconds(Random.Range(speedMin, speedMax));
      }

      _light.enabled = true;
    }
  }
}