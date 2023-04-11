using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numberOfMedals;
    [SerializeField]
    private TextMeshProUGUI timeText;
    public TextMeshProUGUI NumberOfMedals => numberOfMedals;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private void Awake()
    {
        SetTimeRemain(120);
    }

    private void OnEnable()
    {
        SetTimeRemain(120);
        timerIsRunning = true;
        ItemCollector.collectMedalDelegate += OnPlayerCollect;
    }

    private void OnDisable()
    {
        ItemCollector.collectMedalDelegate -= OnPlayerCollect;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                if (UIManager.HasInstance && GameManager.HasInstance && AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_LOSE);
                    GameManager.Instance.PauseGame();
                    UIManager.Instance.ActiveLosePanel(true);
                }
            }
        }
    }

    private void OnPlayerCollect(int value)
    {
        numberOfMedals.SetText(value.ToString());
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimeRemain(float v)
    {
        timeRemaining = v;
    }
}
