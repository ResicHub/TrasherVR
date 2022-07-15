using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform floor;
    [SerializeField]
    private float heightChangeSpeed = 1.0f;
    [SerializeField]
    private List<float> heightBorders;
    [SerializeField]
    private float positionChangeSpeed = 1.0f;
    [SerializeField]
    private List<float> positionBorders;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            MainMenuManager.instance.ResetSettingsActive();
        }
        if (MainMenuManager.instance.isSettingsActive)
        {
            // Height
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            {
                if (floor.position.z < heightBorders[1])
                {
                    floor.position = floor.position + Vector3.forward * heightChangeSpeed * Time.fixedDeltaTime;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
                if (floor.position.z > heightBorders[0])
                {
                    floor.position = floor.position - Vector3.forward * heightChangeSpeed * Time.fixedDeltaTime;
                }
            }
            // Distance to table
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                // Position++
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                // Position--
            }
        }
    }
}
