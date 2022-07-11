using System.Collections;
using UnityEngine;
using OculusSampleFramework;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private GameObject bottle;
    private bool isCanSpawn;

    private void Start()
    {
        isCanSpawn = true;
        Spawn();
    }

    private void Update()
    {
        if (isCanSpawn && bottle.GetComponent<DistanceGrabbable>().isGrabbed)
        {
            isCanSpawn = false;
            Spawn();
        }
    }

    private void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        bottle = Instantiate(
                prefab,
                gameObject.transform.position,
                Quaternion.Euler(-90, 0, 0));
        isCanSpawn = true;
    }
}