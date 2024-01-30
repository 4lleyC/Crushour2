using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnockDown2 : MonoBehaviour
{
    private bool isKnockedDown = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float dragSpeed = 5.0f;
    public float disappearDelay = 0.5f;
    private Animator animator;
    private bool hasStartedDragging = false;
    private CapsuleCollider capsuleCollider;
    public float colliderMoveDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockedDown)
        {
    
            Debug.Log("KnockedDown 2!");
            // 在这里执行击倒后的逻辑
            // 例如：播放动画、修改材质、等等
        }

    }

    void OnMouseUp()
    {
        if (!isKnockedDown)
        {
            // 将物体击倒
            KnockDown();
        }
    }

    void OnMouseDrag()
    {
        if (isKnockedDown && !hasStartedDragging)
        {
            animator.SetBool("IsDragging", true);
            hasStartedDragging = true;
            Debug.Log("Drag 1!");

            // 在击倒状态下，拖拽物体
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragSpeed);

            StartCoroutine(DisappearAfterDelay());
        }
    }

    IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        yield return new WaitForSeconds(1.3f);

        // 在这里执行消失逻辑
        Disappear();
    }

    void KnockDown()
    {
        // 在这里执行击倒逻辑
        isKnockedDown = true;
    }

    void Disappear()
    {
        // 在这里执行消失逻辑
        Destroy(gameObject);
    }

}

