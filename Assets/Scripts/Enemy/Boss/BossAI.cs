using System;
using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // need 2 states for the boss, one is roaming and the other is attacking
    // the roaming state will move the boss around the map and the attacking state will attack the player
    // the boss will switch between these two states based on attack cooldown 

    public enum BossState { Roaming, Attacking }
    private BossState currentState;

    [Header("Attack Settings")]
    [SerializeField] private float attackDuration = 3f;
    [SerializeField] private float attackCooldown = 5f;
    private float attackTimer;
    private float cooldownTimer;

    [Header("Roaming Settings")]
    [SerializeField] private float roamSpeed = 2f;
    private Transform nextRoamPoint;
    private int currentRoamIndex = 0;

    void Start()
    {
        currentState = BossState.Attacking;
        attackTimer = attackDuration;
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Attacking:
                Attack();
                break;
            case BossState.Roaming:
                Roam();
                break;
        }
    }

    void Attack()
    {
        // need to replace with actual attacking logic
        // for now, just log to the console
        Debug.Log("Boss is ATTACKING!");
        GetComponent<BossShooter>().Attack();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            cooldownTimer = attackCooldown;
            currentState = BossState.Roaming;
        }
    }

    void Roam()
    {
        print("Boss is ROAMING!");
        Vector2 nextPos = GetNextRoamPosition();
        
        transform.position = Vector2.MoveTowards(transform.position, nextPos, roamSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPos) < 0.1f)
        {
            nextPos = GetNextRoamPosition();
        }

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            attackTimer = attackDuration;
            currentState = BossState.Attacking;
        }
    }

    private Vector2 GetNextRoamPosition()
    {
        float minX = -1.3f;
        float maxX = 1.3f;
        float minY = 0f;
        float maxY = 2f;
        print("Roaming to new position: " + new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY)));
        return new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
    }
}
