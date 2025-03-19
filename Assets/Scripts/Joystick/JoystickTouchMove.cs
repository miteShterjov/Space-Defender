using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickTouchMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private Vector2 inputVector;
    
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private void Start()
    {
        joystickOriginalPos = background.anchoredPosition;
        joystickRadius = background.sizeDelta.x / 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position - joystickOriginalPos;
        inputVector = (dragPos.magnitude > joystickRadius) ? dragPos.normalized : dragPos / joystickRadius;
        handle.anchoredPosition = inputVector * joystickRadius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() { return inputVector.x; }
    public float Vertical() { return inputVector.y; }
}
