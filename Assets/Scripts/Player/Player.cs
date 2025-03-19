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

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
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
}
