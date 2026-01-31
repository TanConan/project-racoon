using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLinker : MonoBehaviour
{
    public List<DoorLinker> doors;

    private int _count;

    private List<GameObject> _objects;

    private void Start()
    {
        if (doors == null) Debug.LogWarning("Warning: Pressure plate is not linked to a door.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("pressure plate enter " + other.name);
        _count += 1;
        if (_count == 1)
        {
            foreach (var door in doors)
            {
                if (door)
                {
                    door.IncreaseCurrentButtons();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _count -= 1;
        Debug.Log("pressure plate exit");
        if (_count == 0)
        {
            Debug.Log("pressure plate empty - locking doors");
            foreach (var door in doors)
            {
                if (door)
                {
                    door.DecreaseCurrentButtons();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("pressure plate enter");

        //foreach (var door in doors) door.SetLocked(false);
    }
}