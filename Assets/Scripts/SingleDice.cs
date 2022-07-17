using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a dice
public class SingleDice : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    private int number = 1;
    private bool numberIsLocked;

    //private MeshRenderer renderProperties;
    //public GameObject ContainedElement;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[6];  //this is the blanco die
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseEnter()
    {
    }

    void OnMouseExit()
    {
    }

    void OnMouseDown()
    {
        numberIsLocked = !numberIsLocked;
        if(numberIsLocked)
        {
            rend.sharedMaterial = material[number + 6]; //This will give the samen die, but gray.
        }
        else
        {
            rend.sharedMaterial = material[number - 1];
        }
    }

    public void Unlock()
    {
        numberIsLocked = false;
        rend.sharedMaterial = material[number - 1];
    }
    public void ChangeNumber(int aNumber)
    {
        if (!numberIsLocked)
        {
            number = aNumber;
            rend.sharedMaterial = material[number - 1];
        }
    }

    public int GetNumber()
    {
        return number;
    }
}
