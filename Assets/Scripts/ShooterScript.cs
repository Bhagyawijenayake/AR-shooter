using UnityEngine;
using TMPro;

public class ShooterScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;
    public GameOverUI gameOverUI;

    private int spidersShot = 0;
    private float gameTimer = 0f;
    private bool gameIsOver = false;

    private void Update()
    {
        if (gameIsOver) return;

        gameTimer += Time.deltaTime;

        if (spidersShot >= 10)
        {
            gameIsOver = true;
            HandleGameOver();
        }
    }

    public void Shoot()
    {
        if (gameIsOver) return;

        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.name == "spider Variant(Clone)")
            {
                Destroy(hit.transform.gameObject);
                Instantiate(smoke, hit.point, Quaternion.identity);
                spidersShot++;
            }
        }
    }

    private void HandleGameOver()
    {
        // Stop any game-related actions
        gameIsOver = true;

        // Show the game over UI with the timer
        gameOverUI.ShowGameOver(gameTimer);

        //stop the timer in TimerUIUpdater
        FindObjectOfType<TimerUIUpdater>().StopTimer();

    }
}
