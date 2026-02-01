using System;
using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool isDying;
    private Animator animator;
    private Soundboard2D soundboard2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        soundboard2D = FindAnyObjectByType<Soundboard2D>();
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;
        soundboard2D.PlaySound("Death");
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
        yield return new WaitForSeconds(2f);
        soundboard2D.PlaySound("Death");
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.ReloadLevel();
    }
}