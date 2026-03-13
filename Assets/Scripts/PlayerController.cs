using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera mainCamera;

    [Header("Parameters")]
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float testLookMagnitude = 5.0f;

    // Private
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private Vector2 movementInput;
    private Vector2 aimInput;
    private Vector3 aimVector;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Aim");
        attackAction = InputSystem.actions.FindAction("Attack");
        jumpAction.performed += Jump;
        attackAction.performed += FireGrapple;
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        attackAction.performed -= FireGrapple;
    }

    private void Update()
    {
        movementInput = moveAction.ReadValue<Vector2>();
        aimInput = lookAction.ReadValue<Vector2>();
        aimVector = mainCamera.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, mainCamera.nearClipPlane));
    }

    private void FixedUpdate()
    {
        MoveCharacter(movementInput);
    }

    private void MoveCharacter(Vector2 movement)
    {
        transform.position += new Vector3(movement.x, 0.0f, 0.0f) * playerSpeed * Time.deltaTime;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Would jump if I had one.");
        rb.AddForce(new Vector2(0.0f, jumpForce));
    }

    private void FireGrapple(InputAction.CallbackContext context)
    {
        Debug.Log("Want to grapple");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = gameObject.transform.position;
        Vector3 target = aimVector;
        Gizmos.DrawSphere(origin, 0.1f);
        Gizmos.DrawSphere(target, 0.1f);
        Gizmos.DrawLine(origin, target);
    }
}
