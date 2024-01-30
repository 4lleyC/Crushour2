using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerKnock : MonoBehaviour
{
    public int clickCountToDisappear = 3; // 设置点击次数阈值
    private int currentClickCount = 0;
    public bool haveHammer = false;

    void Update()
    {
        // 检查场景中是否存在目标物体
        GameObject myHammer = GameObject.FindWithTag("Hammer");

        // 如果找不到物体，将 isObjectMissing 设置为 true
        if (myHammer == null)
        {
            haveHammer = true;
        }
    }

    private void OnMouseDown()
    {
        currentClickCount++;

        if (currentClickCount >= clickCountToDisappear && haveHammer)
        {
            // 当达到点击次数阈值时，销毁物体
            Destroy(gameObject);
        }
    }
}
