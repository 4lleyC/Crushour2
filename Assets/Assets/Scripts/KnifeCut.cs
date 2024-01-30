using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCut : MonoBehaviour
{
    public bool haveKnife = false;
    private bool isPressing = false;

    void Update()
    {
        // 检查场景中是否存在目标物体
        GameObject myKnife = GameObject.FindWithTag("Knife");

        // 如果找不到物体，将 haveKnife 设置为 true
        if (myKnife == null)
        {
            haveKnife = true;
        }

        // 检查是否正在长按
        if (isPressing && haveKnife)
        {
            // 长按时间超过3秒时，销毁物体
            if (Time.time - startTime >= 1.8f)
            {
                Destroy(gameObject);
            }
        }
    }

    private float startTime;

    private void OnMouseDown()
    {
        startTime = Time.time;
        isPressing = true;
    }

    private void OnMouseUp()
    {
        isPressing = false;
    }
}
