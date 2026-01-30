using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour {
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;
    private InputActionReference jumpAction;

    private void Awake() {
        //jumpAction.performed += ctx => Jump();
    }

    private void OnEnable() {
        //jumpAction.Enable();
    }

    private void OnDisable() {
        // jumpAction.Disable();
    }

    private void Jump() {
        if (rb != null && Mathf.Abs(rb.linearVelocity.y) < 0.01f) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}