using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraHeight = 5.0f;
    public float distanceFromPlayer = 10.0f;

    void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        // 计算相机位置
        Vector3 cameraPosition = playerTransform.position - Vector3.forward * distanceFromPlayer;
        cameraPosition.y = cameraHeight;

        // 设置相机位置
        transform.position = cameraPosition;
    }
}
