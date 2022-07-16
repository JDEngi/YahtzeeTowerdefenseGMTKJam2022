using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnablePrefab;
    // Start is called before the first frame update

    public void MakeWave(int numSpawns, float spawnInterval)
    {
        StartCoroutine(CallSpawn(numSpawns, spawnInterval));
    }

    private IEnumerator CallSpawn(int numSpawns, float spawnInterval)
    {
        for (int i = 0; i < numSpawns; i++)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        GameObject newEnemy = Instantiate(spawnablePrefab, transform.parent);
        newEnemy.transform.position = transform.position;
    }
}
