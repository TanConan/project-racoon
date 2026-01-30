using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour
{
    [Header("Object References")]
    public GameObject movePoint;
    public LayerMask collisionMask;
    

    [Header("Movement finetuning")]
    public float movementSpeed = 0.5f;
    public float threshold = 0.5f;

    readonly float _gridStep = 1f;
    Animator _animator;
    PlayerInput _playerInput;
    InputAction _moveAction;
    
    void OnEnable()
    {
        movePoint.transform.parent = null;
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _moveAction.Enable();
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= threshold)
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
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
        float x = inputVector.x;
        float y = inputVector.y;

        if (Mathf.Abs(x) > 0.5f)
        {
            float direction = inputVector.x > 0 ? 1f : -1f;
            TryMove(new Vector3(direction * _gridStep, 0f, 0f));
            return;
        }
        
        
        if (Mathf.Abs(y) > 0.5f)
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
