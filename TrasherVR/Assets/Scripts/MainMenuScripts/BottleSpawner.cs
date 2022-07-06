using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool isTrashOnTable = true;
    private float timer = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (!isTrashOnTable && other.tag == "Trash")
        {
            isTrashOnTable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Trash")
        {
            isTrashOnTable = false;
        }
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (!isTrashOnTable)
        {
            Instantiate(
                prefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
        }
        if (timer <= 0)
        {
            timer = 1f;
        }
    }
}
