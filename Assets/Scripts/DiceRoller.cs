using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    //public TMP_Text diceText;
    public SingleDice diceOriginal;
    private GameObject diceContainer;
    private List<SingleDice> dices;
    //private List<>

    // Start is called before the first frame update
    void Start()
    {
        diceContainer = new GameObject();
        dices = new List<SingleDice>();
        //AddDice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollDice()
    {
        foreach (SingleDice dice in dices)
        {
            int rolledNumber = Mathf.RoundToInt(Random.Range(0.5f, 6.5f));
            dice.ChangeNumber(rolledNumber);
        }
        CheckForCombos();
    }

    public void AddDice()
    {
        SingleDice dice = Instantiate(diceOriginal) as SingleDice;        
        int nrOfChilds = diceContainer.transform.childCount;
        dice.transform.position += new Vector3((nrOfChilds % 3) * 1.2f, 0.0f, -(Mathf.Floor(nrOfChilds / 3) * 1.2f));
        dice.name = "diceNumber" + nrOfChilds;
        dice.transform.SetParent(diceContainer.transform);
        dices.Add(dice);
    }

    public void RemoveDice()
    {
        dices.RemoveAt(dices.Count - 1);
        Destroy(diceContainer.transform.GetChild(diceContainer.transform.childCount - 1).gameObject);
    }

    private void CheckForCombos()
    {
        List<int> numbers = new List<int>();
        foreach (var dice in dices) numbers.Add(dice.GetNumber());

        
        for (int i = 1; i < 6; i++)
        {
            int sameNumbers = numbers.FindAll(x => x == i).Count;
            //Debug.Log("for " + i + "there is " + sameNumbers + " times the same number");
            if (sameNumbers > 2)
            {
                Debug.Log("You have trown a three of a kind!");
            }
        }
        
    }
}
