using UnityEngine;

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
        if (!isRedwall) _spriteRenderer.color = Color.blue;
    }

    private void Start()
    {
        FlipWallStatus(MaskStore.SelectedActiveMasks);
    }

    public override void MaskChange(ActiveMasks activeMasks)
    {
        FlipWallStatus(activeMasks);
    }

    private void FlipWallStatus(ActiveMasks activeMasks)
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.zero, 0);

        if (hit)
        {
            if (hit.collider.GetComponent<Box>()) return;

            if (hit.collider.TryGetComponent<Death>(out var player))
            {
                // 2. Logik nur prüfen, WENN es ein Player ist
                var shouldDie = activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
                Debug.Log(player.gameObject.name);
                if (shouldDie)
                {
                    player.Die();
                    return;
                }
            }
        }

        _collider.enabled = activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
        _spriteRenderer.enabled = activeMasks.HasFlag(ActiveMasks.RedBlueMask) ? isRedwall : !isRedwall;
    }
}