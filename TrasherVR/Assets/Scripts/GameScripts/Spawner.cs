using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float respawnTime;
    private float timer;
    public bool isSpawning = false;

    [SerializeField]
    private List<GameObject> prefabs;

    public void SetRespawn(float value)
    {
        respawnTime = value;
        timer = value;
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
    
    private void Spawn()
    {
        Instantiate(
            prefabs[Random.Range(0, prefabs.Count)], 
            transform.position + new Vector3((Random.value * 2 - 1) / 2, 0, 0), 
            Quaternion.Euler(0, Random.Range(-180, 180), 0),
            transform);
    }

    public void SetSpawnDifficulty(int mode)
    {
        switch (mode)
        {
            case 1:
                prefabs.RemoveRange(6, 6);
                break;
            case 2:
                prefabs.RemoveRange(9, 3);
                break;
            default:
                break;
        }
    }
}
