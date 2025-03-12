using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerMoveSpeed = 10f;

    private Vector2 rawInput;

    //viewport bounds
    private Vector2 minBounds;
    private Vector2 maxBounds;

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
        Vector3 delta = rawInput * Time.deltaTime * playerMoveSpeed;
        Vector2 newPosition = new Vector2();

        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }

    private void OnMove(InputValue inputValue)
    {
        rawInput = inputValue.Get<Vector2>();
    }
}
