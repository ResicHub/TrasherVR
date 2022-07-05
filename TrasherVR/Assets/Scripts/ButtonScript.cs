using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour
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
        board.SetActive(true);
        camera.Move(cameraGoalPosition, cameraGoalRotation);
        StartCoroutine(HideCoroutine(buttons));
    }

    private IEnumerator HideCoroutine(GameObject objectToHide)
    {
        yield return new WaitForSeconds(0.5f);
        objectToHide.SetActive(false);
    }
}
