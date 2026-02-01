using System.Collections;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Animator creditsAnimator;
    public Canvas canvas;
    private bool isRollingCredits;

    void Awake()
    {
        canvas.worldCamera = GameObject.Find("2D Camera").GetComponent<Camera>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRollingCredits) return;
        isRollingCredits = true;
        collision.GetComponent<GridMovement>().isMovementActive = false;
        collision.GetComponent<Animator>().SetTrigger("fall");
        StartCoroutine(CreditsRoutine());
    }

    private IEnumerator CreditsRoutine()
    {
        yield return new WaitForSeconds(3f);
        creditsAnimator.SetTrigger("credits");
    }
}
