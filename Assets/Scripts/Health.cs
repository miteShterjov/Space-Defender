using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private ParticleSystem boomVFX;
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private bool isPlayer;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (!cameraShake)
        {
            Debug.LogError("CameraShake component not found on the main camera");
        }

        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        if (!audioPlayer)
        {
            Debug.LogError("AudioPlayer component not found on the Health object");
        }
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        if (!scoreKeeper)
        {
            Debug.LogError("ScoreKeeper component not found on the Health object");
        }
    }

    public int GetPlayerHealth() => health;
    public void SetPlayerHealth(int healthValue) => health = healthValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        ShakeCamera();
        damageDealer.Hit();
    }

    private void ShakeCamera()
    {
        if (applyCameraShake)
        {
            cameraShake.ShakeCamera();
        }
    }

    private void TakeDamage(int damageValue)
    {
        health -= damageValue;
        audioPlayer.PlayDamageSound();
        if (health <= 0)
        {
            audioPlayer.PlayExplosionSound();
            Destroy(gameObject);

            if (CompareTag("Enemy"))
            {
                scoreKeeper.AddScore();
            }
        }
    }

    private void PlayHitEffect()
    {
        if (boomVFX)
        {
            ParticleSystem instance = Instantiate(boomVFX, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration);
        }
    }
}
