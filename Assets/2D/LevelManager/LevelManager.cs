using System.Collections;
using System.Collections.Generic;
using TMPro;
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
  public TMP_Text levelText;

  public List<LevelInformation> Levels;

  public int CurrentLevelId = 0;
  public int RED_BLUE_MASK_LEVEL_ID;
  public int TWIN_MASK_LEVEL_ID;

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
    if (Player.GetComponent<Death>().isDying)
    {
      if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("peter_fall_new"))
      {
        Player.GetComponent<Animator>().SetTrigger("revive");
      }
      Player.GetComponent<Death>().isDying = false;
    }
    Camera2D.orthographicSize = Levels[CurrentLevelId].CamSize;
    levelText.text = string.Format("LEVEL {0:D2}", CurrentLevelId + 1);
    FindFirstObjectByType<TwinMask>().TwinMaskLogic();
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
    if (CurrentLevelId == RED_BLUE_MASK_LEVEL_ID)
    {
      ShowMaskTutorial();
      GameObject parent = GameObject.Find("Desk");
      GameObject mask = parent.transform.Find("RedBlueMask").gameObject;
      mask.SetActive(true);
    }
    if (CurrentLevelId == TWIN_MASK_LEVEL_ID)
    {
      GameObject parent = GameObject.Find("Desk");
      Debug.Log(parent);
      GameObject mask = parent.transform.Find("TwinMask").gameObject;
      Debug.Log(mask);
      mask.SetActive(true);
    }
  }

  private void ShowMaskTutorial()
  {
    Tutorial.Instance.Show(TutorialText.LOOK);
    Tutorial.Instance.Show(TutorialText.ZOOM);
    Tutorial.Instance.Show(TutorialText.INTERACT);
  }
}
