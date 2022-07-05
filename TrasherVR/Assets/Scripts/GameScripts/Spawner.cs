using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float respawnTime = 1;
    private float timer;

    [SerializeField]
    private List<GameObject> prefabs;

    public bool isSpawning = false;
    void Start()
    {
        timer = respawnTime;
    }

    void FixedUpdate()
    {
        if (isSpawning)
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                Spawn();
                timer = respawnTime;
            }
        }
    }

    public void SetRespawn(float value)
    {
        respawnTime = value;
    }

    private void Spawn()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Count)], 
                    transform.position + new Vector3(Random.value * 2 - 1,0,0), 
                    Quaternion.Euler(0,Random.Range(-180, 180),0),
                    transform);
    }
}
