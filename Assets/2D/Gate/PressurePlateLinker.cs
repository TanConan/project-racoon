using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLinker : MonoBehaviour
{
    public List<DoorLinker> doors;

    private int _count;

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        if (_count == 0)
        {
            foreach (var door in doors)
            {
                if (door)
                {
                    door.DecreaseCurrentButtons();
                }
            }
        }
    }
}