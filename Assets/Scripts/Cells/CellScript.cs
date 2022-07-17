using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CellScript : MonoBehaviour
{
    public CellGenerator Generator;
    public GameObject ContainedElement;
    public bool IsPath;

    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    private MeshRenderer renderProperties;

    void Start()
    {
        renderProperties = GetComponent<MeshRenderer>();
        if (IsPath)
        {
            ContainedElement = Instantiate(Generator.PathPrefab, transform);
        }
    }

    public void SetCellCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseEnter()
    {
        if (ContainedElement == null)
        {
            renderProperties.material = Generator.MaterialWhenVisible;
        }
    }
    
    void OnMouseExit()
    {
        renderProperties.material = Generator.MaterialWhenNotVisible;
    }

    void OnMouseDown()
    {
        if (ContainedElement != null) return;
        GameObject tower = Generator.TowerSelectionScript.SelectedTowerPrefab;
        if (tower == null) return;

        PlaceTower(tower);
        
    }

    public void PlaceTower(GameObject tower)
    {
        DiceRoller diceRoller = FindObjectOfType<DiceRoller>();
        if (diceRoller)
        {
            diceRoller.RemoveDice(tower.GetComponent<AbstractTower>().BuildCost);

            Generator.TowerSelectionScript.SelectedTowerPrefab = null;
            GameObject ContainedElement = Instantiate(tower, transform);
            ContainedElement.transform.localScale = new Vector3(1.1f, 1, -1.1f);

            diceRoller.CheckForCombos();
        }
    }
}
