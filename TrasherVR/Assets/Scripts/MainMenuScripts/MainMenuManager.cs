using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private IEnumerator SetBG(bool value)
    {
        yield return new WaitForSeconds(1);
        bg.gameObject.SetActive(value);
    }

    public void StartGame()
    {
        MenuOff();
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }
}
