﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isGameScene = false;
    public bool IsCheckingAnyButton = false;

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

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            isGameScene = true;
        }
    }

    private void Update()
    {
        if (isGameScene && IsCheckingAnyButton)
        {
            CheckGameInput();
        }
        else
        {
            CheckMainMenuInput();
        }
    }

    private void CheckMainMenuInput()
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
                    floor.position += heightChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
                if (floor.position.z > heightBorders[0])
                {
                    floor.position -= heightChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
            // Distance to table
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                if (transform.position.z < positionBorders[1])
                {
                    transform.position += positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                if (transform.position.z > positionBorders[0])
                {
                    transform.position -= positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
        }
    }

    private void CheckGameInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameManager.Instance.GetButton(true);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            GameManager.Instance.GetButton(false);
        }
        IsCheckingAnyButton = false;
    }
}
