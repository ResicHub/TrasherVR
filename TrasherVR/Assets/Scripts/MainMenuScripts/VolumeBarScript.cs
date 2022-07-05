using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeBarScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> volumeButtons;

    [SerializeField]
    private Material activeMaterial;
    [SerializeField]
    private Material deactiveMaterial;

    public void ResetVolumeButtons(int number)
    {
        for (int i = 0; i < number; i++)
        {
            volumeButtons[i].GetComponent<MeshRenderer>().material = activeMaterial;
        }
        for (int i = number; i < volumeButtons.Count; i++)
        {
            volumeButtons[i].GetComponent<MeshRenderer>().material = deactiveMaterial;
        }
    }
}
