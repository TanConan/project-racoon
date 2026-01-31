using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLinker : MonoBehaviour
{

    public List<DoorLinker> doors;

    private void Start()
    {
        if (doors == null)
        {
            Debug.LogWarning("Warning: Pressure plate is not linked to a door.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);

        foreach (var door in doors)
        {
            door.SetLocked(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        foreach (var door in doors)
        {
            door.SetLocked(false);
        }
    }
}
