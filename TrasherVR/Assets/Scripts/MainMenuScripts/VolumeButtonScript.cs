using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButtonScript : MonoBehaviour
{
    private VolumeBarScript volumeBar;

    [SerializeField]
    private int Value;

    private void Awake()
    {
        volumeBar = GetComponentInParent<VolumeBarScript>();
    }

    private void OnMouseDown()
    {
        volumeBar.ResetVolumeButtons(Value);
        SettingsManager.Instance.SetVolume(Value);
    }
}
