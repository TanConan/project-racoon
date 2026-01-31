using UnityEngine;

public class DoorLinker : MonoBehaviour
{
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public LayerMask collisionMask;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private bool closeASAP;

    public void Awake()
    {
        _collider = GetComponentInChildren<Collider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = lockedSprite;
    }

    private void Update()
    {
        if (closeASAP)
        {
            SetLocked(true);
        }
    }

    public void SetLocked(bool locked)
    {
        var hit = Physics2D.OverlapCircle(transform.position, 0.2f, collisionMask);

        if (hit == null || hit.gameObject == gameObject)
        {
            _collider.enabled = locked;
            _spriteRenderer.sprite = locked ? lockedSprite : unlockedSprite;
            gameObject.layer = locked ? LayerMask.NameToLayer("2D Wall") : LayerMask.NameToLayer("2D");
            closeASAP = locked;
        }
        else
        {
            closeASAP = true;
        }
    }
}