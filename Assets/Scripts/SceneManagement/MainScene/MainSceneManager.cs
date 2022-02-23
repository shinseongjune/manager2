using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    void GameStart()
    {
        SceneManager.LoadScene("NewGameScene");
    }

    void GameExit()
    {
        Application.Quit();
    }
}
