using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewGameScript : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject board;

    [SerializeField]
    private Vector3 cameraGoalPosition;
    [SerializeField]
    private Vector3 cameraGoalRotation;

    private void OnMouseDown()
    {
        if (SaveLoadManager.Instance.CheckSavedGame())
        {
            board.SetActive(true);
            camera.Move(cameraGoalPosition, cameraGoalRotation);
            StartCoroutine(HideCoroutine(buttons));
        }
        else
        {
            MainMenuManager.Instance.StartGame();
        }
    }

    private IEnumerator HideCoroutine(GameObject objectToHide)
    {
        yield return new WaitForSeconds(0.8f);
        objectToHide.SetActive(false);
    }
}
