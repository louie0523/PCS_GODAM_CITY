using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    public void StartBtn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void TitleBtn()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
