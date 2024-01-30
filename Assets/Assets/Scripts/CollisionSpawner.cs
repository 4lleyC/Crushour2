using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSpawner : MonoBehaviour
{
    public GameObject spikePrefab;  // 障碍物钉子预制体
    public GameObject leafPrefab;   // 障碍物叶子预制体
    public int numberOfObstacles = 3;  // 每种障碍物生成数量

    void Start()
    {
        GenerateObstacles(spikePrefab, -2f, 19f, 6.6f, -200f, 400f);  // 生成障碍物钉子
        GenerateObstacles(leafPrefab, 6f, 18f, 6.95f, -200f, 400f);    // 生成障碍物叶子
    }

    void GenerateObstacles(GameObject prefab, float minX, float maxX, float y, float minZ, float maxZ)
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            Vector3 spawnPosition = new Vector3(randomX, y, randomZ);

            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}