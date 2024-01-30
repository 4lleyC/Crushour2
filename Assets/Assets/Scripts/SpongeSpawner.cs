using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeSpawner : MonoBehaviour
{
    public GameObject Sponge;  // 物体的预制体

    void Start()
    {
        GenerateRandomObject();
    }

    void GenerateRandomObject()
    {
        // 定义三个可能的位置
        Vector3 position1 = new Vector3(21f, 10f, 415f);
        Vector3 position2 = new Vector3(21f, 10f, 74f);
        Vector3 position3 = new Vector3(21f, 10f, -117f);

        // 随机选择一个位置
        Vector3 randomPosition = GetRandomPosition(position1, position2, position3);

        // 在随机位置生成物体

        GameObject spawnedObject = Instantiate(Sponge, randomPosition, Quaternion.identity);

    }

    Vector3 GetRandomPosition(Vector3 pos1, Vector3 pos2, Vector3 pos3)
    {
        // 随机生成一个数字，用于选择位置
        int randomIndex = Random.Range(0, 3);

        // 根据随机数选择相应的位置
        switch (randomIndex)
        {
            case 0:
                return pos1;
            case 1:
                return pos2;
            case 2:
                return pos3;
            default:
                Debug.LogError("Invalid random index");
                return Vector3.zero;
        }
    }
}
