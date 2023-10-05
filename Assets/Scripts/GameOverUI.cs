using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;

    public void ShowGameOver(float time)
    {
        timerText.text = "Time Played: " + FormatTime(time);
        gameOverText.gameObject.SetActive(true);
    }

    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int remainingSeconds = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }
}
