using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class DialogeManager : MonoBehaviour //TODO refactor this later, just for testing plopping things here for now 
{



    public List<Dialoug> fullCharacterRootNodes;
    public List<Dialoug> currentPlayerOptions;

    JsonLoader jsn;

    public List<DialougStructure> AllOpinions; //not done the best way but for rapid testing for now 
    List<DialougStructure> highVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> midVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> lowVaueOpinions = new List<DialougStructure>();



    ConversationalCharacter currentCNPC;

    //add a node to check if explored 
    BackgroundCharacter bcg;

    //this is messy - move UI stuff to its own script me! 
    public Text GuicharacterName;
    public Text GuidialougText;
    public Text[] optionText;




    public Button[] PlayerButtons;



    public List<InterestingCharacters> conversedAboutChar;

    public void Start()
    {
        bcg = FindObjectOfType<BackgroundCharacter>();
        jsn =FindObjectOfType<JsonLoader>();
        AllOpinions = jsn.listOfConversations;
        currentCNPC = FindObjectOfType<CharacterManager>().characters[0]; //hard coded with tim for now 
        OrgnizeCNPCOpinions();// TODO add a p[arm to send in the current cnpc instyead od hc

        conversedAboutChar = bcg.GetFiltredCharerList();
        generateListOfPlayerOptions();
        PickStartingNodes();

    }

    private void OrgnizeCNPCOpinions()
    {

        foreach (DialougStructure op in AllOpinions)
        {
            if (op.topic.Contains("High"))
            {
                highVaueOpinions.Add(op);
            }
            if(op.topic.Contains("Mid"))
            {
                midVaueOpinions.Add(op);
            }
            if(op.topic.Contains("Low"))
            {
                lowVaueOpinions.Add(op);
            }
        }
    }

    private void Update()
    {
        printOpinions();
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            printCurrentNodes();
        }*/
    }

    void printOpinions()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach(DialougStructure op in AllOpinions)
            {
                if (op.topic.Contains("BTrueTYourHeart"))
                {
                    Debug.Log("findme"+op.NarrativeElements.bodyOne);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
    }
 


    void presentPlayerOptions(List<Dialoug> nodes)
    {
        int i = 0;
        foreach (Button b in PlayerButtons)
        {
            b.GetComponentInChildren<Text>().text = nodes[i].ButtonText;
            Debug.Log("helloworld"+ nodes[i].dialougText); //so the issue is wirth the node... says out of range
            string s = nodes[i].dialougText;
            b.onClick.AddListener(delegate { onClickPlayerOption(b, " " + s); });
            i++;
        }

    }


    void PickStartingNodes()
    {
        int i = 0;
        foreach(Button b in PlayerButtons)
        {
            currentPlayerOptions.Add(fullCharacterRootNodes[i]);           
            i++;
        }
        presentPlayerOptions(currentPlayerOptions);
    }

    //omnce explored flag it :) 

    void clearOptions()
    {
        foreach(Button b in PlayerButtons)
        {
            b.onClick.RemoveAllListeners();
        }
    }

    private void onClickPlayerOption(Button B, string s)
    {
        Debug.Log("this button " + B.name + "was clicked");
        GuidialougText.text = s;

    }


    //on button click via options remove the node ---- or move it to disabled nodes...
    public void generateListOfPlayerOptions()
    {
        foreach(InterestingCharacters c in conversedAboutChar)
        {
            fullCharacterRootNodes.Add(new Dialoug("what do you think of " + c.fullName + "?", 
                setNodeDialoug(c)));
          //  Debug.Log("testing" + c.fullName);
        }

    }

    void printCurrentNodes()
    {
        foreach(Dialoug c in fullCharacterRootNodes)
        {
            Debug.Log(c.ButtonText);
        }
    }

    private string setNodeDialoug(InterestingCharacters character)
    {
        string topic = generateDialougBasedOnTopic(character);
        return topic;
    }
    string  generateDialougBasedOnTopic(InterestingCharacters character)
    { 

        switch (selectATopic(character)) //initial topic
        {
            case "butcherRole":// adding more cases for the same key on cnpc, why they are keyed diff ( ex: value for family and social might have common narrative elenmtns) 
               return  checkValueOfCNPCAndReturnD("AnimalLover", character);//add something like this has been selected
            case "WillActOnLove":
                return checkValueOfCNPCAndReturnD("BTrueTYourHeart", character);
            case "polluterRole":
                return checkValueOfCNPCAndReturnD("Enviromentalist", character);
            case "familyPerson":
                return checkValueOfCNPCAndReturnD("FamilyPerson", character);
            default:
                break;
        }
        return "could not form a topic!";
    }

    private string selectATopic(InterestingCharacters character)
    {
        if (checkForIntrestingTopics(character) != "")
        {
            return checkForIntrestingTopics(character);
        }
        else
        {
            return FirstTopicFound(character);
        }

    }

    private string checkValueOfCNPCAndReturnD(string key, InterestingCharacters character)
    {//this is hard coded for now but later send in the conversational character 
     // Debug.Log(currentCNPC.ConvCharacterMoralFactors[key]);
        switch (currentCNPC.ConvCharacterMoralFactors[key])
        {
            case ConversationalCharacter.RatingVlaues.High:
                //Debug.Log("TESTTT:" + returnIntroTopic(highVaueOpinions, key) +
               // returnContrastingOpinion(highVaueOpinions, key, character));
                return
                returnIntroTopic(highVaueOpinions, key) +
                returnContrastingOpinion(highVaueOpinions, key, character);
            case ConversationalCharacter.RatingVlaues.Mid:
                //Debug.Log("TESTTT:" + returnIntroTopic(midVaueOpinions, key) +
           // returnContrastingOpinion(midVaueOpinions, key, character));
                return returnIntroTopic(midVaueOpinions, key) +
                returnContrastingOpinion(midVaueOpinions, key, character);
            default://low
                return returnIntroTopic(lowVaueOpinions, key) +
                 returnContrastingOpinion(lowVaueOpinions, key, character);
        }
    }
    //make this into one method  with the topic below --- 
    //also introduce the key they are talking about with a character name --- 
    //if occupation then talk about their occupation and their name ( like or displike ) and then append the rest of the dialoug node but fix the logic here 
    //also bring up other topics about a character if that topic has been selected 
    private string returnContrastingOpinion(List<DialougStructure> VaueOpinions,
        string key, InterestingCharacters character)
    {
        //fix this logic for some things that make sense vyt for now i am just grabbing anoither high value op
        foreach (DialougStructure op in VaueOpinions) //ex: all high opp
        {
            if (!op.topic.Contains(key))
            {
                return op.NarrativeElements.bodytwo; // change this si it does not rerun the first element but based on things that kinda make sense ( or random ) unsure --- 
            }
        }
        throw new NotImplementedException();
    }

    string returnIntroTopic(List<DialougStructure> opinions, string key) //the sent in list is of high./mid or low 
    {//selecting intro and the first part of the body here --- 
        foreach (DialougStructure op in opinions) //ex: all high opp
        {
            if (op.topic.Contains(key))
            {
                //Debug.Log("findme" + op.NarrativeElements.intro);
                return op.NarrativeElements.intro + op.NarrativeElements.bodyOne;
            }
        }
        return "NO TOPIC WAS FOUND --- need to author these topics  " + key;
    }

    void assembleADialougString()
    {
        string s;
    }


    private string FirstTopicFound(InterestingCharacters character)
    {
        string s = "";
        foreach (KeyValuePair<string, bool> kvp in character.characterFlags)
        {
            if (kvp.Value) //checking for the first topic that is true - set it as an inital topic 
            {
                s = kvp.Key;
                return s;
            }
            else
            {
                s = "this character has no intresesting topics";
            }
        }
        return s;
    }

    private string checkForIntrestingTopics(InterestingCharacters character) //return topic
    { //make this into a switch stat - therse are the topics i wote things for... so searching for them first - 
        if(character.characterFlags["WillActOnLove"] == true)
        {
            return "WillActOnLove";
        }
        if (character.characterFlags["polluterRole"] == true)
        {
            return "polluterRole";
        }
        if (character.characterFlags["butcherRole"] == true)
        {
            return "butcherRole";
        }
        if (character.characterFlags["familyPerson"] == true)
        {
            return "familyPerson";
        }
        else { return ""; }
    }

    public void GetListOfTalkedAboutNPC()
    {

    }

    public void GetPlayerOptions()
    {
        //links to nodes 
    }

}



