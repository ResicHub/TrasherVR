using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
