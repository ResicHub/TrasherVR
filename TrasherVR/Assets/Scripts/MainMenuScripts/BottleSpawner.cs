using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool isTrashOnTable = true;

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
        if (!isTrashOnTable)
        {
            GameObject bottle = Instantiate(
                prefab,
                new Vector3(0,100,0),
                gameObject.transform.rotation);
            StartCoroutine(SpawnCoroutine(bottle, transform.position));
        }
    }

    private IEnumerator SpawnCoroutine(GameObject obj, Vector3 finalPosition)
    {
        Vector3 finalScale = obj.transform.localScale;
        obj.transform.localScale = Vector3.zero;

        obj.transform.position = finalPosition;
        float spawnTimer = 0f;
        float spawningTime = 0.25f;
        while (spawnTimer < spawningTime || obj.transform.localScale != finalScale)
        {
            yield return obj.transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, spawnTimer / spawningTime);
            spawnTimer += Time.deltaTime;
        }
    }
}
