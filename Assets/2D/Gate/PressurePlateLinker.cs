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

    private void OnTriggerEnter2D(Collider2D other)
    {
        door.SetLocked(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        door.SetLocked(true);
    }

}
