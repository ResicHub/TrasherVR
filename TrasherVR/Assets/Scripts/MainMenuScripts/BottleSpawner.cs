using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private bool isTrashOnTable = true;
    [SerializeField]
    private Vector3 spawnPosition;
    
    private void OnTriggerStay(Collider other)
    {
        if (!isTrashOnTable && other.tag == "Trash")
        {
            isTrashOnTable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
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
            StartCoroutine(SpawnCoroutine());
        }
    }
    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (!isTrashOnTable)
        {
            GameObject bottle = Instantiate(
                prefab,
                new Vector3(0, 100, 0),
                gameObject.transform.rotation);
            Vector3 finalScale = bottle.transform.localScale;

            bottle.transform.localScale = Vector3.zero;
            bottle.transform.position = spawnPosition;
            float spawnTimer = 0f;
            float spawningTime = 0.25f;
            while (spawnTimer < spawningTime || bottle.transform.localScale != finalScale)
            {
                yield return bottle.transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, spawnTimer / spawningTime);
                spawnTimer += Time.deltaTime;
            }
        }
    }
}