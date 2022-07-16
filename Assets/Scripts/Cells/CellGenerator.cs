using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    public GameObject Prefab;

    public float Spacing;
    public int NumXElements;
    public int NumYElements;

    public void Generate()
    {
        for (int x = 0; x < NumXElements; x++)
        {
            for (int y = 0; y < NumYElements; y++)
            {
                Object newCell = PrefabUtility.InstantiatePrefab(Prefab, transform);
                Transform newCellTransform = newCell.GameObject().transform;
                newCellTransform.position += new Vector3(x * Spacing, 0, y * Spacing);
                newCellTransform.position += new Vector3(Spacing / 2, 0, Spacing / 2);

                CellScript cellScript = newCell.GetComponent<CellScript>();
                cellScript.SetCellCoordinate(x, y);
            }
        }
    }
}
