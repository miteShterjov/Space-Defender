using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] obstacleSprites;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] float minSpawnTime = 10f;
    [SerializeField] float maxSpawnTime = 20f;
    [SerializeField] float spawnerStartTime = 25f;

    private float minXSpawnCord = -2f;
    private float maxXSpawnCord = 2f;

    private void Start()
    {
        Invoke("StartSpawnCourutine", spawnerStartTime);
    }

    private void StartSpawnCourutine()
    {
        StartCoroutine(SpawnGameObstacles());
    }

    private IEnumerator SpawnGameObstacles()
    {
        do
        {
            GetRandomSpawnTime(out float spawnTime);
            yield return new WaitForSeconds(spawnTime);
            GetRandomSpawnCord(out Vector3 spawnCord);
            GetRandomSprite(out GameObject obstacle);
            Instantiate(obstacle, spawnCord, Quaternion.identity);
            obstacle.tag = "Obstacle";
        }
        while (true);
    }

    private void GetRandomSpawnCord(out Vector3 spawnCord)
    {
        float x = UnityEngine.Random.Range(minXSpawnCord, maxXSpawnCord);
        spawnCord = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void GetRandomSprite(out GameObject obstacle)
    {
        obstacle = Instantiate(obstaclePrefab); // Instantiate the obstacle
        int randomSpriteIndex = UnityEngine.Random.Range(0, obstacleSprites.Length);
        obstacle.GetComponent<SpriteRenderer>().sprite = obstacleSprites[randomSpriteIndex];
    }

    private void GetRandomSpawnTime(out float spawnTime)
    {
        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}
