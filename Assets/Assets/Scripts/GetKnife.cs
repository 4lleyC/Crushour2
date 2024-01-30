using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKnife : MonoBehaviour
{
    void OnMouseDown()
    {
        // 当鼠标点击物体时销毁物体
        Destroy(gameObject);
    }
}
