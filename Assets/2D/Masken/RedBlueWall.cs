using UnityEngine;
using UnityEngine.Tilemaps;

public class RedBlueWall : MaskListener
{

    public bool isRedwall;
    private Collider2D _collider;
    private TilemapRenderer _tilemapRenderer;

    public override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider2D>();
        _tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    void Start()
    {
        FlipWallStatus(ActiveMasks.NONE);
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        FlipWallStatus(activeMasks);
    }

    private void FlipWallStatus(ActiveMasks activeMasks)
    {
        _collider.enabled = activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
        _tilemapRenderer.enabled =  activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
    }
}
