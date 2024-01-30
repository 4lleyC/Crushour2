using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseButtonLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TryAgainButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    public void QuitPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

