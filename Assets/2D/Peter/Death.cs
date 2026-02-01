using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator peterAnimator;
    private GridMovement player;

    public bool isDying;

    void Awake()
    {
        peterAnimator = GetComponent<Animator>();
        player = GetComponent<GridMovement>();
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;
        player.isMovementActive = false;
        LevelManager.Instance.ReloadLevel();
    }

    public void FallDie()
    {
        if (isDying) return;
        isDying = true;
        player.isMovementActive = false;
        peterAnimator.SetTrigger("fall");
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(3f);
        LevelManager.Instance.ReloadLevel();
    }
}
