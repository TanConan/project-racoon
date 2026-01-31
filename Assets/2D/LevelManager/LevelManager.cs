using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInformation
{
    public float CamSize;
    public GameObject Level;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Camera Camera2D;
    public GridMovement Player;
    public Animator resetAnimator;

    public List<LevelInformation> Levels;

    public int CurrentLevelId = 0;

    private GameObject _currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(_currentLevel);
        yield return new WaitForEndOfFrame();
        _currentLevel = Instantiate(Levels[CurrentLevelId].Level, transform);
        Player.Teleport(GameObject.FindWithTag("Respawn").transform.position);
        Camera2D.orthographicSize = Levels[CurrentLevelId].CamSize;
    }

    public void ReloadLevel()
    {
        resetAnimator.SetTrigger("reset");
        StartCoroutine(LoadLevel());
    }

    public void NextLevel()
    {
        CurrentLevelId++;
        StartCoroutine(LoadLevel());
    }
}
