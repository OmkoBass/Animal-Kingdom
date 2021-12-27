using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreText;
    private void LateUpdate()
    {
        ScoreText.text = $"Score: {GameManager.Score.ToString()}";
    }
}
