using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _cameraTransform;

    private bool _isJumping;
    
    private Vector2 _movementInput;
    RaycastHit hit;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!_isJumping) Move();
    }

    public void OnMOVE(CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnJUMP(CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            Jump();
        }
    }

    private void Move()
    {
        //Vector3 movementVector = (transform.right * _movementInput.x + transform.forward * _movementInput.y).normalized;
        //_rb.velocity = movementVector * _speed;

        Vector3 movementVector = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * new Vector3(_movementInput.x * _speed, 0, _movementInput.y * _speed);
        _rb.velocity = new Vector3(movementVector.x, _rb.velocity.y, movementVector.z);
        //_rb.MovePosition(movementVector * _speed);
        //_rb.AddForce(movementVector * _speed);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        //_rb.velocity = Vector3.up * _jumpForce;
        Debug.Log("tried to jump");
    }

    private bool IsGrounded()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, 1 << 3)) return true;
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
    }
}