/* OLD CODE 
 
 
 
         public void Start()
        {        //for testing purp
            dialougNodes.Add(addingNodeManually("greeting", "hello there! ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
            dialougNodes.Add(addingNodeManually("oh no", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
            dialougNodes.Add(addingNodeManually("what", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));

        //Debug.Log(dialougNodes[0].GetDialougText());

        //manual for testing purp
        dialougNodes[0].setConnectedNodes(dialougNodes[1], dialougNodes[2]);
        GuidialougText.text = dialougNodes[0].GetDialougText();
        
        for(int i=0;i < optionText.Count()-1; i++)
        {
            if(dialougNodes[0].getNodeOptiontext(i)==null)
                return;
            optionText[i].text = dialougNodes[0].getNodeOptiontext(i);
        }
     

    }

    public void Update()
        {
        }

        public void addDialougNode(Dialoug dNode)
        {
            if (dNode == null)
                return;
            dialougNodes.Add(dNode);
        }


        //ugly!! maybe just pass in a list of floats for each of the values instead two core ones? 
        Dialoug addingNodeManually(string labe, string dText, float[] values)
        {

            Dialoug d = new Dialoug(
            Dialoug.DialougType.introduction,
            labe,
            dText,
            addValues(values));//need to add connected nodes --- 
            return d;
        }

        //duplicated method - unify this into value system instead of character and dialoug one 
        private List<KeyValuePair<Dialoug.DialougValue, float>> addValues(float[] values)
        {
            List<KeyValuePair<Dialoug.DialougValue, float>> listofValues =
                new List<KeyValuePair<Dialoug.DialougValue, float>>();

            int i = 0;
            foreach (Dialoug.DialougValue Dvalue in Enum.GetValues(typeof(Dialoug.DialougValue)))
            {
                listofValues.Add((new KeyValuePair<Dialoug.DialougValue, float>
                    (Dvalue, values[i])));
                i++;
            }

            return listofValues;
        }

        void runNode(Dialoug node)
        {

        }


        void getNextNode()
        {

        }
*/