using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFountain : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs;
    private float timer = 10f;
    private bool isActive = false;
    private bool isDone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trash")
        {
            isActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isDone && isActive)
        {
            timer -= Time.deltaTime;
            GameObject obj = Instantiate(
                prefabs[Random.Range(0, prefabs.Count)],
                gameObject.transform.position,
                Quaternion.identity);
            Vector3 direction = new Vector3(Random.Range(-0.2f, 0.2f), 1.5f, Random.Range(-0.2f, 0.2f));
            obj.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            
            if (timer <= 0)
            {
                isDone = true;
            }
        }
    }
}
