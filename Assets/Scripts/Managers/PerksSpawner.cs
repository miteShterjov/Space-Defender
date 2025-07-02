using System;
using System.Collections;
using UnityEngine;

public class PerksSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gamePerks;
    [SerializeField] float minSpawnTime = 10f;
    [SerializeField] float maxSpawnTime = 30f;

    private float minXSpawnCord = -6f;
    private float maxXSpawnCord = 6f;


    private void Start()
    {
        StartCoroutine(SpawnGamePerk());
    }

    private IEnumerator SpawnGamePerk()
    {
        do
        {
            GetRandomSpawnTime(out float spawnTime);
            yield return new WaitForSeconds(spawnTime);
            GetRandomSpawnCord(out Vector3 spawnCord);
            GetRandomPerk(out GameObject perk);
            Instantiate(perk, spawnCord, Quaternion.identity);
            perk.tag = "Perks";
        }
        while (true);
    }

    private void GetRandomSpawnCord(out Vector3 spawnCord)
    {
        float x = UnityEngine.Random.Range(minXSpawnCord, maxXSpawnCord);
        spawnCord = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void GetRandomPerk(out GameObject perk)
    {
        int randomPerkIndex = UnityEngine.Random.Range(0, gamePerks.Length);
        perk = gamePerks[randomPerkIndex];
    }

    private void GetRandomSpawnTime(out float spawnTime)
    {
        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}