using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Object References")]
    public GameObject movePoint;
    public LayerMask collisionMask;


    [Header("Movement finetuning")]
    public float movementSpeed = 0.5f;

    readonly float _gridStep = 1f;

    Vector2 inputVector;


    void OnEnable()
    {
        movePoint.transform.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.transform.position) == 0)
        {
            DoMovement();
        }
    }

    private void DoMovement()
    {
        float x = inputVector.x;
        float y = inputVector.y;

        float minDistanceToMovementPoint = 0f;
        if (Mathf.Abs(x) > minDistanceToMovementPoint)
        {
            float direction = inputVector.x > 0 ? 1f : -1f;
            TryMove(new Vector3(direction * _gridStep, 0f, 0f));
            return;
        }


        if (Mathf.Abs(y) > minDistanceToMovementPoint)
        {
            float direction = inputVector.y > 0 ? 1f : -1f;
            TryMove(new Vector3(0f, direction * _gridStep, 0f));
            return;
        }

    }

    private void TryMove(Vector3 direction)
    {
        if (!Physics2D.OverlapCircle(movePoint.transform.position + direction, 0.2f, collisionMask))
        {
            movePoint.transform.position += direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inputVector = transform.position - collision.transform.position;
        Debug.Log(inputVector);
    }
}
