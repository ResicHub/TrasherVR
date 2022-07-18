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
    private List<GameObject> containers;

    [SerializeField]
    private TextMeshPro levelText;
    [SerializeField]
    private TextMeshPro timerText;
    private string timerTextCopy;

    private float levelTimer = 30f;

    [SerializeField]
    private TextMeshPro statisticText;
    [SerializeField]
    private TextMeshPro gameResultText;
    
    private bool gameOn;
    private int level;

    private static int caughtCount;
    private static int missedCount;

    private int oldCaughtCount;
    private int oldMissedCount;

    private bool isLevelCompleete = true;

    public static GameManager Instance;

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
        levelText.text = $"Level: {level}";

        ContainerSetup();

        OVRScreenFade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(4);

        gameOn = true;
        spawner.isSpawning = true;
        belt.SetMovement(true);
    }

    private void ContainerSetup()
    {
        switch (level)
        {
            case 1:
                containers[2].SetActive(false);
                containers[3].SetActive(false);
                spawner.SetSpawnDifficulty(1);
                spawner.SetRespawn(2);
                belt.SetSpeed(1);
                break;
            case 2:
                containers[2].SetActive(false);
                containers[3].SetActive(false);
                spawner.SetSpawnDifficulty(1);
                spawner.SetRespawn(1.5f);
                belt.SetSpeed(1);
                break;
            case 3:
                containers[3].SetActive(false);
                spawner.SetSpawnDifficulty(2);
                spawner.SetRespawn(1.5f);
                belt.SetSpeed(1);
                break;
            case 4:
                containers[3].SetActive(false);
                spawner.SetSpawnDifficulty(2);
                spawner.SetRespawn(1.25f);
                belt.SetSpeed(1.25f);
                break;
            case 5:
                spawner.SetRespawn(1.25f);
                belt.SetSpeed(1.25f);
                break;
            default:
                break;
        }
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
        float robotCountAll = sessionCountAll + oldCaughtCount + oldMissedCount;
        float robotAccuracy = (caughtCount + oldCaughtCount) / robotCountAll * 100;
        string statisticString = $"Session accuracy: {System.Math.Round(sessionAccuracy, 2)}%{System.Environment.NewLine}" +
                                 $"Robot accuracy: {System.Math.Round(robotAccuracy, 2)}%";
        isLevelCompleete = sessionAccuracy >= 95f;
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
        yield return new WaitForSecondsRealtime(1);
        timerText.fontSize = 8;
        if (level == 1)
        {
            timerText.text = 
                "Press 'A' to start next level\n" +
                "Press 'X' to quit game";
        }
        PlayerController.Instance.IsCheckingAnyButton = true;
    }

    public void GetButton(bool goToNextLevel)
    {
        if (goToNextLevel)
        {
            StartCoroutine(GoToNextLevelCoroutine());
        }
        else
        {
            StartCoroutine(GoToMainMenuCoroutine());
        }
    }

    private IEnumerator GoToNextLevelCoroutine()
    {
        OVRScreenFade.instance.FadeOut();
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Game");
    }

    private IEnumerator GoToMainMenuCoroutine()
    {
        OVRScreenFade.instance.FadeOut();
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("MainMenu");
    }
}
