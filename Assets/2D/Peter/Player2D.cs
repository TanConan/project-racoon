using UnityEngine;

public class Player2D : MonoBehaviour
{


  public void Die()
  {
    Debug.Log("Player is dead");
    LevelManager.Instance.ReloadLevel();
  }
}
