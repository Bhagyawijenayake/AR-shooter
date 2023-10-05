using UnityEngine;
using TMPro;

public class TimerUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private bool timerStarted = false;

    void Start()
    {
        timerText.text = "Time: 0";
    }

    void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F1"); // Display the timer with one decimal place.
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
    }
}
