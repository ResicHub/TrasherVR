using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    private float volume;
    private bool soundOn;

    [SerializeField]
    private VolumeBarScript volumeBar;
    [SerializeField]
    private SoundButtonScript soundButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SaveLoadManager.SettingsData data = SaveLoadManager.Instance.LoadSettings();
        volume = data.volume;
        soundOn = data.soundOn;
        if (volumeBar != null)
        {
            volumeBar.ResetVolumeButtons((int) volume);
        }
        if (soundButton != null)
        {
            soundButton.ResetText(soundOn);
        }
    }

    public void SetVolume(float value)
    {
        volume = value;
    }

    public void SetSound(bool isOn)
    {
        soundOn = isOn;
    }

    public void SaveSettings()
    {
        SaveLoadManager.SettingsData data = new SaveLoadManager.SettingsData()
        {
            soundOn = soundOn,
            volume = volume
        };
        SaveLoadManager.Instance.SaveSettings(data);
    }
}
