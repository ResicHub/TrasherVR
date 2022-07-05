using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    private float timer = 0;
    private float activationTime = 2.0f;
    private bool isActive = false;
    private Material material;
    private Color startColor;

    private void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        startColor = material.color;
    }


    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (!isActive && collision.collider.tag == "GameController")
        {
            timer += Time.deltaTime;
            ResetColor();
            if (timer >= activationTime)
            {
                isActive = true;
                timer = 0;
                Activate();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isActive && collision.collider.tag == "GameController")
        {
            timer = 0;
            ResetColor();
        }
    }

    private void Activate()
    {
        GameObject sphere = Instantiate(
            GameObject.CreatePrimitive(PrimitiveType.Sphere),
            gameObject.transform.position,
            Quaternion.identity,
            gameObject.transform);
        sphere.AddComponent<Rigidbody>();
    }

    private void ResetColor()
    {
        float colorIndex = (activationTime - timer) / 2;
        material.color = new Color(startColor.r * colorIndex, startColor.g * colorIndex, startColor.b * colorIndex);
    }
}
