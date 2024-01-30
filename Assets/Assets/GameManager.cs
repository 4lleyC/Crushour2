using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public TMPro.TMP_Text text;
    public GameObject train; // 引用你的 "Train" GameObject
    public float TotalTime = 30.0f;
    private bool trainStarted = false;
    public float trainSpeed = 5.0f; // 调整移动速度

    void OnEnable()
    {
        _instance = this;
    }

    void Start()
    {
        // 在这里可以添加其他初始化逻辑
    }

    void Update()
    {
        TotalTime -= Time.deltaTime;

        // 判断是否需要开始移动 "Train"
        if (TotalTime <= 40.0f && !trainStarted)
        {
            StartTrainMovement();
        }

        if (TotalTime < 0.0f)
        {
            text.text = "0 seconds before train leaves";
            FailLevel();
        }
        else
        {
            text.text = "<color=yellow>" + ((int)TotalTime) + "</color> <color=white>  seconds before train leaves";
        }
    }

    void StartTrainMovement()
    {
        trainStarted = true;
        StartCoroutine(MoveTrain());
    }

    IEnumerator MoveTrain()
    {
        while (train.transform.position.z > -258f)
        {
            // 计算移动距离
            float moveDistance = trainSpeed * Time.deltaTime;

            // 计算新的位置
            Vector3 newPosition = train.transform.position - new Vector3(0f, 0f, moveDistance);

            // 移动 "Train"
            train.transform.position = newPosition;

            yield return null;
        }

    }

    public void FailLevel()
    {
        SceneManager.LoadScene("LoseScene");
        Debug.Log("fail");
    }

    public void CompleteLevel()
    {
        Debug.Log("win");
        SceneManager.LoadScene("WinScene");
    }
}
