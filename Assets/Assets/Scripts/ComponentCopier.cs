using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCopier : MonoBehaviour
{
    public Component sourceComponent; // 选择你想要复制的组件

    void Start()
    {
        if (sourceComponent != null)
        {
            // 将组件添加到当前对象
            Component newComponent = gameObject.AddComponent(sourceComponent.GetType());

            // 复制源组件上的属性到新组件
            // 这可能涉及复制公共属性或字段，具体取决于你的组件的特性
            // 可以根据需要扩展此部分代码
        }
        else
        {
            Debug.LogWarning("Source component is not assigned.");
        }
    }
}