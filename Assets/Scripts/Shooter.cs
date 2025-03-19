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

    private Coroutine firingCoroutine;

    public bool IsFiring { get; set; }

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
            rb.linearVelocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
