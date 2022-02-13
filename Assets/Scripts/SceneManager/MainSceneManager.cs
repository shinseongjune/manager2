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
