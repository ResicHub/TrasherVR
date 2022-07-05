using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonPauseScript : MonoBehaviour
{
    private bool isPause;

    private void Start()
    {
        isPause = false;
    }

    private void OnMouseDown()
    {
        isPause = !isPause;
        GameManager.Instance.SetPause(isPause);
    }
}
