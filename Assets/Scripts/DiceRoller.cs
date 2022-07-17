using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    //public TMP_Text diceText;
    public SingleDice diceOriginal;
    private GameObject diceContainer;
    private List<SingleDice> dices;
    public Button[] buttons;
    //private List<>

    // Start is called before the first frame update
    void Start()
    {
        diceContainer = new GameObject();
        dices = new List<SingleDice>();
        int startDices = 3;
        for (int i = 0; i < startDices; i++)
        {
            AddDice();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollDice()
    {
        StartCoroutine(RollTheDicesAnimated());
    }

    IEnumerator RollTheDicesAnimated()
    {
        int numberOfAnimatedRolls = 5;
        for (int i = 0; i < numberOfAnimatedRolls; i++)
        {
            foreach (SingleDice dice in dices)
            {
                int rolledNumber = Mathf.RoundToInt(Random.Range(0.5f, 6.5f));
                dice.ChangeNumber(rolledNumber);  //Only the last throw must change the number.
            }
            yield return new WaitForSeconds(.05f * i);
        }
        CheckForCombos();
    }

    public int GetDiceCount()
    {
        return dices.Count;
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

    public void RemoveDice(int numOfDice)
    {
        if (dices.Count < numOfDice)
        { 
            return; 
        }

        for (int i = 0; i < numOfDice; i++)
        {
            int removalIdx = diceContainer.transform.childCount - 1 - i;
            dices.RemoveAt(removalIdx);
            Destroy(diceContainer.transform.GetChild(removalIdx).gameObject);
        }
    }

    public void CheckForCombos()
    {
        DisableAllButtons();
        List<int> numbers = new List<int>();
        foreach (var dice in dices) numbers.Add(dice.GetNumber());

        List<int> sameNumbersList = new List<int>();

        for (int i = 1; i <= 6; i++)
        {
            int sameNumbers = numbers.FindAll(x => x.Equals(i)).Count;
            sameNumbersList.Add(sameNumbers);
            //Debug.Log("for " + i + "there is " + sameNumbers + " times the same number");

            if (sameNumbers == 3)
            {
                EnableButton(0);
                //Debug.Log("You threw a three of a kind!");
            }
            else if (sameNumbers == 4)
            {
                EnableButton(1);
                //Debug.Log("You threw a carre!");
            }
            else if (sameNumbers == 5)
            {
                EnableButton(4);
                //Debug.Log("You threw a Yathzee!");
            }
            else if (sameNumbers >= 6)
            {
                EnableButton(5);
                //Debug.Log("You threw a MEGA Yathzee!");
            }
        }

        //section for detecting streets.
        int streetCount = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = i; j < 6; j++)
            {
                if (sameNumbersList[j] < 1)
                {
                    break;
                }
                else
                {
                    streetCount++;
                }
            }
            if (streetCount == 3)
            {
                EnableButton(0);
                //Debug.Log("You threw a tiny street!");
            }
            else if (streetCount == 4)
            {
                EnableButton(1);
                //Debug.Log("You threw a small street!");
            }
            else if (streetCount == 5)
            {
                EnableButton(2);
                //Debug.Log("You threw a big street!");
            }
            else if (streetCount >= 6)  //street bigger then 6 is not possible (in theory...)
            {
                EnableButton(4);
                //Debug.Log("You threw a huge street!");
            }
            streetCount = 0;
        }

        //This is for detecting full house
        bool detectedPair = false;
        bool detectedThreeOfAKind = false;
        for (int i = 0; i < 6; i++)
        {
            if (sameNumbersList[i] >= 3)
            {
                if (detectedThreeOfAKind)
                {
                    detectedPair = true;
                }
                detectedThreeOfAKind = true;
            }
            else if (sameNumbersList[i] == 2)
            {
                detectedPair = true;
            }
        }
        if (detectedPair && detectedThreeOfAKind)
        {
            EnableButton(3);
            //Debug.Log("You threw a full house!");
        }
    }

    private void DisableAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    
    private void EnableButton(int buttonNumber)
    {
        buttons[buttonNumber].interactable = true;
    }
}
