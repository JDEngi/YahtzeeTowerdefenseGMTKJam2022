using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] public GameObject GruntPrefab, SoldierPrefab, TankPrefab;
    // Start is called before the first frame update

    public void MakeWave(int numSpawns, float spawnInterval)
    {
        StartCoroutine(CallSpawn(numSpawns, spawnInterval));
    }

    private IEnumerator CallSpawn(int numSpawns, float spawnInterval)
    {
        for (int i = 1; i <= numSpawns; i++)
        {
            if (i % 3 == 0)
            {
                Spawn(EnemyTypes.soldier);
            }
            if (i % 5 == 0)
            {
                Spawn(EnemyTypes.tank);
            }
            if((i % 3 != 0) && (i % 5 != 0))
            {
                Spawn(EnemyTypes.grunt);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn(EnemyTypes type)
    {
        GameObject spawnSelection = null;
        switch (type)
        {
            case EnemyTypes.grunt:
                spawnSelection = GruntPrefab;
                break;
            case EnemyTypes.soldier:
                spawnSelection = SoldierPrefab;
                break;
            case EnemyTypes.tank:
                spawnSelection = TankPrefab;
                break;
            default:
                spawnSelection = GruntPrefab;
                Debug.LogWarning("Tyring to spawn enemy of invalid enemy type");
                break;
        }
        GameObject newEnemy = Instantiate(spawnSelection, transform.parent);
        newEnemy.transform.position = transform.position;
    }
}
