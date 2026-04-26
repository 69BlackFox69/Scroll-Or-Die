using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    public float timeRemaining = 180f; // 3 минуты = 180 секунд
    public TextMeshProUGUI timerText;

    private bool timerIsRunning = true;

    public GameObject GameOverUI;
    public GameObject RadUI;
    public GameObject PlayerUI;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(timeRemaining);
                GameOverUI.SetActive(true);
                RadUI.SetActive(false);
                PlayerUI.SetActive(false);
                Time.timeScale = 0f;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
