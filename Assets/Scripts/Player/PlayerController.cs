using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;

    private Vector2 _movementInput;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    public void OnMOVE(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementVector = (transform.right * _movementInput.x + transform.forward * _movementInput.y).normalized;
        _rb.velocity = movementVector * _speed;
    }
}
