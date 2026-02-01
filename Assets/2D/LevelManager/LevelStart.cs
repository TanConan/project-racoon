using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public Canvas canvas;

    void Awake()
    {
        canvas.worldCamera = GameObject.Find("2D Camera").GetComponent<Camera>();
    }

    void Start()
    {
        LevelManager.Instance.Player.isMovementActive = false;
    }
}
