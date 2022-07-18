using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBucket : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsBoard;
    private bool isActive = false;
    private float showingTime = 2f;
    private Vector3 finalPosition = new Vector3(-2.9f, 1.5f, -1.3f);

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && other.tag == "Trash" )
        {
            isActive = true;
            StartCoroutine(ShowBoardCoroutine());
        }
    }

    private IEnumerator ShowBoardCoroutine()
    {
        creditsBoard.SetActive(true);
        Vector3 startPosition = creditsBoard.transform.position;
        float showTimer = 0f;
        while (showTimer < showingTime || creditsBoard.transform.position != finalPosition)
        {
            yield return creditsBoard.transform.position = Vector3.Lerp(startPosition, finalPosition, showTimer / showingTime);
            showTimer += Time.deltaTime;
        }
        yield return creditsBoard.transform.position = finalPosition;
    }
}
