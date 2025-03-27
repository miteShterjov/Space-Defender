using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingSound;
    [SerializeField][Range(0f, 1f)] private float shootingVolume = 0.5f;

    [Header("Take Damage")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField][Range(0f, 1f)] private float damageVolume = 0.5f;
    
    [Header("Explosion")]
    [SerializeField] private AudioClip explosionSound;
    [SerializeField][Range(0f, 1f)] private float explosionVolume = 0.4f;

    void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        int instanceCount = FindObjectsByType<AudioPlayer>(FindObjectsSortMode.None).Length;
        if (instanceCount > 1) 
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
    }

    public void PlayShootingSound()
    {
        if (shootingSound == null) 
        {
            Debug.LogError("Shooting sound is not set in the AudioPlayer component.");
            return;
        }
        AudioSource.PlayClipAtPoint(shootingSound, Camera.main.transform.position, shootingVolume);
    }

    public void PlayDamageSound()
    {
        if (damageSound == null) 
        {
            Debug.LogError("Damage sound is not set in the AudioPlayer component.");
            return;
        }
        AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position, damageVolume);
    }

    public void PlayExplosionSound()
    {
        if (explosionSound == null) 
        {
            Debug.LogError("Explosion sound is not set in the AudioPlayer component.");
            return;
        }
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionVolume);
    }


}
