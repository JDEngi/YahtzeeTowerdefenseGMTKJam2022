using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a dice
public class SingleDice : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    private int number;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[6];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeNumber(int aNumber)
    {
        number = aNumber;
        rend.sharedMaterial = material[number - 1];
    }

    public int GetNumber()
    {
        return number;
    }
}
