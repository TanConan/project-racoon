using System;
using UnityEngine;

public class PressurePlateLinker : MonoBehaviour
{

    public DoorLinker door;

    private void Start()
    {
        if (door == null)
        {
            Debug.LogWarning("Warning: Pressure plate is not linked to a door.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        door.SetLocked(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.name);
        door.SetLocked(false);
    }
}
