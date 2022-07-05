using System.Collections;
using UnityEngine;

public class ButtonBackScript : MonoBehaviour
{
    [SerializeField]
    private CameraController camera;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject board;

    private void OnMouseDown()
    {
        camera.MoveBack();
        buttons.SetActive(true);
        StartCoroutine(HideCoroutine(board));
    }

    private IEnumerator HideCoroutine(GameObject objectToHide)
    {
        yield return new WaitForSeconds(0.8f);
        objectToHide.SetActive(false);
    }
}
