using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool isTrashOnTable;

    private void OnTriggerStay(Collider other)
    {
        isTrashOnTable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Trash")
        {
            if (!isTrashOnTable)
            {
                Instantiate(
                prefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
            }
        }
    }
}
