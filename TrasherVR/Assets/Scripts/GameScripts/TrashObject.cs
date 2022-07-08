using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    public int Type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            GameManager.IncreaseMissed();
            Destroy(gameObject);
        }
    }
}
