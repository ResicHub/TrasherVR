using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [SerializeField]
    private TextMeshPro prompt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        prompt.text = "Press 'A' to open settings\n(lover button of right controller)";
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            StartSettings();
        }
    }

    private void StartSettings()
    {
        prompt.text =
            "Use secondary tumb sticks to grab the bottle.\n" +
            "Use left stick to change distance to the table,\n" +
            "and right stick to change height.\n" +
            "Make sure you are comfortable grabbing the object on the table.";
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }
}
