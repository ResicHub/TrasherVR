using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    public int type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            //GameManager.Instance.IncreaseMissed();
            Destroy(gameObject);
        }
    }
}
