using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] obstacleSprites;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] float minSpawnTime = 10f;
    [SerializeField] float maxSpawnTime = 20f;
    [SerializeField] float spawnerStartTime = 25f;

    private float minXSpawnCord = -6f;
    private float maxXSpawnCord = 6f;

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
            Instantiate(obstaclePrefab, spawnCord, Quaternion.identity);
            SetRandomSprite(obstaclePrefab);
            obstaclePrefab.tag = "Obstacle";
        }
        while (true);
    }

    private void GetRandomSpawnCord(out Vector3 spawnCord)
    {
        float x = UnityEngine.Random.Range(minXSpawnCord, maxXSpawnCord);
        spawnCord = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void SetRandomSprite(GameObject obstacle)
    {
        //obstacle = Instantiate(obstaclePrefab); // Instantiate the obstacle
        int randomSpriteIndex = UnityEngine.Random.Range(0, obstacleSprites.Length);
        obstacle.GetComponent<SpriteRenderer>().sprite = obstacleSprites[randomSpriteIndex];
    }

    private void GetRandomSpawnTime(out float spawnTime)
    {
        spawnTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
    }
}
