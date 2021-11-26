using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button ButtonStart;

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ButtonStart = root.Q<Button>("ButtonStart");

        ButtonStart.clicked += StartGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SceneGame");
    }
}
