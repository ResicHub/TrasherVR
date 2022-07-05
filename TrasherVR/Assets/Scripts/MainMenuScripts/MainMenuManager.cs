using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Test()
    {
        GameObject sphere = Instantiate(
            GameObject.CreatePrimitive(PrimitiveType.Sphere), 
            new Vector3(0, 1, 0), 
            Quaternion.identity, 
            gameObject.transform);
        sphere.AddComponent<Rigidbody>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
