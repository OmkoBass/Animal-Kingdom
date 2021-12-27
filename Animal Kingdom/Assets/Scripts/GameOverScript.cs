using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;

    void LateUpdate()
    {
        if (GameManager.GameOver)
        {
            GenerateAnimalName.Clear();
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
