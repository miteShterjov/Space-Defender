using System;
using System.Collections;
//using System.Numerics;
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

    [Header("Roaming Settings")]
    [SerializeField] private float roamSpeed = 2f;
    [SerializeField] private float cooldownTimer = 3f;
    private Vector2 currentPos;
    private Vector2 nextPos;
    private Vector2 startingPos = new Vector2(0.07f,2.32f);

    void Start()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPos, roamSpeed * Time.deltaTime);
        nextPos = GetNextRoamPosition(currentPos);
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Roaming:
                Roam();
                break;
            case BossState.Attacking:
                Attack();
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
        float delay = 1f;
        print("Boss is ROAMING!");

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            attackTimer = attackDuration;
            currentState = BossState.Attacking;
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, roamSpeed * Time.deltaTime);
        StartCoroutine(SmallBreakCourutine(delay));

        if (Vector2.Distance(transform.position, nextPos) < 0.4f)
        {
            nextPos = GetNextRoamPosition(currentPos);
            transform.position = Vector2.MoveTowards(transform.position, nextPos, roamSpeed * Time.deltaTime);
            
        }

    }

    private IEnumerator SmallBreakCourutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("1 second has passed.");
    }

    private Vector2 GetNextRoamPosition(Vector2 currentPos)
    {
        Vector2[] positions = new Vector2[8];
        positions[0] = new Vector2(0.14f, 2.41f);
        positions[1] = new Vector2(-1.31f, 0.65f);
        positions[2] = new Vector2(1.49f, 0.9f);
        positions[3] = new Vector2(-0.5f, 1.5f);
        positions[4] = new Vector2(1.32f, 2.39f);
        positions[5] = new Vector2(1.57f, 2.11f);
        positions[6] = new Vector2(1.7f, 1.26f);
        positions[7] = new Vector2(1.04f, 1.27f);

        Vector2 nextPos = positions[UnityEngine.Random.Range(0, positions.Length)];
        // check if the next position is the same as the current position
        while (nextPos == currentPos)
        {
            nextPos = positions[UnityEngine.Random.Range(0, positions.Length)];
        }

        return nextPos;



    }
}
