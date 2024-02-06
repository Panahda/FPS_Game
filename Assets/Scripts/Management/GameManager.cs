using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign in the inspector
    public GameObject timerPanel; // Assign the panel containing the timer Text in the inspector
    public GameObject winUI; // Assign your Win UI panel in the inspector

    private bool timerStarted = false;
    private float startTime;

    void Start()
    {
        timerPanel.SetActive(false); // Ensure the timer panel is not visible at the start
        winUI.SetActive(false); // Ensure the win UI is not visible at the start
    }

    void Update()
    {
        if (timerStarted)
        {
            if (!IsAnyEnemyAlive())
            {
                StopTimer();
                ShowWinUI();
            }
            else
            {
                UpdateTimer();
            }
        }
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        startTime = Time.time;
        timerStarted = true;
        timerPanel.SetActive(true); // Show the timer panel
    }

    bool IsAnyEnemyAlive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    void StopTimer()
    {
        timerStarted = false; // Stops the timer
        timerPanel.SetActive(false); // Optionally hide the timer panel
    }

    void ShowWinUI()
    {
        winUI.SetActive(true);
    }

    void UpdateTimer()
    {
        float timeSinceStarted = Time.time - startTime;
        string minutes = ((int)timeSinceStarted / 60).ToString();
        string seconds = (timeSinceStarted % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }
}
