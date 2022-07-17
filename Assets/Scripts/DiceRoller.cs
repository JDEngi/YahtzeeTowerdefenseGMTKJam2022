using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    public SingleDice diceOriginal;
    private GameObject diceContainer;
    private List<SingleDice> dices;
    public GameObject[] buttons;
    public TMP_Text RerollsLeftText;
    private int RerollsLeft;
    public Button RollDicesButton;


    public void Awake()
    {
        GameManager.OnGameStateChanged += OnGameManagerOnStateChanged;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameManagerOnStateChanged;
    }


    public void OnGameManagerOnStateChanged(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.START:
                // Set number of dice to 3
                InitializeDice();
                break;
            case GameStates.RUN:
                break;
            case GameStates.PAUSE:
                break;
            case GameStates.GAMEOVER:
                DestroyDices();
                break;
            default:
                break;
        }
    }

    private void DestroyDices()
    {
        foreach (SingleDice dice in dices)
        {
            Destroy(dice);
        }

        Destroy(diceContainer);
    }

    private void InitializeDice()
    {
        diceContainer = new GameObject();
        dices = new List<SingleDice>();
        int startDices = 3;
        for (int i = 0; i < startDices; i++)
        {
            AddDice();
        }
        RerollsLeft = 3;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LowerRerolls()
    {
        RerollsLeft--;
        SetRerollsText();
    }

    private void SetRerollsText()
    {
        RerollsLeftText.text = "Rerolls left: " + RerollsLeft;
    }

    private void UnlockAllDices()
    {
        foreach (SingleDice dice in dices)
        {
            dice.Unlock();
        }
    }

    public void RollDice()
    {
        if (RerollsLeft <= 0)
        {
            UnlockAllDices();
            RerollsLeft = 3;
        }
        StartCoroutine(RollTheDicesAnimated());
    }


    IEnumerator RollTheDicesAnimated()
    {
        LockRollDicesButton();
        int numberOfAnimatedRolls = 5;
        for (int i = 0; i < numberOfAnimatedRolls; i++)
        {
            foreach (SingleDice dice in dices)
            {
                int rolledNumber = Mathf.RoundToInt(Random.Range(0.5f, 6.5f));
                dice.ChangeNumber(rolledNumber);
            }
            yield return new WaitForSeconds(.05f * i);
        }
        LowerRerolls();
        CheckForCombos();
        UnlockRollDicesButton();
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

            if (sameNumbers >= 3)
            {
                EnableButton(0, i);
                //Debug.Log("You threw a three of a kind!");
            }
            if (sameNumbers >= 4)
            {
                //EnableButton(1, i);
                //Debug.Log("You threw a carre!");
            }
            if (sameNumbers >= 5)
            {
                EnableButton(4, i);
                //Debug.Log("You threw a Yathzee!");
            }
            if (sameNumbers >= 6)
            {
                EnableButton(5, i);
                //Debug.Log("You threw a MEGA Yathzee!");
            }
        }

        //section for detecting streets.
        int streetCount = 0;
        for (int i = 0; i < 3; i++)
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
            if (streetCount >= 3)
            {
                //EnableButton(0, i);
                //Debug.Log("You threw a tiny street!");
            }
            if (streetCount >= 4)
            {
                EnableButton(1, i + 1);
                //Debug.Log("You threw a small street!");
            }
            if (streetCount >= 5)
            {
                EnableButton(2, 1);
                //Debug.Log("You threw a big street!");
            }
            if (streetCount >= 6)  //street bigger then 6 is not possible (in theory...)
            {
                //EnableButton(4, i); //The powerlevel is always 1
                //Debug.Log("You threw a huge street!");
            }
            streetCount = 0;
        }

        //This is for detecting full house
        int detectedPair = 0;
        int detectedThreeOfAKind = 0;
        for (int i = 0; i < 6; i++)
        {
            if (sameNumbersList[i] >= 3)
            {
                if (detectedThreeOfAKind > 0)
                {
                    detectedPair = i + 1;
                }
                detectedThreeOfAKind = i + 1;
            }
            else if (sameNumbersList[i] == 2)
            {
                detectedPair = i + 1;
            }
        }
        if (detectedPair > 0 && detectedThreeOfAKind > 0)
        {
            EnableButton(3, Mathf.FloorToInt((detectedPair + detectedThreeOfAKind) / 2));
            //Debug.Log("You threw a full house!");
        }
    }

    private void DisableAllButtons()
    {
        foreach (GameObject buttonObj in buttons)
        {
            Button button = buttonObj.GetComponent<Button>();
            button.interactable = false;
        }
    }
    
    private void EnableButton(int buttonNumber, int powerlevel)
    {
        Button button = buttons[buttonNumber].GetComponent<Button>();
        button.interactable = true;

        TowerOptionScript selection = buttons[buttonNumber].GetComponent<TowerOptionScript>();
        selection.power = powerlevel;
    }

    private void UnlockRollDicesButton()
    {
        RollDicesButton.interactable = true;
    }
    private void LockRollDicesButton()
    {
        RollDicesButton.interactable = false;
    }
}
