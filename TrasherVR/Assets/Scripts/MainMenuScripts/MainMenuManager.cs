using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    [SerializeField]
    private BackGroundController bg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MenuOn();
    }

    public void MenuOn()
    {
        bg.SceneOn();
        StartCoroutine(SetBG(false));
    }
    public void MenuOff()
    {
        bg.gameObject.SetActive(true);
        bg.SceneOff();
    }

    public void StartGame()
    {
        SaveLoadManager.GameData data = new SaveLoadManager.GameData()
        {
            level = 1,
            caught = 0,
            missed = 0
        };
        SaveLoadManager.Instance.SaveGame(data);
        MenuOff();
        StartCoroutine(GameStartCoroutine());
    }

    public void ContinueGame()
    {
        MenuOff();
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator SetBG(bool value)
    {
        yield return new WaitForSeconds(1);
        bg.gameObject.SetActive(value);
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameScene");
    }
}
