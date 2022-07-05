using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonQuit : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.QuitGame();
    }
}
