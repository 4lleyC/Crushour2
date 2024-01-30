using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Erase : MonoBehaviour
{
    private bool isErasing = false;
    private float dragStartTime;
    public float dragDelay = 1.0f;
    public bool haveSponge = false;

    // Start is called before the first frame update
    void Update()
    {
        // 检查场景中是否存在目标物体
        GameObject mySponge = GameObject.FindWithTag("Sponge");

        // 如果找不到物体，将 isObjectMissing 设置为 true
        if (mySponge == null)
        {
            haveSponge = true;
        }
    }

        void OnMouseDown()
    {

            // 记录拖拽开始的时间
            dragStartTime = Time.time;

            // 当鼠标按下时开始擦除
            isErasing = true;

    }

    void OnMouseUp()
    {
        // 当鼠标松开时停止擦除
        isErasing = false;
    }

    void OnMouseDrag()
    {
        // 在拖拽状态下，执行擦除逻辑
        if ((isErasing && haveSponge) && Time.time - dragStartTime > dragDelay)
        {
            EraseLogic();
        }
    }

    void EraseLogic()
    {
        Destroy(gameObject);
        Debug.Log("eraseSuccess!");
    }
}
