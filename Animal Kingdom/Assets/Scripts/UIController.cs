using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void StartGameScene()
    {
        SceneManager.LoadScene("SceneGame");
    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene("SceneGame");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }
}
