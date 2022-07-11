using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private TransportBeltMovung belt;

    [SerializeField]
    private TextMeshPro levelText;
    [SerializeField]
    private TextMeshPro timerText;
    private string timerTextCopy;

    [SerializeField]
    private float beltSpeed;
    [SerializeField]
    private float spawnerSpeed;
    [SerializeField]
    private float levelTimer;

    [SerializeField]
    private TextMeshPro statisticText;
    [SerializeField]
    private TextMeshPro gameResultText;
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    public static GameManager Instance;
    
    private bool gameOn;
    private int level;

    private static int caughtCount;
    private static int missedCount;

    private int oldCaughtCount;
    private int oldMissedCount;

    private bool isLevelCompleete = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameOn = false;
        spawner.isSpawning = false;
        level = 1;
        caughtCount = 0;
        missedCount = 0;
        oldCaughtCount = 0;
        oldMissedCount = 0;

        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        levelText.text = $"level {level}";
        yield return new WaitForSecondsRealtime(2);
        Debug.Log($"level {level}");

        spawner.SetRespawn(spawnerSpeed);
        belt.SetSpeed(beltSpeed);

        gameOn = true;
        spawner.isSpawning = true;
        belt.SetMovement(true);
    }

    private void FixedUpdate()
    {
        if (gameOn)
        {
            levelTimer -= Time.deltaTime;
            ResetTimerText();
            if (levelTimer <= 0)
            {
                gameOn = false;
                spawner.isSpawning = false;
                belt.SetMovement(false);
                Debug.Log("Time!");
                timerText.text = "Time!";
                StartCoroutine(EndGameCoroutine());
            }
        }
    }

    private void ResetTimerText()
    {
        string newTime = $"0{(int)levelTimer / 60}:{TimePart((int)levelTimer % 60)}";
        if (newTime != timerTextCopy)
        {
            timerTextCopy = newTime;
            timerText.text = newTime;
        }
    }

    private string TimePart(int sec)
    {
        if (sec <= 9)
        {
            return $"0{sec}";
        }
        return sec.ToString();
    }

    public static void IncreaseCaught()
    {
        caughtCount++;
    }

    public static void IncreaseMissed()
    {
        missedCount++;
    }

    private IEnumerator EndGameCoroutine()
    {
        float sessionCountAll = caughtCount + missedCount;
        float sessionAccuracy = caughtCount / sessionCountAll * 100;
        Debug.Log($"{caughtCount} ###############################################");
        Debug.Log($"{sessionCountAll} ###############################################");
        Debug.Log($"{sessionAccuracy} ###############################################");
        float robotCountAll = sessionCountAll + oldCaughtCount + oldMissedCount;
        float robotAccuracy = (caughtCount + oldCaughtCount) / robotCountAll * 100;
        Debug.Log($"{robotAccuracy} ###############################################");
        string statisticString = $"Session accuracy: {System.Math.Round(sessionAccuracy, 2)}%{System.Environment.NewLine}" +
                                 $"Robot accuracy: {System.Math.Round(robotAccuracy, 2)}%";
        isLevelCompleete = sessionAccuracy >= 95f;
        yield return new WaitForSecondsRealtime(1);
        statisticText.text = statisticString;
        if (isLevelCompleete)
        {
            gameResultText.text = "Level completed!";
            gameResultText.color = Color.green;
        }
        else
        {
            gameResultText.text = "Level failed!";
            gameResultText.color = Color.red;
        }
        GameOver();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(5);
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetPause(bool mode)
    {
        gameOn = !mode;
        spawner.isSpawning = !mode;
        belt.SetMovement(!mode);
    }
}
