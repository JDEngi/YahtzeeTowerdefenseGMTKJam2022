using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionScript : MonoBehaviour
{
    public int power = 1;

    public GameObject[] TowerPrefabs;

    public void Activate(TowerSelectionScript target)
    {
        target.SetTower(TowerPrefabs[power - 1]);
    }
}
