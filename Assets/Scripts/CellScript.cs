using System;
using UnityEngine;
using UnityEngine.Events;

public class CellScript : MonoBehaviour
{
    public UnityEvent<Transform>[] mOnClickHandler;

    void OnMouseEnter()
    {

    }
    
    void OnMouseExit()
    {

    }

    void OnMouseDown()
    {
        foreach (UnityEvent<Transform> lHandler in mOnClickHandler)
        {
            lHandler.Invoke(transform);
        }
    }
}
