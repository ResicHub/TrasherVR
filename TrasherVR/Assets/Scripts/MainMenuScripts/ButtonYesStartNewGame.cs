using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonYesStartNewGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        MainMenuManager.Instance.StartGame();
    }
}
