using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonLogic : MonoBehaviour
{
    [SerializeField] private Button Lv1Button;

    private void Awake()
    {
        Lv1Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        }

        );
    }
}
