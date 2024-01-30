using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private RandomMovement randomMovement;

    void Start()
    {
        randomMovement = GetComponent<RandomMovement>();
        StartCoroutine(StartWanderTimer());
    }

    IEnumerator StartWanderTimer()
    {
        // 在每个 NPC 上启动协程以在一开始的时候设置初始 wanderTimer
        yield return new WaitForSeconds(Random.Range(0f, 5f));

        // 设置初始 wanderTimer
        randomMovement.SetNewWanderTimer();
    }
}

public class RandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    private float wanderTimer;

    // 允许的wanderTimer范围
    public float minWanderTimer = 3f;
    public float maxWanderTimer = 8f;

    private Vector3 target;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            SetNewRandomDestination();
            SetNewWanderTimer(); // 设置新的wanderTimer
        }
        bool isMoving = IsAgentMoving();
        animator.SetBool("IsMoving", isMoving);

        bool IsAgentMoving()
        {
            // 检查NavMeshAgent是否在移动
            return agent.velocity.magnitude > 0.01f && agent.remainingDistance > agent.radius;
        }
    }

    public void SetNewWanderTimer()
    {
        // 生成一个在minWanderTimer和maxWanderTimer之间的随机值
        wanderTimer = Random.Range(minWanderTimer, maxWanderTimer);
    }

    void SetNewRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);

        target = hit.position;
        agent.SetDestination(target);
    }

    public void StopRandomMovement()
    {
        // 实现停止随机移动的逻辑
        agent.Stop();
    }
}
