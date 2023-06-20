using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> tanks;

    GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        SpawnTank();
    }



    void SpawnTank()
    {
        if(gc.totalEnemies == 0 || gc.spawnedEnemies.Count == 5)
        {
            Invoke("SpawnTank", Random.Range(3f, 5f));
            return;
        }

        gc.spawnedEnemies.Add(
            Instantiate(tanks[Random.Range(0, tanks.Count)],
            transform.position,
            transform.rotation *= new Quaternion(0, -1, 0, 0)));

        gc.totalEnemies--;

        Invoke("SpawnTank", Random.Range(3f, 5f));
    }
}
