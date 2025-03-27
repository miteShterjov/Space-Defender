using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButtonTouchUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Button fireButton;
    private Player player;
    private bool isPressed;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogError("Player not found in FireButtonUI script!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.Shoot(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.Shoot(false);
    }
}
