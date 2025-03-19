using UnityEngine;

public class PlayerJoystickMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private JoystickTouchMove joystick;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveDirection = new Vector2(joystick.Horizontal(), joystick.Vertical());
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
