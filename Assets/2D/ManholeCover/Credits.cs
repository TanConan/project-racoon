using System.Collections;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private bool isRollingCredits;

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
        LevelManager.Instance.ReloadLevel();
    }
}
