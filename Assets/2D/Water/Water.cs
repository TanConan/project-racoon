using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] GameObject waterloggedBox;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void DropBoxInWater()
    {
        boxCollider.enabled = false;
        waterloggedBox.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            DropBoxInWater();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<Player2D>())
        {
            collision.gameObject.GetComponent<Player2D>().Die();
        }
    }
}
