using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLinker : MonoBehaviour
{
  public List<DoorLinker> doors;

  private void Start()
  {
    if (doors == null) Debug.LogWarning("Warning: Pressure plate is not linked to a door.");
  }
    
  private void OnTriggerStay2D(Collider2D other)
  {
    Debug.Log("pressure plate enter");

    foreach (var door in doors) door.SetLocked(false);
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    Debug.Log("pressure plate exit");
    foreach (var door in doors) door.SetLocked(true);
  }
}