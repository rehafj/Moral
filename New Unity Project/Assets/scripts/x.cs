using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x : MonoBehaviour
{/*
    //this is hardcoded for now but will change this structure later 
    // maybe xml, or inkl, or fungus...? 
     List<Dialoug> dialougNodes;


    //this is messy - move UI stuff to its own script me! 
    public Text GuicharacterName;
    public Text GuidialougText;
    public Text[] optionText; 


    public void Start()
    {        //for testing purp
        dialougNodes.Add( addingNodeManually( "greeting", "hello there! ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        dialougNodes.Add(addingNodeManually("oh no", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        dialougNodes.Add(addingNodeManually("what", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));

    }

    public void Update()
    {
        GuidialougText.text = dialougNodes[0].GetDialougText();
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
        foreach(Dialoug.DialougValue Dvalue in Enum.GetValues(typeof(Dialoug.DialougValue)))
        {
            listofValues.Add((new KeyValuePair<Dialoug.DialougValue, float>
                (Dvalue, values[i])));
               i++;
        }

        return listofValues;
    }

    void runNode(Dialoug node) { 

    }


    void getNextNode()
    {

    }
*/

    //remove the node, go to next node.. 
}
//float[] values = { 10, 0, 0,0,0,0,0,70 };

/* OLD CODE 
 * 
 * 
 * 
 * 
 * 
 * 
 *  /*   AllOpinions = jsn.listOfConversations;
           currentCNPC = FindObjectOfType<CharacterManager>().characters[0]; //hard coded with tim for now 
           OrgnizeCNPCOpinions();// TODO add a p[arm to send in the current cnpc instyead od hc

           conversedAboutChar = bgchar.GetFiltredCharerList();
           generateListOfDialougNodes();
           //PickStartingNodes();

           playerName = "Rehaf";
           StartCoroutine(TypeInDialoug("hey there" + playerName +
               " \n thanks for meeting me for brunch! Boy has the town been eventful lately! "));
           disableorEnablePlayerButtons();*/
//testinga tree node
/*      testingTree.root = new Dialoug("test", "test");
      testingTree.root.children = new List<Dialoug>();//add temp list of other topics */
/*
*
* 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 
                 b.gameObject.GetComponentInParent<GameObject>().SetActive(true);

 
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


 IEnumerator co1(Dialoug d)
    {
        //Debug.Log("+ called second");
        displayDialougOpinion();

        yield return null;
    }
    IEnumerator coWaitForExecution(Dialoug d)
    {
        Debug.Log("+ called first");
        StartCoroutine(TypeInDialoug(d.IntroducingATopicdialoug));
        yield return co1(d);
    }
*/


/* old code base 
 
 ///
 /// 
 /// 
 /// 
 /// 
 /// 
 
   /*    private string checkValueOfCNPCAndReturnD(string key, InterestingCharacters character)
        {//this is hard coded for now but later send in the conversational character 
         // Debug.Log(currentCNPC.ConvCharacterMoralFactors[key]); - fix logic 
            switch (currentCNPC.ConvCharacterMoralFactors[key])
            {
                case ConversationalCharacter.RatingVlaues.High:
                    return returnIntroTopic(highVaueOpinions, key) +
                    returnContrastingOpinion(highVaueOpinions, key, character);
                case ConversationalCharacter.RatingVlaues.Mid:
                    //Debug.Log("TESTTT:" + returnIntroTopic(midVaueOpinions, key) + returnContrastingOpinion(midVaueOpinions, key, character));
                    return returnIntroTopic(midVaueOpinions, key) +
                    returnContrastingOpinion(midVaueOpinions, key, character);
                default://low
                    return returnIntroTopic(lowVaueOpinions, key) +
                     returnContrastingOpinion(lowVaueOpinions, key, character);
            }
        }*/

