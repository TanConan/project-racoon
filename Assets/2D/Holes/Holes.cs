using System.Collections;
using UnityEngine;

public class Holes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("FallTrigger");
            StartCoroutine(DelayedDestroyBox(collision.gameObject));
        }

        if (collision.gameObject.GetComponent<Death>())
        {
            collision.gameObject.GetComponent<Death>().FallDie();
        }
    }

    public IEnumerator DelayedDestroyBox(GameObject box)
    {
        yield return new WaitForSeconds(1);
        Destroy(box);
    }
}
