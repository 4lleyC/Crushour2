using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs; // 指定你的10个不同预制体
    public int numberOfInstancesPerCharacter = 14; // 每个预制体生成的实例数量
    public float spawnHeight = 6.6f; // 生成的高度
    public Vector2 spawnRangeX = new Vector2(0f, 23f); // 生成的X范围
    public Vector2 spawnRangeZ = new Vector2(-195f, 440f); // 生成的Z范围

    void Start()
    {
        // 循环生成每个预制体的实例
        foreach (GameObject characterPrefab in characterPrefabs)
        {
            for (int i = 0; i < numberOfInstancesPerCharacter; i++)
            {
                // 随机生成实例的位置
                float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
                float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
                Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);

                // 使用Instantiate创建实例，并指定位置和旋转信息
                GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);

                // 如果你希望对生成的实例进行一些设置，可以在这里添加代码
                // 例如，设置不同外观、属性等等
            }
        }
    }
}