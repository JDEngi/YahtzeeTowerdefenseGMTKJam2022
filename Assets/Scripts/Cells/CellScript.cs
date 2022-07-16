using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class CellScript : MonoBehaviour
{
    public UnityEvent<Transform>[] OnClickHandler;
    public Material MaterialWhenVisible;
    public Material MaterialWhenNotVisible;

    public GameObject ContainedElement = null;
    public bool IsPath;

    private int x;
    private int y;

    private MeshRenderer renderProperties;

    void Start()
    {
        renderProperties = GetComponent<MeshRenderer>();
        if (IsPath)
        {
            ContainedElement = new GameObject("Path filler");
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
            renderProperties.material = MaterialWhenVisible;
        }
    }
    
    void OnMouseExit()
    {
        renderProperties.material = MaterialWhenNotVisible;
    }

    void OnMouseDown()
    {
        Debug.Log("Click on Cell " + x + " " + y);

        if (ContainedElement == null)
        {
            foreach (UnityEvent<Transform> lHandler in OnClickHandler)
            {
                lHandler.Invoke(transform);
            }
        }
    }
}
