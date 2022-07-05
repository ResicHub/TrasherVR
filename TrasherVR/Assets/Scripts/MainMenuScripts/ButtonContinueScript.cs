using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonContinueScript : MonoBehaviour
{
    [SerializeField]
    private GameObject message;

    private void OnMouseDown()
    {
        if (SaveLoadManager.Instance.CheckSavedGame())
        {
            MainMenuManager.Instance.ContinueGame();
        }
        else
        {
            StartCoroutine(ShowMessageCoroutine());
        }
    }

    private IEnumerator ShowMessageCoroutine()
    {
        message.SetActive(true);
        yield return new WaitForSeconds(3f);
        message.SetActive(false);
    }
}
