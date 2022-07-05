using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;
    [SerializeField]
    private BackGroundController bg;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject StatisticBoard;
    [SerializeField]
    private Vector3 statisticCameraPosition;
    [SerializeField]
    private Vector3 statisticCameraRotation;
    [SerializeField]
    private TextMeshPro statisticText;
    [SerializeField]
    private TextMeshPro gameResultText;

    [SerializeField]
    private TransportBeltMovung belt;

    [SerializeField]
    private Spawner spawner;

    public static GameManager Instance;

    private float levelTimer;
    private bool gameOn;
    private int level;

    private int caughtCount;
    private int missedCount;

    private int oldCaughtCount;
    private int oldMissedCount;

    [SerializeField]
    private TextMeshPro levelText;
    [SerializeField]
    private TextMeshPro timerText;
    private string timerTextCopy;

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
        levelTimer = 15;
        level = -1;
        caughtCount = 0;
        missedCount = 0;
        oldCaughtCount = 0;
        oldMissedCount = 0;

        StartCoroutine(LoadGameCoroutine());
        GameOn();
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        SaveLoadManager.GameData data = SaveLoadManager.Instance.LoadGame();
        level = data.level;
        levelText.text = $"Level {level}";
        oldCaughtCount = data.caught;
        oldMissedCount = data.missed;
        yield return null;
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(2);
        Debug.Log($"level {level}");

        spawner.SetRespawn(1 - (level - 1) * 0.1f);
        belt.SetSpeed(2 + level);

        gameOn = true;
        spawner.isSpawning = true;
        belt.SetMovement(true);
        StartCoroutine(ShowPauseButtonCoroutine());
    }

    private IEnumerator ShowPauseButtonCoroutine()
    {
        Vector3 start = pauseButton.transform.position;
        Vector3 goal;
        goal = new Vector3(start.x, start.y + 1f, start.z);
        float t = 0f;
        while (pauseButton.transform.position != goal)
        {
            yield return pauseButton.transform.position = Vector3.Lerp(start, goal, t);
            t += Time.deltaTime * 2;
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
                DeactivateTrash();
                belt.SetMovement(false);
                Debug.Log("Time!");
                timerText.text = "Time!";
                StartCoroutine(EndGameCoroutine());
            }
        }
    }

    public void GameOn()
    {
        bg.SceneOn();
        StartCoroutine(SetBG(false));
    }
    public void GameOff()
    {
        bg.gameObject.SetActive(true);
        bg.SceneOff();
    }

    private IEnumerator SetBG(bool value)
    {
        yield return new WaitForSeconds(1);
        bg.gameObject.SetActive(value);
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

    public void IncreaseCaught()
    {
        caughtCount++;
    }

    public void IncreaseMissed()
    {
        missedCount++;
    }

    private void ActivateTrash()
    {
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            GameObject child = spawner.transform.GetChild(i).gameObject;
            child.GetComponent<TrashObject>().canCatch = true;
        }
    }

    private void DeactivateTrash()
    {
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            GameObject child = spawner.transform.GetChild(i).gameObject;
            TrashObject childTO = child.GetComponent<TrashObject>();
            childTO.canCatch = false;
            childTO.MouseUp();
        }
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
        yield return new WaitForSeconds(3);
        StatisticBoard.SetActive(true);
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
        camera.Move(statisticCameraPosition, statisticCameraRotation);
    }

    public void GameOver()
    {
        if (isLevelCompleete)
        {
            if (level < 5)
            {
                StartCoroutine(NextLevelCoroutine());
                GameOff();
                StartCoroutine(GoToNextLevel());
            }
            
            else
            {
                GameOff();
                SaveLoadManager.Instance.RemoveGame();
                StartCoroutine(GameOverTextCoroutine("You Win!"));
            }
        }
        else
        {
            GameOff();
            SaveLoadManager.Instance.RemoveGame();
            StartCoroutine(GameOverTextCoroutine("Game Over"));
        }
    }

    public IEnumerator NextLevelCoroutine()
    {
        SaveLoadManager.GameData data = new SaveLoadManager.GameData()
        {
            level = level + 1,
            caught = oldCaughtCount + caughtCount,
            missed = oldMissedCount + missedCount
        };
        SaveLoadManager.Instance.SaveGame(data);
        yield return null;
    }

    public IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene");
    }

    public IEnumerator GameOverTextCoroutine(string text)
    {
        gameOverText.text = text;
        yield return new WaitForSeconds(2);
        float t = 0;
        while (t <= 1)
        {
            Color color = gameOverText.color;
            color.a = Mathf.Lerp(0f, 1f, t);
            yield return gameOverText.color = color;
            t += Time.deltaTime * 2;
        }
        yield return new WaitForSeconds(2);
        t = 0;
        while (t <= 1)
        {
            Color color = gameOverText.color;
            color.a = Mathf.Lerp(1f, 0f, t);
            yield return gameOverText.color = color;
            t += Time.deltaTime * 2;
        }
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetPause(bool mode)
    {
        gameOn = !mode;
        spawner.isSpawning = !mode;
        belt.SetMovement(!mode);
        StartCoroutine(ShowMenuButtonCoroutine(mode));
        if (mode)
        {
            DeactivateTrash();
        }
        else
        {
            ActivateTrash();
        }
    }

    private IEnumerator ShowMenuButtonCoroutine(bool mode)
    {
        Vector3 start = menuButton.transform.position;
        Vector3 goal;
        if (mode)
        {
            goal = new Vector3(start.x, start.y + 1f, start.z);
        }
        else
        {
            goal = new Vector3(start.x, start.y - 1f, start.z);
        }
        
        float t = 0f;
        while (menuButton.transform.position != goal)
        {
            yield return menuButton.transform.position = Vector3.Lerp(start, goal, t);
            t += Time.deltaTime * 2;
        }
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    private IEnumerator QuitGameCoroutine()
    {
        GameOff();
        yield return new WaitForSeconds(2);
        GoToMainMenu();
    }
}