/*
    private void setUprootNodes()
    {
        foreach (InterestingCharacters c in conversedAboutCharectersList)
        {
            //for testing 

            Dialoug tempRootDialoug = new Dialoug("hmmm should I bring up " + c.fullName + "?",
                setNodeDialoug(c)); //change this into npc text instead of player buttons 
            fullCharacterRootNodes.Add(tempRootDialoug);
            Tree tempCharacterFullTree = new Tree();
            tempCharacterFullTree.root = tempRootDialoug;
            allCharacterConversationsTrees.Add(tempCharacterFullTree);// the tree  listy should hold all starting nodes     
        }
    }*/
/*

    ///////////////////////////////////old code///////////////////////////////////////////////

    private void setUprootNodes()
    {
        foreach (InterestingCharacters c in conversedAboutCharectersList)
        {
            //for testing 

            Dialoug tempRootDialoug = new Dialoug("hmmm should I bring up " + c.fullName + "?",
                setNodeDialoug(c)); //change this into npc text instead of player buttons 
            fullCharacterRootNodes.Add(tempRootDialoug);
            Tree tempCharacterFullTree = new Tree();
            tempCharacterFullTree.root = tempRootDialoug;   
            allCharacterConversationsTrees.Add(tempCharacterFullTree);// the tree  listy should hold all starting nodes     
        }
    }

    public void setUpDialougs()
    {
        foreach (InterestingCharacters c in conversedAboutCharectersList)
        {
            //for testing 

            Dialoug tempRootDialoug = new Dialoug("hmmm should I bring up " + c.fullName + "?",
                setNodeDialoug(c)); //change this into npc text instead of player buttons 
            fullCharacterRootNodes.Add(tempRootDialoug);
            Tree tempCharacterFullTree = new Tree();
            tempCharacterFullTree.root = tempRootDialoug;
            //to do method that loosp over topics unexplored and populates the tree with children (recursion)
            tempRootDialoug.children = returnConnectedNodes(tempRootDialoug, c);
            allCharacterConversationsTrees.Add(tempCharacterFullTree);// the tree  listy should hold all starting nodes 

            //  Debug.Log("testing" + c.fullName);
            printATree(tempCharacterFullTree);
        }
    }



    //////////////////////////////////////////////////////////////////////////////////////\
    //       //to do method that loosp over topics unexplored and populates the tree with children (recursion)
    ///tempRootDialoug.children = returnConnectedNodes(tempRootDialoug, c);
    private void OrgnizeCNPCOpinions()
    {

        foreach (DialougStructure op in AllOpinions)
        {
            if (op.topic
"High"))
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            PickStartingNodes();

           // printCurrentNodes();
        }
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

    //change current opinion to be the current node in a tree
    //add a method to mreplace buttons with new text (opinions based on current tree) 

    void presentPlayerOptions(List<Dialoug> nodes)
    {
        disableorEnablePlayerButtons();
        int i = 0;
        foreach (Button b in PlayerButtons)
        {
            b.GetComponentInChildren<Text>().text = nodes[i].ButtonText;
            Debug.Log("helloworld"+ nodes[i].dialougText); //so the issue is wirth the node... says out of range
            string s = nodes[i].dialougText;

            b.onClick.AddListener(delegate { onClickPlayerOption(b, s); });
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
        //GuidialougText.text = s;
        StartCoroutine(TypeInDialoug(s));

        populateCNPCOpinions();

  }

    private void populateCNPCOpinions()
    {
        int i = 0;
        foreach (Text t in CNPCoptionText)
        {
            Debug.Log(i);


            int X = UnityEngine.Random.Range(0, currentThoughts.Count-1);
            if (i == 0)
            {
                //add the selected opinion as an element
                t.text = translateOpinionIntoText(selectedOpnion);
            }
            else
            {           
                t.text = translateOpinionIntoText( currentThoughts[X]);
            }
            i++;
        }


    }


    public void generateListOfDialougNodes()
    {
        foreach(InterestingCharacters c in conversedAboutCharectersList)
        {
            //for testing 

            Dialoug tempRootDialoug = new Dialoug("hmmm should I bring up "+ c.fullName +"?",
                setNodeDialoug(c)); //change this into npc text instead of player buttons 
            fullCharacterRootNodes.Add(tempRootDialoug);
            Tree tempCharacterFullTree = new Tree();
            tempCharacterFullTree.root = tempRootDialoug;
            //to do method that loosp over topics unexplored and populates the tree with children (recursion)
            tempRootDialoug.children = returnConnectedNodes(tempRootDialoug, c);
            allCharacterConversationsTrees.Add(tempCharacterFullTree);// the tree  listy should hold all starting nodes 

            //  Debug.Log("testing" + c.fullName);
            printATree(tempCharacterFullTree);
        }
    }



    List<Dialoug> returnConnectedNodes(Dialoug node, InterestingCharacters character)
    {
        List <Dialoug> connectedChildNodes= new List<Dialoug>();
        List<string> allCharacterFlags = getOtherOpinions(character); // need to do alot of refactoring.... 
        foreach( string key in allCharacterFlags)
        {
            connectedChildNodes.Add(new Dialoug(setNodePlayerClickText(key), setNodeDialoug(character)));

        }

        return connectedChildNodes;
    }

    private string setNodePlayerClickText(string key)
    {
       return translatePlayerOptionIntoText(key);
        //refresh buttons when clicked 
        //make this recursive for children of children in a tree--- 
    }

    void printCurrentNodes()
    {
        foreach(Dialoug c in fullCharacterRootNodes)
        {
            Debug.Log(c.ButtonText);
        }
    }

    private string setNodeDialoug(InterestingCharacters character) //the actual dialoug generated --- 
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
        currentThoughts = getOtherOpinions(character);

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
     // Debug.Log(currentCNPC.ConvCharacterMoralFactors[key]); - fix logic 
        switch (currentCNPC.ConvCharacterMoralFactors[key])
        {
            case ConversationalCharacter.RatingVlaues.High:
                return  returnIntroTopic(highVaueOpinions, key) +
                returnContrastingOpinion(highVaueOpinions, key, character);
            case ConversationalCharacter.RatingVlaues.Mid:
                //Debug.Log("TESTTT:" + returnIntroTopic(midVaueOpinions, key) + returnContrastingOpinion(midVaueOpinions, key, character));
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
                selectedOpnion =op.topic.Split('_').Last();
                return op.NarrativeElements.intro + op.NarrativeElements.
;
            }
        }
        return "NO TOPIC WAS FOUND --- need to author these topics  " + key;
    }
    //TODO add a visted node or pop them, unless want to keep a record of it, need to fix logic fot topics displayed and choosing thee right contrasting opinion 
    //for debugging
    void printATree(Tree t)
    {
        Debug.Log("the root of the tree is "+ t.root);
        foreach(Dialoug d in t.root.children)
        {
            Debug.Log("the root" + t.root.ButtonText + "has childen of button text " + d.ButtonText);
        }
        Debug.Log("this tree has childer" + t.root.children.Count);

    }
    void assembleADialougString()
    {
        string s;
    }

   List<string> getOtherOpinions(InterestingCharacters character)
    {
        List<string> possibleOpinions = new List<string>();
        foreach(KeyValuePair<string, bool> kvp in character.characterFlags)
        {
            if (kvp.Value)
            {
                possibleOpinions.Add(kvp.Key);

            }
         }
        return possibleOpinions;
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
        if(character.characterFlags["WillActOnLove"])
        {
            return "WillActOnLove";
        }
        if (character.characterFlags["polluterRole"] )
        {
            return "polluterRole";
        }
        if (character.characterFlags["butcherRole"] )
        {
            return "butcherRole";
        }
        if (character.characterFlags["familyPerson"] )
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



*/
/*
               if (!bgCharacterNOdes[i].Explored)
               {
                   Debug.Log("we still have more things to say about tghis character;");
                   node = bgCharacterNOdes.ElementAt(i);
                   bgCharacterNOdes[i].Explored = true;
                   currentNode = node;
                   return currentNode;
               }
               else
               {

                   if(i == bgCharacterNOdes.Count)
                   {
                       Debug.Log("we talked about evrything!!");

                       currentTree.FullyExplored = true;

                   }
               }*/
