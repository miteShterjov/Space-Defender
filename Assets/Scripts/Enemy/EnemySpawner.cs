using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0.5f;
    [SerializeField] private bool isLooping = false;
    private WaveConfigSO currentWave;
    private int bossSpawnDelay = 1;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() => currentWave;

    private IEnumerator SpawnEnemyWaves()
    {
        do
        {
            // Uncomment this block to spawn waves in random order
            for (int i = 0; i < waveConfigs.Count; i++)
            {
                int index = UnityEngine.Random.Range(0, waveConfigs.Count);
                currentWave = waveConfigs[index];
                yield return StartCoroutine(SpawnAllEnemiesInWave());
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            /* Uncomment this block to spawn waves in order
            foreach (WaveConfigSO waveConfig in waveConfigs)
            {
                currentWave = waveConfig;
                yield return StartCoroutine(SpawnAllEnemiesInWave());
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            */
        } while (isLooping);

        Invoke("SpawnBoss", bossSpawnDelay);
        yield return new WaitForSeconds(timeBetweenWaves);
    }

    private void SpawnBoss()
    {
        Instantiate(
            bossPrefab,
            new Vector3(0,5,0),
            Quaternion.identity,
            null
            );
        Debug.Log("Boss Spawned!");
    }


    private IEnumerator SpawnAllEnemiesInWave()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(
                currentWave.GetEnemyPrefab(i),
                currentWave.GetStartingWaypoint().position,
                Quaternion.Euler(0, 0, 180),
                transform
                );
            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }
}
