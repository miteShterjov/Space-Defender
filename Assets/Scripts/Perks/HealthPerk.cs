using System;
using UnityEngine;

public class HealthPerk : MonoBehaviour, IPerk
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float delay = 10f;
    [SerializeField] private int healthPerk = 10;

    void Update()
    {
        MoveGameObject();
        OnBecameInvisible();
    }

    public void ApplyPerkEffect(GameObject player)
    {
        if (player.GetComponent<Health>().CurrentHealth == player.GetComponent<Health>().MaxHealth) return;
        player.GetComponent<Health>().AddPlayerHealth(healthPerk);
        Destroy(gameObject);
    }

    public void MoveGameObject()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject, delay);
    }
}
