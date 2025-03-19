using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Scriptable Objects/Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [Header("Enemy Configuration")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [Header("Path Configuration")]
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 4.5f;
    [Header("Spawn Configuration")]
    [SerializeField] private float timeBetweenEnemySpawns = 1f;
    [SerializeField] private float spawnTimeVariance = 0f;
    [SerializeField] private float minSpawnTime = 0.3f;

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];
    public float GetMoveSpeed() => moveSpeed;
    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetRandomSpawnTime()
    {
        float variance = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(variance, minSpawnTime, float.MaxValue);
    }
}
