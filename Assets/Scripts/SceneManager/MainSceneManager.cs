using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    void GameStart()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }

    void GameExit()
    {
        Application.Quit();
    }
}
