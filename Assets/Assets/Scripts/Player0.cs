using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player0 : MonoBehaviour
{
    private Vector3 _destination;
    public float moveSpeed = 2.0f; // 调整这个值以改变移动速度

    void Start()
    {
        _destination = transform.position;
    }

    public void SetTarget(Vector3 targetPos)
    {
        _destination = targetPos;
    }

    void Update()
    {
        Vector3 moveDirection = _destination - transform.position;

        if (moveDirection.magnitude > 0.5f)
        {
            moveDirection.Normalize();
            transform.rotation = Quaternion.LookRotation(moveDirection);
            GetComponent<Animator>().SetBool("IsWorking", true);

            // 使用移动速度乘以 Time.deltaTime 来调整移动速度
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsWorking", false);
        }
    }
}

