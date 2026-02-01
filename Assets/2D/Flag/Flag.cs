using UnityEngine;

public class Flag : MonoBehaviour
{
  private Soundboard2D _soundboard2D;

  public void Awake()
  {
    _soundboard2D = GameObject.Find("SoundBites").GetComponent<Soundboard2D>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!other.CompareTag("Player")) return;
    _soundboard2D.PlaySound("Win");
    LevelManager.Instance.NextLevel();
  }
}