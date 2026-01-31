using UnityEngine;

public class Holes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<Player2D>())
        {
            collision.gameObject.GetComponent<Player2D>().Die();
        }
    }
}
