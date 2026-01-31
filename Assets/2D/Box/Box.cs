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
    public bool alreadyPushed;


    void OnEnable()
    {
        movePoint.transform.parent = null;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, movePoint.transform.position) == 0)
        {
            alreadyPushed = false;
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
        inputVector = Vector2.zero;
    }

    public bool PushBox(Vector3 direction)
    {
        Debug.Log(direction);

        if (Vector3.Distance(transform.position, movePoint.transform.position) == 0)
        {
            inputVector = direction;
            Collider2D[] collidies = Physics2D.OverlapCircleAll(movePoint.transform.position + direction, 0.2f, collisionMask);
            foreach (Collider2D collider in collidies)
            {
                if (collider.GetComponent<Box>() && collider.gameObject != gameObject)
                {
                    Vector2 boxPushDirection = collider.transform.position - transform.position;
                    if (!alreadyPushed && boxPushDirection.magnitude == 1)
                    {
                        alreadyPushed = true;
                        return collider.GetComponent<Box>().PushBox(boxPushDirection);
                    }
                }
            }
        }
        return !Physics2D.OverlapCircle(movePoint.transform.position + direction, 0.2f, collisionMask);
    }
}
