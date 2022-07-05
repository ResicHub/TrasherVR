using UnityEngine;

public class ButtonSaveScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        SettingsManager.Instance.SaveSettings();
    }
}
