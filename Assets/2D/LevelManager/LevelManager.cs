using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInformation
{
    public float CamSize;
    public GameObject Level;
    public Transform SpawnPoint;

}

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager Instance;
    
    public Camera Camera2D;
    public GridMovement Player;
    
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
        foreach (var level in Levels)
        {
            Destroy(level.Level);
        }
        
        LoadLevel(CurrentLevelId);
    }


    public void LoadLevel(int levelId)
    {
        _currentLevel = Instantiate(Levels[levelId].Level, transform);
        Player.Teleport(Levels[levelId].SpawnPoint.position);
        Camera2D.orthographicSize = Levels[levelId].CamSize;

        CurrentLevelId = levelId;
    }

    public void ReloadLevel()
    {
        Destroy(_currentLevel);
        LoadLevel(CurrentLevelId);
    }
    
}
