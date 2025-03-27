using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private ParticleSystem boomVFX;
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private bool isPlayer;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;
    private LevelManager levelManager;
    private Player player;
    private Transform playerShield;
    private ShieldPerkActive playeShieldPerk;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

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
        levelManager = FindAnyObjectByType<LevelManager>();
        if (!levelManager)
        {
            Debug.LogError("LevelManager component not found on the Health object");
        }
        player = FindAnyObjectByType<Player>();
    }
    void Update()
    {
        if (player == null) levelManager.LoadGameOver(1);
    }

    public int GetPlayerHealth() => CurrentHealth;
    public void SetPlayerHealth(int healthValue) => CurrentHealth = healthValue;
    public void AddPlayerHealth(int healthValue)
    {
        CurrentHealth += healthValue;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer && playerShield == null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
        if (damageDealer && playerShield != null)
        {
            playeShieldPerk.HasShieldUp = true;
            playeShieldPerk.ShieldMaxHp -= damageDealer.GetDamage();
            if (playeShieldPerk.ShieldMaxHp <= 0)
            {
                playeShieldPerk.ShieldSpriteActive(-1);
                playeShieldPerk.HasShieldUp = false;
                Destroy(playerShield.gameObject);
            }
            damageDealer.Hit();
        }

        if (other.CompareTag("Perks"))
        {
            if (player)
            {
                other.GetComponent<IPerk>().ApplyPerkEffect(gameObject);
                playerShield = player.gameObject.transform.GetChild(1);
                playeShieldPerk = playerShield.GetComponent<ShieldPerkActive>();
            }
        }
        if (other.CompareTag("Enemy"))
        {
            if (isPlayer)
            {
                Destroy(gameObject);
            }
        }
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
        CurrentHealth -= damageValue;
        audioPlayer.PlayDamageSound();
        if (CurrentHealth <= 0)
        {
            audioPlayer.PlayExplosionSound();

            if (CompareTag("Enemy"))
            {
                ScoreKeeper.Instance.AddScore();
                Destroy(gameObject);
            }
            if (isPlayer)
            {
                print("Player died, time to load game over scene");
                levelManager.LoadGameOver();
                Destroy(gameObject);
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
