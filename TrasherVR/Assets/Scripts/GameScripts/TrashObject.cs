using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    [SerializeField]
    private int type;

    private Vector3 startPoint;

    private float goalZCoord = 4.5f;
    private float approachSpeed = 100f;
    private bool goToGoal = false;

    private Rigidbody rigidbody;
    private bool isDropped = false;
    public bool canCatch = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (canCatch)
        {
            rigidbody.useGravity = false;
            startPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            goToGoal = true;
            isDropped = false;
        }
    }

    private void OnMouseDrag()
    {
        if (canCatch)
        {
            if (!goToGoal)
            {
                Vector3 mousePoint = Input.mousePosition;
                mousePoint.z = goalZCoord;
                gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePoint);
            }
        }
    }

    private void OnMouseUp()
    {
        if (canCatch)
        {
            isDropped = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = true;
        }
    }

    public void MouseUp()
    {
        isDropped = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = true;
    }

    private void Update()
    {
        if (goToGoal)
        {
            GoingToTheGoal();
        }
    }

    private void GoingToTheGoal()
    {
        startPoint.z -= Time.deltaTime * approachSpeed;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(startPoint);
        if (startPoint.z <= goalZCoord)
        {
            goToGoal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            GameManager.Instance.IncreaseMissed();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDropped)
        {
            if (other.transform.tag == "Container")
            {
                GameManager.Instance.IncreaseMissed();
                StartCoroutine(GoToContainerCoroutine(transform.position, other.transform.position));
                StartCoroutine(SqueezeCoroutine());
                StartCoroutine(DestroyCoroutine());
            }
        }
    }

    private IEnumerator GoToContainerCoroutine(Vector3 startPosition, Vector3 goal)
    {
        float t = 0;
        while (t <= 1)
        {
            yield return transform.position = Vector3.Lerp(startPosition, goal, t);
            t += Time.deltaTime * 2;
        }
    }

    private IEnumerator SqueezeCoroutine()
    {
        float t = 0;
        while (t <= 1)
        {
            yield return transform.localScale = Vector3.Lerp(Vector3.one * transform.localScale.x, Vector3.zero, t);
            t += Time.deltaTime * 2;
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
