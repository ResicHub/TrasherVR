using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBeltMovung : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private float speed = 1f;
    private Vector3 currentPosition;
    private bool isMoving = true;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentPosition = myRigidbody.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            myRigidbody.position += Vector3.forward * speed * Time.fixedDeltaTime;
            myRigidbody.MovePosition(currentPosition);
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void SetMovement(bool value)
    {
        isMoving = value;
    }
}
