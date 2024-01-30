using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyCharacter : MonoBehaviour
{
    //保存角色当前目的地的变量
    Vector3 _Destination;
    private UnityEngine.AI.NavMeshPath _path;
    List<Vector3> _simplePath = new List<Vector3>();
    public CapsuleCollider _Collider;
    private float cooldownTime = 2.0f;
    private float lastExecutionTime = 0.0f;
    public float moveSpeed = 2.0f;
    public TMP_Text timePunishText;
    private float timePunishDuration = 1.0f;
    private float timePunishTimer;
    public static MyCharacter Instance;

    // Start is called before the first frame update
    void Start()
    {
        //当我们开始时，将目的地设置为当前位置
        _Destination = transform.position;
        _path = new UnityEngine.AI.NavMeshPath();
        Instance = this;
    }
    //设置角色目标目的地的函数
    public void SetTarget(Vector3 TargetPos)
    {
        _Destination = TargetPos;
        //找到从当前位置到目的地的路径
        bool foundPath = UnityEngine.AI.NavMesh.CalculatePath(transform.position, TargetPos, UnityEngine.AI.NavMesh.AllAreas, _path);
        _simplePath.Clear();
        for (int i = 0; i < _path.corners.Length; i++)
        {
            _simplePath.Add(_path.corners[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimePunishText();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 设置角色目标位置为鼠标右键点击的地面位置
                SetTarget(hit.point);
            }
        }
        //更新时，计算出我们需要移动的方向

        Vector3 MoveDirection = Vector3.zero;
        //删除我们真正接近的路径的任何部分
        while (_simplePath.Count > 0 && (transform.position - _simplePath[0]).magnitude < 0.5f)
        {
            _simplePath.RemoveAt(0);
        }
        //如果还有路径可走，则计算方向
        if (_simplePath.Count > 0)
        {
            MoveDirection = _simplePath[0] - transform.position;
        }

        //如果目的地距离合理，则更新角色旋转以指向该方向
        if (MoveDirection.magnitude > 0.5f)
        {
            MoveDirection.Normalize();
            //确保角色始终指向上方。
            //我们这样做是为了从移动方向移除任何向上的分量
            //这称为“投影”
            Vector3 ProjectedMoveDirection = MoveDirection - (Vector3.up * Vector3.Dot(Vector3.up, MoveDirection));
            transform.rotation = Quaternion.LookRotation(ProjectedMoveDirection, Vector3.up);

            //在动画控制器中设置一个变量
            GetComponent<Animator>().SetBool("IsWorking", true);
            transform.Translate(MoveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {

            //在动画控制器中设置一个变量
            GetComponent<Animator>().SetBool("IsWorking", false);

        }
        //将角色向下移动一点（有点简单的重力）
        transform.position = transform.position + new Vector3(0.0f, -0.01f, 0.0f);
        //角色控制器碰撞（不直接使用物理，仅进行碰撞测试）
        //我们这样做是因为我们只希望角色在与地板碰撞时垂直移动
        //这种类型的碰撞通常称为“角色控制器”
        //手动计算对象碰撞的好技巧！
        Collider[] coliders = Physics.OverlapBox(transform.position, _Collider.bounds.extents);
        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i] != _Collider)
            {
                if (coliders[i].gameObject.layer == 3)
                {
                    //hit completion zone
                    Debug.Log("Completion zone!");
                    GameManager._instance.CompleteLevel();
                }
                else
                {
                    Vector3 hitDirection;
                    float hitDistance;
                    if (Physics.ComputePenetration(coliders[i], coliders[i].transform.position, coliders[i].transform.rotation, _Collider,
                   _Collider.transform.position, _Collider.transform.rotation, out hitDirection, out hitDistance))
                    {
                        //使击打方向相对于角色
                        hitDirection *= -1.0f;
                        //如果是地板，我们只想在垂直方向上进行depenatrate
                        float MinFloorDot = 0.7f;
                        if (Vector3.Dot(hitDirection, Vector3.up) > MinFloorDot)
                        {
                            Vector3 depenatrationDir = Vector3.up;
                            //相应增加穿透深度
                            float denominator = Mathf.Abs(Vector3.Dot(depenatrationDir, hitDirection));
                            if (denominator > 0.0f)
                            {
                                transform.position += depenatrationDir * (hitDistance / denominator);
                            }
                        }
                        else
                        {
                            //它不是地板，沿自然方向退化
                            transform.position += hitDirection * hitDistance;
                        }
                    }
                }


                if (coliders[i].gameObject.layer == 6 && Time.time - lastExecutionTime >= cooldownTime)
                {
                    GameObject emptyObject = GameObject.Find("GameManag3r");
                    GameManager gameManager = emptyObject.GetComponent<GameManager>();

                    if (gameManager != null)
                    {
                        gameManager.TotalTime -= 5.0f;
                        lastExecutionTime = Time.time;
                        ShowTimePunishText();
                    }
                }
            }
        }
    }

    void ShowTimePunishText()
    {
        // 设置提示文本内容
        timePunishText.text = "-5!";

        // 激活Text元素
        timePunishText.gameObject.SetActive(true);

        // 启动计时器
        timePunishTimer = Time.time;
    }
    void UpdateTimePunishText()
    {
        if (Time.time - timePunishTimer >= timePunishDuration)
        {
            // 隐藏Text元素
            timePunishText.gameObject.SetActive(false);
        }
    }


}