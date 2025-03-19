using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        
        TakeDamage(damageDealer.GetDamage());
        damageDealer.Hit();
    }

    private void TakeDamage(int damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
