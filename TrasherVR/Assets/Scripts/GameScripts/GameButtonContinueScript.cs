using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonContinueScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.GameOver();
    }
}
