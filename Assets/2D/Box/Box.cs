using UnityEngine;

public class Box : MonoBehaviour
{
  [Header("Object References")]
  public GameObject movePoint;
  public LayerMask collisionMask;

  [Header("Movement finetuning")]
  public float movementSpeed = 5f; // Etwas höherer Wert für flüssigeres Grid-Gefühl
  readonly float _gridStep = 1f;

  void OnEnable()
  {
    movePoint.transform.parent = null;
  }

  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);
  }

  public bool PushBox(Vector3 direction)
  {
    if (Vector3.Distance(transform.position, movePoint.transform.position) > 0.05f)
    {
      return false;
    }

    Vector3 targetPos = movePoint.transform.position + direction;
    Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, collisionMask);

    if (hit != null)
    {
      Box nextBox = hit.GetComponent<Box>();
      if (nextBox != null && nextBox != this)
      {
        if (nextBox.PushBox(direction))
        {
          movePoint.transform.position += direction;
          return true;
        }
      }
      return false;
    }

    // 3. Weg ist frei (kein Hit), movePoint sofort verschieben
    movePoint.transform.position += direction;
    return true;
  }
}