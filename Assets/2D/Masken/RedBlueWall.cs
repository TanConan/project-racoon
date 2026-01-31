using UnityEngine;
using UnityEngine.Tilemaps;

public class RedBlueWall : MaskListener
{

    public bool isRedwall;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    public override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _spriteRenderer.color = Color.red;
        if (!isRedwall)
        {
            _spriteRenderer.color = Color.blue;
        }
    }

    void Start()
    {
        FlipWallStatus(MaskStore.SelectedActiveMasks);
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        FlipWallStatus(activeMasks);
    }

    private void FlipWallStatus(ActiveMasks activeMasks)
    {
        _collider.enabled = activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
        _spriteRenderer.enabled =  activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
    }
}
