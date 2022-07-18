using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField]
    private TextMeshPro prompt;

    public bool isSettingsActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetSettings();
        OVRScreenFade.instance.FadeIn();
    }

    public void ResetSettingsActive()
    {
        isSettingsActive = !isSettingsActive;
        SetSettings();
    }

    public void SetSettings()
    {
        if (isSettingsActive)
        {
            prompt.text =
                "Use hand triggers (middle fingers) to grab the bottle.\n" +
                "Use right stick to change distance to the table,\n" +
                "and left stick to change height.\n" +
                "Make sure you are comfortable grabbing the object on the table.";
        }
        else
        {
            prompt.text = 
                "Press 'A' to open settings\n" +
                "(lover button of right controller)";
        }
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        OVRScreenFade.instance.FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }
}
