using UnityEngine;

public class QuitBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            Application.Quit();
        }
    }
}
