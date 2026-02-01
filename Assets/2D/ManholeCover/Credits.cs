using System.Collections;
using UnityEngine;

public class Credits : MonoBehaviour
{
  public Soundboard2D soundboard2D;
  public Animator creditsAnimator;
  public Canvas canvas;
  private bool isRollingCredits;

  private void Awake()
  {
    canvas.worldCamera = GameObject.Find("2D Camera").GetComponent<Camera>();
    soundboard2D = GameObject.Find("SoundBites").GetComponent<Soundboard2D>();
  }

  private void Start()
  {
    StartCoroutine(ManholeSound());
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (isRollingCredits) return;
    isRollingCredits = true;
    collision.GetComponent<GridMovement>().isMovementActive = false;
    collision.GetComponent<Animator>().SetTrigger("fall");
    StartCoroutine(CreditsRoutine());
  }

  private IEnumerator ManholeSound()
  {
    yield return new WaitForSeconds(1.5f);
    soundboard2D.PlaySound("Manhole");
  }

  private IEnumerator CreditsRoutine()
  {
    yield return new WaitForSeconds(3f);
    creditsAnimator.SetTrigger("credits");
  }
}