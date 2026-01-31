using UnityEngine;

public class Box : MonoBehaviour
{
  [Header("Object References")] public GameObject movePoint;

  public LayerMask collisionMask;

  [Header("Movement finetuning")] public float movementSpeed = 5f; // Etwas höherer Wert für flüssigeres Grid-Gefühl

  private readonly float _gridStep = 1f;

  private void Update()
  {
    transform.position =
      Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);
  }

  private void OnEnable()
  {
    movePoint.transform.parent = null;
  }

  public bool PushBox(Vector3 direction)
  {
    if (Vector3.Distance(transform.position, movePoint.transform.position) > 0.05f) return false;

    var targetPos = movePoint.transform.position + direction;
    var hit = Physics2D.OverlapCircle(targetPos, 0.2f, collisionMask);

    if (hit != null)
    {
      var nextBox = hit.GetComponent<Box>();
      if (nextBox != null && nextBox != this)
        if (nextBox.PushBox(direction))
        {
          movePoint.transform.position += direction;
          return true;
        }

      return false;
    }

    // 3. Weg ist frei (kein Hit), movePoint sofort verschieben
    movePoint.transform.position += direction;
    return true;
  }

  public void DestroyWithMovePoint()
  {
    Destroy(gameObject);
    Destroy(movePoint);
  }
}