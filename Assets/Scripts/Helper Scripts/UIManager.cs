using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI P1Score, P2Score, timerText, winnerText, highScoreText;
    public GameObject gameOverCanvas, player1, player2;
    public float timer = 180.0f;

    private void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().gameObject;
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().gameObject;
        highScoreText.text = PlayerPrefs.GetInt("HIGH SCORE: ", 0).ToString();
    }

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        timer -= Time.deltaTime;
        timerText.text = "TIME: " + timer.ToString("F0");

        if(timer < 0)
        {
            timer = 0;
        }

        if(timer == 0)
        {
            if(player1.activeInHierarchy == true && player2.activeInHierarchy == true)
            {
                winnerText.text = "DRAW";
            }

            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }
    }

    public void PauseTimer()
    {
        Time.timeScale = 0;
    }

    public void ResumeTimer()
    {
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        highScoreText.text = "HIGH SCORE: ";
    }
}
