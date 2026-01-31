using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour, InputSystem.I_2DPlayerActions
{
    [Header("Object References")]
    public GameObject movePoint;
    public LayerMask collisionMask;


    [Header("Movement finetuning")]
    public float movementSpeed = 0.5f;

    readonly float _gridStep = 1f;
    Animator _animator;

    private InputSystem inputSystem;
    private InputSystem._2DPlayerActions _2DPlayerActions;
    private Vector2 inputVector;
    public bool alreadyPushed;

    void OnEnable()
    {
        movePoint.transform.parent = null;
        _animator = GetComponent<Animator>();
        inputSystem = new();
        _2DPlayerActions = inputSystem._2DPlayer;
        _2DPlayerActions.AddCallbacks(this);
        _2DPlayerActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.transform.position) == 0)
        {
            alreadyPushed = false;
            DoMovement();
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
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
        Collider2D[] collidies = Physics2D.OverlapCircleAll(movePoint.transform.position + direction, 0.2f, collisionMask);
        foreach (Collider2D collider in collidies)
        {
            if (collider.GetComponent<Box>() && collider.gameObject != gameObject)
            {
                Vector3 boxPushDirection = collider.transform.position - transform.position;
                if (!alreadyPushed && boxPushDirection.magnitude == 1)
                {
                    alreadyPushed = true;
                    if (collider.GetComponent<Box>().PushBox(boxPushDirection))
                    {
                        movePoint.transform.position += direction;
                        inputVector = Vector2.zero;
                    }
                }
                return;
            }
        }
        if (!Physics2D.OverlapCircle(movePoint.transform.position + direction, 0.2f, collisionMask))
        {
            movePoint.transform.position += direction;
        }
    }
}
