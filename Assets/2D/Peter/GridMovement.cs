using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour, InputSystem.I_2DPlayerActions
{
    [Header("Object References")]
    public GameObject movePoint;
    public LayerMask collisionMask;


    [Header("Movement finetuning")]
    public float movementSpeed = 0.5f;

    readonly float _gridStep = 0.5f;
    Animator _animator;

    private InputSystem inputSystem;
    private InputSystem._2DPlayerActions _2DPlayerActions;
    private Vector2 inputVector;

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
        if (!Physics2D.OverlapCircle(movePoint.transform.position + direction, 0.2f, collisionMask))
        {
            movePoint.transform.position += direction;
        }
    }
}
