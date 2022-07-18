using UnityEngine;

public class NewGameBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            SaveLoadManager.Level = 1;
            SaveLoadManager.CaughtCount = 0;
            SaveLoadManager.MissedCount = 0;
            MainMenuManager.instance.StartGame();
        }
    }
}
