using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformantOnBNPC : MonoBehaviour
{
    public InputField inputField;
    public Text inputFieldDisplayText;
    public Text text;
    public List <InterestingCharacters> bgList = new List<InterestingCharacters>();
    public InterestingCharacters c;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("hitest" + DialogeManager.Instance.conversedAboutCharectersList.Count);
           // Debug.Log(search("Ernest"));

        }
    }

    public void changedInput(string s)
    {
        search(inputFieldDisplayText.text);
       // setUpTextFields(search(s));
    }

    void search(string _name)
    {
        Debug.Log(_name);

        Debug.Log(CharacterManager.Instance.getCurrentBackgroundCharacterList().Count);
        setUpTextFields(CharacterManager.Instance.getCurrentBackgroundCharacterList(), _name);
    /*    InterestingCharacters caracter = 
            DialogeManager.Instance.conversedAboutCharectersList.Find(c => (c.name ==_name) || (c.name.Contains(_name)));
        Debug.Log(caracter.fullName + "found");*/

        // return caracter;
    }

    void setUpTextFields(List<InterestingCharacters> cli, string charName)
    {
        string con = "";
        string currentFact = "";
        List<string> keysFound = new List<string>() { };

        foreach (InterestingCharacters c in cli)
        {
            if (c.firstName.ToLower() == charName || c.LastName.ToLower() == charName ||
                c.firstName.ToLower().Contains(charName.ToLower()) || c.fullName.ToLower().Contains(charName))
            {
                con = c.fullName + ":\n";
                foreach (KeyValuePair<string, bool> kvp in c.characterFlags)
                {
                    if (kvp.Value == true)
                    {
                        keysFound.Add(kvp.Key);

                    }

                }

            }
        }
        foreach (string s in keysFound)
        {

            con = con + s+ "\n";
            //Debug.Log(translateCharacterFlags(s));//translateCharacterFlags(s) 

        }
        keysFound.Clear();
        text.text = con;
    }

    public string translateCharacterFlags(string str)
    {
        foreach(CharacterSearchBarFacts key in JsonLoader.Instance.ListOfBNPCFacts)
        {
            if (key.SearchBarKey == str || key.SearchBarKey.Contains(str))
            {
                Debug.Log("SHOULD RETURN " + key.BNPCsearchTranslation[0]);
                return  key.BNPCsearchTranslation[0];

            }
            else
            {
                return str;
            }            
        }
        return str;
            
         // CharacterSearchBarFacts fact  = JsonLoader.Instance.ListOfBNPCFacts.Find(c => (c.SearchBarKey == s));


       // return fact.BNPCsearchTranslation[Random.Range(0, 2)];
    }

    public void GetBNPCListofName() //currently tied to button press
    {

        string con = "";
        List<string> keysFound = new List<string>() { };
        foreach (InterestingCharacters c in CharacterManager.Instance.getCurrentBackgroundCharacterList())
        {
            
                con = con +  c.fullName + ":\n";
                text.text = con;
            
        }
    }




}
/*  foreach (CharacterSearchBarFacts FACT in JsonLoader.Instance.ListOfBNPCFacts)
          {
              Debug.Log("first hadn = " + s + "secomd is " + FACT.SearchBarKey.ToLower());
              if (FACT.SearchBarKey.ToLower() == s)
              {
                  con = con + FACT.BNPCsearchTranslation[UnityEngine.Random.Range(0, 2)] + "\n ";

              }
          }*/