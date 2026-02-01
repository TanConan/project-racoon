using System;
using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;

    public bool isDying;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;
        Array.ForEach(FindObjectsByType<GridMovement>(FindObjectsSortMode.None), gm => gm.isMovementActive = false);
        LevelManager.Instance.ReloadLevel();
    }

    public void FallDie()
    {
        if (isDying) return;
        isDying = true;
        Array.ForEach(FindObjectsByType<GridMovement>(FindObjectsSortMode.None), gm => gm.isMovementActive = false);
        animator.SetTrigger("fall");
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(3f);
        LevelManager.Instance.ReloadLevel();
    }
}
