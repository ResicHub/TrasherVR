using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundButtonScript : MonoBehaviour
{
    private bool soundOn = true;
    [SerializeField]
    private Material speaker;

    private void OnMouseDown()
    {
        if (soundOn)
        {
            speaker.color = Color.red;
        }
        else
        {
            speaker.color = Color.white;
        }
        soundOn = !soundOn;
        SettingsManager.Instance.SetSound(soundOn);
    }
    public void ResetText(bool sOn)
    {
        soundOn = sOn;
        if (!soundOn)
        {
            speaker.color = Color.red;
        }
        else
        {
            speaker.color = Color.white;
        }
    }
}
