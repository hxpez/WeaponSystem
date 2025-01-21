using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("Settings")] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField, Header("References")] private Rigidbody _rb;
    [SerializeField] private Transform _cameraTransform;

    private Vector2 _movementInput;
    private RaycastHit _hit;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    public void OnMOVE(CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnJUMP(CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector3 movementVector = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * new Vector3(_movementInput.x * _speed, 0, _movementInput.y * _speed);
        _rb.velocity = new Vector3(movementVector.x, _rb.velocity.y, movementVector.z);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _hit, 1)) return true;
        else return false;
    }
}
