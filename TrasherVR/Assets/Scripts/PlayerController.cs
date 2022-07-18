using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isGameScene = false;
    public bool IsCheckingAnyButton = false;

    private float heightChangeSpeed = 0.5f;
    [SerializeField]
    private List<float> heightBorders;
    private float positionChangeSpeed = 0.5f;
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

    private void Start()
    {
        transform.position = SaveLoadManager.PlayerPosition;
    }

    private void Update()
    {
        if (IsCheckingAnyButton)
        {
            CheckGameInput();
        }
        else if (!isGameScene)
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
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                if (transform.position.y < heightBorders[1])
                {
                    transform.position += heightChangeSpeed * Time.fixedDeltaTime * Vector3.up;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                if (transform.position.y > heightBorders[0])
                {
                    transform.position -= heightChangeSpeed * Time.fixedDeltaTime * Vector3.up;
                }
            }
            // Distance to table
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            {
                if (transform.position.z < positionBorders[1])
                {
                    transform.position += positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
                if (transform.position.z > positionBorders[0])
                {
                    transform.position -= positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
        }
    }

    public void SavePlayerSettings()
    {
        SaveLoadManager.PlayerPosition = transform.position;
    }

    private void CheckGameInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameManager.Instance.GetButton(true);
            IsCheckingAnyButton = false;
        }
        else if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            GameManager.Instance.GetButton(false);
            IsCheckingAnyButton = false;
        }
    }
}
