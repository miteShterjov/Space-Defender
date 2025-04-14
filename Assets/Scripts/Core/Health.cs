using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private ParticleSystem boomVFX;
    [SerializeField] private bool applyCameraShake;
    [SerializeField] private bool isPlayer;
    [SerializeField] private float powerPerkCooldown = 10f; // Duration of the PowerPerk effect

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
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return;
        }
        CurrentHealth += healthValue;
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
            string perkName = other.gameObject.GetComponent<IPerk>().PerkName;

            if (player.GetComponent<Shooter>().HasPowerPerk && perkName == "PowerPerk") return;
            if (player && playerShield != null && perkName == "ShieldPerk") return;
            if (player && currentHealth >= maxHealth && perkName == "HealthPerk") return;

            if (player)
            {
                other.GetComponent<IPerk>().ApplyPerkEffect(gameObject);

                if (perkName == "ShieldPerk")
                {
                    playerShield = player.gameObject.transform.GetChild(1);
                    playeShieldPerk = playerShield.GetComponent<ShieldPerkActive>();
                }
                if (perkName == "PowerPerk")
                {
                    StartCoroutine(StartPowerPerkCooldownRoutine(player));
                }
            }
        }
        if (other.CompareTag("Enemy"))
        {
            if (isPlayer)
            {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Obstacle"))
        {
            if (isPlayer)
            {
                TakeDamage(maxHealth);
                PlayHitEffect();
                ShakeCamera();
            }
        }
    }

    
    private IEnumerator StartPowerPerkCooldownRoutine(Player player)
    {
        // Start the cooldown for the PowerPerk effect
        yield return new WaitForSeconds(powerPerkCooldown);
        // After the value of the var powerPerkCooldown, the player will stop shooting 3 projectiles at once
        if (player != null)
        {
            player.GetComponent<Shooter>().HasPowerPerk = false;
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
                ScoreKeeper.Instance.AddScore();
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
