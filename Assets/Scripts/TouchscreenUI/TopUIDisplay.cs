using System;
using UnityEngine;
using UnityEngine.UI;

public class TopUIDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI killCountText;
    [SerializeField] private Slider healthBar;

    private Player player;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        if (!player)
        {
            Debug.LogError("Player object not found in the scene");
        }
    }

    void Start()
    {
        healthBar.maxValue = player.GetComponent<Health>().GetPlayerHealth();
    }

    void Update()
    {
        UpdateKillCount();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (player == null)
        {
            healthBar.value = 0;
            return;
        }

        healthBar.value = player.GetComponent<Health>().GetPlayerHealth();

    }

    private void UpdateKillCount()
    {
        killCountText.text = ScoreKeeper.Instance.GetScore().ToString("000000");
    }
}
