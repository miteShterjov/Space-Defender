using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerMoveSpeed = 10f;

    [Header("Screen Padding")]
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    //input
    private Vector2 rawInput;

    //viewport bounds
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Shooter shooter;


    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        PlayerMove();
    }

    // Initializes the bounds based on the camera's viewport
    // This method is called once at the start of the game.
    // It calculates the minimum and maximum bounds of the player's movement area
    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f)); // Bottom-left corner
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f)); // Top-right corner
    }

    private void PlayerMove()
    {
        Vector2 delta = rawInput * Time.deltaTime * playerMoveSpeed;
        Vector2 newPosition = new Vector2();

        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPosition;
    }

    private void OnMove(InputValue inputValue)
    {
        rawInput = inputValue.Get<Vector2>();
    }

    private void OnFire(InputValue inputValue)
    {
        if (!shooter)
        {
            Debug.LogError("Shooter component not found");
            return;
        }

        shooter.IsFiring = inputValue.isPressed;
    }
    public void Shoot(bool isPressed)
    {
        shooter.IsFiring = isPressed;
    }
}
