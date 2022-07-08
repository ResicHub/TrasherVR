using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTrigger : MonoBehaviour
{
    [SerializeField]
    private int type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trash")
        {
            if (type == other.gameObject.GetComponent<TrashObject>().Type)
            {
                GameManager.IncreaseCaught();
            }
            else
            {
                GameManager.IncreaseMissed();
            }
            Destroy(other.gameObject);
        }
    }
}
