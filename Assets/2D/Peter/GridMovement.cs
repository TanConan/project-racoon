using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour, InputSystem.I_2DPlayerActions
{
    [Header("Object References")]
    public GameObject movePoint;
    public LayerMask collisionMask;

    [Header("Movement finetuning")]
    public float movementSpeed = 5f;

    private readonly float _gridStep = 1f;
    private Animator _animator;
    private InputSystem _inputSystem;
    private Vector2 _inputVector;

    void OnEnable()
    {
        movePoint.transform.parent = null;
        _animator = GetComponent<Animator>();
        _inputSystem = new InputSystem();
        _inputSystem._2DPlayer.AddCallbacks(this);
        _inputSystem._2DPlayer.Enable();
    }

    void OnDisable()
    {
        _inputSystem._2DPlayer.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);

        bool moving = Vector3.Distance(transform.position, movePoint.transform.position) > 0.01f;
        _animator.SetBool("isMoving", moving);

        if (!moving)
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        if (_inputVector == Vector2.zero) return;

        Vector3 direction = Vector3.zero;

        if (Mathf.Abs(_inputVector.x) > 0.5f)
        {
            direction = new Vector3(_inputVector.x > 0 ? _gridStep : -_gridStep, 0f, 0f);
        }
        else if (Mathf.Abs(_inputVector.y) > 0.5f)
        {
            direction = new Vector3(0f, _inputVector.y > 0 ? _gridStep : -_gridStep, 0f);
        }

        if (direction != Vector3.zero)
        {
            TryMove(direction);
        }
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 targetPos = movePoint.transform.position + direction;
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, collisionMask);

        if (hit != null)
        {
            Box box = hit.GetComponent<Box>();
            if (box != null)
            {
                if (box.PushBox(direction))
                {
                    movePoint.transform.position = targetPos;
                }
            }
        }
        else
        {
            movePoint.transform.position = targetPos;
        }
    }
    
    public void Teleport(Vector3 position)
    {
        transform.position = position;
        movePoint.transform.position = position;
    }
}