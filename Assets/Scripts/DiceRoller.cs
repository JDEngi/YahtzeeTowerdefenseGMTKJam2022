using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    //public TMP_Text diceText;
    public GameObject diceNumberOriginal;
    private GameObject diceNumberContainer;
    //private List<>

    // Start is called before the first frame update
    void Start()
    {
        diceNumberContainer = new GameObject();
        AddDice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollDice()
    {
    //    var diceNumbers = new List<GameObject>();
    //    foreach (Transform diceNumber in diceNumberContainer.transform) diceNumbers.Add(diceNumber.gameObject);

        //diceNumbers.ForEach()

    //    foreach (Transform diceNumber in diceNumberContainer.transform)
    //    {
    //        int rolledNumber = Mathf.RoundToInt(Random.Range(0.5f, 6.5f));
    //        TMP_Text diceText = (TMP_Text) diceNumber.gameObject;
    //        diceNumber.gameObject.text = rolledNumber.ToString();
    //    }
        
    }

    public void AddDice()
    {
        //GameObject diceNumber = Instantiate(diceNumberOriginal, new Vector3(diceNumberContainer.transform.childCount, 0, 0), diceNumberOriginal.transform.rotation);
        GameObject diceNumber = Instantiate(diceNumberOriginal, this.transform);
        diceNumberContainer.transform.SetParent(diceNumber.transform.parent);
        diceNumber.transform.position += new Vector3(diceNumberContainer.transform.childCount, 0.0f, 0.0f);
        diceNumber.name = "diceNumber" + diceNumberContainer.transform.childCount;
    }

    public void RemoveDice()
    {
        Destroy(diceNumberContainer.transform.GetChild(diceNumberContainer.transform.childCount - 1).gameObject);
    }

    private void CheckForCombos()
    {

    }
}
