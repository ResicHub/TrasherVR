using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.Quit();
    }
}
