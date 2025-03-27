using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectile config")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 51f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private bool useByAI;

    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;
    public bool IsFiring { get; set; }

    void Awake()
    {
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        if (!audioPlayer)
        {
            Debug.LogError("AudioPlayer component not found on the Shooter object");
        }
    }

    void Start()
    {
        if (useByAI)
        {
            IsFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (IsFiring)
        {
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(FireCoroutine());
            }
        }
        else
        {
            if (firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Destroy(projectile, projectileLifeTime);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            
            if (useByAI) rb.linearVelocity = new Vector2(0, -projectileSpeed);
            else rb.linearVelocity = new Vector2(0, projectileSpeed);

            audioPlayer.PlayShootingSound();

            yield return new WaitForSeconds(GetEnemyRandomFireRate(fireRate));
        }
    }

    private float GetEnemyRandomFireRate(float fireRate)
    {
        float topRandomRange = 1.5f;
        return UnityEngine.Random.Range(fireRate, fireRate * topRandomRange);
    }
}
