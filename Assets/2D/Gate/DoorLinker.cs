using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorLinker : MonoBehaviour
{
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    public void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = lockedSprite;
    }

    public void SetLocked(bool locked)
    {
        _collider.enabled = locked;
        _spriteRenderer.sprite = locked ? lockedSprite : unlockedSprite;
        gameObject.layer = locked ? LayerMask.NameToLayer("2D Wall") : LayerMask.NameToLayer("2D");
    }
}
