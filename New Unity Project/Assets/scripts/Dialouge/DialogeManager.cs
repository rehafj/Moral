using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//test

[System.Serializable]
public class DialogeManager : MonoBehaviour //TODO refactor this later, just for testing plopping things here for now 
{
    public Tree testingTree = new Tree(); //--using this one to test one branch out - generate these and place them in the big list of  tree/brance conversations  - using this 

    public List<Tree> allCharacterConversationsTrees = new List<Tree>();
    private Tree currentTree ;

    public List<Dialoug> fullCharacterRootNodes;
    public List<Dialoug> currentPlayerOptions;
    public int treeCounter = 3;

    JsonLoader jsn;

    public List<DialougStructure> AllOpinions; //not done the best way but for rapid testing for now 
    List<DialougStructure> highVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> midVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> lowVaueOpinions = new List<DialougStructure>();



    ConversationalCharacter currentCNPC;

    //add a node to check if explored 
    BackgroundCharacter bgchar;


    List<String> currentThoughts;
    string selectedOpnion;

    public List<InterestingCharacters> conversedAboutCharectersList;



    //ui stuff 
    public string playerName = "playerName";
    public GameObject InstructionsUI;
    public Text instructionsText;

    //this is messy - move UI stuff to its own script! 
    public Text GuicharacterName;
    public Text GuidialougText;
    public Text[] CNPCoptionText;
    public Button[] PlayerButtons;
    [SerializeField] bool isTyping;

    Coroutine currentCorutine;
    Coroutine currentThoughtBubbleCorutine;


    InterestingCharacters currentIntrestingCharracter;
    List<string> currentTopicsAboutCurrentCharacter;

    string startingSceneText = "";


    //for tests 
    Tree TomsTree = new Tree();

    Tree AllCharacterTrees = new Tree();//will hold branches...

    delegate IEnumerator waitAndExecute(List<Tree> tree);
    waitAndExecute MyMethodHolder;

    public void Awake()
    {

    }
    public void Start()
    {
        bgchar = FindObjectOfType<BackgroundCharacter>();
        jsn = FindObjectOfType<JsonLoader>();


        //disableorEnablePlayerButtons();
        AllOpinions = jsn.listOfConversations;
        currentCNPC = FindObjectOfType<CharacterManager>().characters[0]; //hard coded with tim for now 
        OrgnizeCNPCOpinions();
        setUp();


    }


    Dialoug currentNode;
  

    public void setUp()
    {
        Dialoug introductionNode = new Dialoug("introduction", "hey there " + playerName +
        " \n thanks for meeting me for brunch! Boy has the town been eventful lately! "); //move this into its file 

        conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());//fltred list of characters (with 5+ flags)


        currentCorutine = StartCoroutine(TypeInDialoug("hey there " + playerName +
      " \n thanks for meeting me for brunch! Boy has the town been eventful lately! "));

        setUpTrees();

    }


    void setUpTrees()
    {
        foreach (InterestingCharacters character in conversedAboutCharectersList)
        {

            Dialoug node = new Dialoug("thikning about " + character.fullName,
                         "hmm I have been thinking about the cube " + character.fullName);
            Tree tree = new Tree();
            tree.root = node;
            tree.root.children = returnListOfDialougNodes(character); //nodes with flags
            foreach(Dialoug d in tree.root.children)
            {
                d.parent = tree.root;
                d.hatedFacts = getThingsHatedAboutBNPC(d,character);
              
            }
            allCharacterConversationsTrees.Add(tree);

          
        }
        foreach( Tree  t in allCharacterConversationsTrees)
        {


        }

       
        displayThoughts(allCharacterConversationsTrees);
        currentTree = allCharacterConversationsTrees[treeCounter];
       startAconversation(allCharacterConversationsTrees[treeCounter]);//the first cgharacter in the list - yhionking about character....etc 
     
    }

    private List<string> getThingsHatedAboutBNPC(Dialoug d, InterestingCharacters character)
    {
        List<string> hatedFactsAboutThisCharacter = new List<string>();
        List<string> alreadyVistedFlags = new List<string>();
        foreach (KeyValuePair<string, bool> kvp in character.characterFlags)
        {
            if (kvp.Value) //ex:love affairs 
            {
                if (!alreadyVistedFlags.Contains(mapToCNPCMoralFactor(kvp.Key)))
                {
                    if (mapToCNPCMoralFactor(kvp.Key) == "Enviromentalist" || mapToCNPCMoralFactor(kvp.Key) == "AnimalLover" ||
                        mapToCNPCMoralFactor(kvp.Key) == "LandISWhereThehrtIS")
                    { //only these two are kinda the oppasate
                        string fact = returnTopicText(highVaueOpinions, kvp.Key, "CON");
                        hatedFactsAboutThisCharacter.Add(fact);
                    }
                    else
                    {
                        string fact = returnTopicText(lowVaueOpinions, kvp.Key, "CON");
                        hatedFactsAboutThisCharacter.Add(fact);
                    }
                }

             alreadyVistedFlags.Add(mapToCNPCMoralFactor(kvp.Key));

            }
        }
      /*  foreach(string s in hatedFactsAboutThisCharacter)
        {
            Debug.Log("the facts about " + character.fullName + "that our CNPC HATES" + s);
        }*/
        return hatedFactsAboutThisCharacter;

    }
    private void startAconversation(Tree chosenTree)
    {

        currentNode = choseADialougNode(chosenTree.root.children);//i.e we are still in the same tree

            /* currentNode = choseADialougNode(chosenTree.root.children); //new node selection from another tree/branch 
             currentTree = chosenTree;*/

        StartCoroutine(WaitAndPrintcompoundedStatments(currentNode.IntroducingATopicdialoug,
                   currentNode.dialougText));
        DisplayplayCurrentOpinions(currentTree);
        displayPlayerButtons(currentNode);

    }
    Dialoug choseADialougNode(List<Dialoug> bgCharacterNOdes)
    {

        // int r = UnityEngine.Random.Range(0, bgCharacterNOdes.Count - 1);
        int i = 0;
        foreach (Dialoug d in bgCharacterNOdes)
        {
            Debug.Log("list size is " + bgCharacterNOdes.Count) ;

            Debug.Log("value of node" + d.ButtonText + "is "+ d.Explored + "value of counter"+i);
            if (!d.Explored)//element is not explored 
            {
                d.Explored = true;
                return d;
            } i++;
            if (i >= bgCharacterNOdes.Count)//last element in the list 
            {
                Debug.Log("last element of the list");
                currentTree.FullyExplored = true;
                break;  }
        }

     /*   bool fullyexplored = bgCharacterNOdes.All(x => x.Explored == false); */
       
        return currentNode;

    }


    //toDo --- update agree (on topic ) / disagree buttons (on topics) / cxhange topic - see other children in que - have a current tree act6ive = whaty dp you think ahout go to the main list of characters - first 4 elements ,,,   

    void displayThoughts(Tree tree) //used for flags
    {
        int i = 0;
        foreach (Text t in CNPCoptionText)
        {
            t.text = tree.root.children[i].thoughtBubbleText; //grabs first 4
            i++;
        }
    }
    void displayThoughts(List<Tree> tree) //used for general thinking about character 
    {
        int i = 0;
        foreach (Text t in CNPCoptionText)
        {
            t.text = tree[i].root.thoughtBubbleText;
            i++;
        }
    }

    void displayPlayerButtons(Dialoug node) //hardcoded for now - do this for the current height of the tree --- 
    {

        // Debug.Log(node.getHeight());//if the higfght is 2 then i can agree/ dissagree..etc with an option //height 1 then i am thinking aboyut the character ( height 2 is the flag / character topic) 
        // StartCoroutine(waitAndEnableButtons());
        /*        disableorEnablePlayerButtons();
        */
     

        if (node.getHeight() == 2)
        {
            setPlayerButtonText();
            //for testing 
            foreach(Button b in PlayerButtons)
            {
                b.onClick.RemoveAllListeners();
            }
            PlayerButtons[0].onClick.AddListener(playerAgrees);
            PlayerButtons[1].onClick.AddListener(playerDissAgrees);
            PlayerButtons[2].onClick.AddListener(askAboutAnotherCharacter);

        }
    }



    void playerAgrees()
    {
        Debug.Log("player agreed!!!" + currentNode.ButtonText + "after i click on agree! " + currentNode.dialougText);
        StartCoroutine(waitAndPrintAgreement(currentNode.agreementText));
    }

    private void playerDissAgrees()
    {
        Debug.Log("player disagreed!" + currentNode.ButtonText + "after i click on agree! " + currentNode.dialougText);

        StartCoroutine(waitAndPrintDisagreement(currentNode.getRandomHatedFact()));
        setPlayerButtonTextDissagreement();
    }

    private void askAboutAnotherCharacter()
    {
        currentNode = currentNode.parent; //went up on height --- 
        Debug.Log("checking:"+ currentNode.getHeight());
        if (currentNode.getHeight() == 1)
        {
            setPlayerButtonTextCharacter();
            askAboutAcharacter();
        }

    }
  
    //refactor these --- 
    private void setPlayerButtonText()
    {
        PlayerButtons[0].GetComponentInChildren<Text>().text = "I agree with " + currentNode.ButtonText;
        PlayerButtons[1].GetComponentInChildren<Text>().text = "I don't agree with you there";
        PlayerButtons[3].GetComponentInChildren<Text>().text = "Dish more about that cube"; //pull from a list of random strings later //TODO
        PlayerButtons[2].GetComponentInChildren<Text>().text = "you know what, lets talk about something else";
    }
    private void setPlayerButtonTextDissagreement()
    {
        PlayerButtons[0].GetComponentInChildren<Text>().text = "yeah I guess you are right";
        PlayerButtons[1].GetComponentInChildren<Text>().text = "I still don't agree with you there";
        PlayerButtons[3].GetComponentInChildren<Text>().text = "Dish more about that cube"; //pull from a list of random strings later //TODO
        PlayerButtons[2].GetComponentInChildren<Text>().text = "you know what, lets talk about something else";
    }

    private void setPlayerButtonTextCharacter()
    {
        int i = treeCounter;
        foreach(Button b in PlayerButtons)
        {
            Debug.Log(allCharacterConversationsTrees[i].root.thoughtBubbleText);
            b.GetComponentInChildren<Text>().text = "ask about" +
                allCharacterConversationsTrees[i].root.thoughtBubbleText;
                i++;
        }
        
    }

    private void askAboutAcharacter()
    {
        int i = treeCounter;
        foreach (Button b in PlayerButtons)
        {
            b.onClick.RemoveAllListeners();
        }
        foreach (Button b in PlayerButtons)
        {
            Debug.Log(i);

            b.onClick.AddListener(() => moveConversationToSelectedTree(i) );
            i++;
        }

    }

    private void moveConversationToSelectedTree(int treeIndex)
    {
        //fix this --- it is always 4 as an index! 
        Debug.Log("index of the tree" + treeIndex);
        currentTree = allCharacterConversationsTrees[treeIndex];
        StopAllCoroutines();
        GuidialougText.text = "";
        Debug.Log("current tree explorting " + currentTree.root.thoughtBubbleText);
        startAconversation(currentTree);
    }




    void converseAboutNextCharacter()
    {
        Debug.Log("this is called!");
        treeCounter++;
        startAconversation(allCharacterConversationsTrees[treeCounter]);//the first cgharacter in the list - yhionking about character....etc 

    }


    Dialoug choseACharacterTree()
    {
      //  int i = UnityEngine.Random.Range(0, allCharacterConversationsTrees.Count - 1); //do i want it to be random or just the next tree in the list cz they are sorted???
        
        if (!allCharacterConversationsTrees[treeCounter].FullyExplored)//currentTree.FullyExplored)
        {
            Debug.Log("you still hjavbe motr tyo do in this tree; --- can visit it later ");
         
        }
        else //we fully explored the tree 
        {
            treeCounter++;
            startAconversation(currentTree);
            //go to another character. 
        }

        // bgCharacterNOdes.RemoveAt(i);//que and deque from a list ---OR FLAG VISITED add logic on how to choose a better character - random for now 
        return currentNode;

    }

    public void Update()
    {
    /*    Debug.Log("INSIDE UPDATE --- CURNODE currentNode.ButtonText  " +currentNode.ButtonText + "  topic introduction: " + currentNode.IntroducingATopicdialoug
            +"intro baised<dtext>"+ currentNode.dialougText +"a node's agreement"+currentNode.agreementText +"nodes dissagreement text"+ 
            currentNode.hatedFacts[0]+"fully explored this node"+currentNode.Explored);
        Debug.Log(treeCounter);
*/
    }

    private void displayDialougOpinion()
    {
        StopAllCoroutines();
        StartCoroutine(TypeInDialoug(currentNode.dialougText));
    }

    private List<InterestingCharacters> sortCharactersToBringUp(List<InterestingCharacters> characterList)
    {
        InterestingCharacters chosenCharacter;
        List<InterestingCharacters> sortedList = new List<InterestingCharacters>();

        List<InterestingCharacters> priority = new List<InterestingCharacters>();
        List<InterestingCharacters> secondary = new List<InterestingCharacters>();
        List<InterestingCharacters> therest = new List<InterestingCharacters>();


        for (int i = 0; i < characterList.Count - 1; i++)
        {
            chosenCharacter = characterList[i];

            if (characterList[i].HasJuicyMoralFacts())
            {
                characterList.RemoveAt(i); priority.Add(chosenCharacter);
            }
            else if (characterList[i].NumberOFFlags() > 5)
            {
                characterList.RemoveAt(i); secondary.Add(chosenCharacter);
            }
            else
            {
                /*  int r = UnityEngine.Random.Range(0, characterList.Count - 1);
                  chosenCharacter = characterList.ElementAt(r);*/
                characterList.RemoveAt(i); therest.Add(chosenCharacter);

            }

        }
        sortedList.AddRange(priority);
        sortedList.AddRange(secondary);
        sortedList.AddRange(therest);

        //perhaps make some of those random.. 

        return sortedList;



    }

    private List<Dialoug> returnListOfDialougNodes(InterestingCharacters character) //change name later 
    {
        List<Dialoug> nodes = new List<Dialoug>();
        foreach (KeyValuePair<string, bool> kvp in character.characterFlags)
        {
            if (kvp.Value)
            {
                Dialoug node = new Dialoug(
                   translateOpinionIntoText(kvp.Key),
                   getInitialCNPCOpinionAsDialoug(kvp.Key, character, "INTRO"));//general feelings about a topic 
                //--- gives the general feelings about a thing, add to the same node the pther structure elements... 

                node.IntroducingATopicdialoug =
                    getIntroductionTopicString(kvp.Key, character); //unbaised opening statment 
              
                node.agreementText = getInitialCNPCOpinionAsDialoug(kvp.Key, character, "AGREE");//general feelings about a topic 

                node.ButtonText = setPlayerButtonText(kvp.Key);
                nodes.Add(node);

            }
        }
        return nodes;

        //int i = UnityEngine.Random.Range(0, character.characterFlags.Count);

    }

    private string setPlayerButtonText(string key)
    {
        //will change depeninding on input for now its set to just the key 
        return key ; //will add logic about current cnpc thoughts and modding this later 
    }

    //2
    string returnTopicText(List<DialougStructure> opinions, string key, string flag) //the sent in list is of high./mid or low 
    {//selecting intro and the first part of the body here --- 
        foreach (DialougStructure op in opinions) //ex: all high opp
        {
            if (op.topic.Contains(mapToCNPCMoralFactor(key))) //get the translatiopn of they key but not ditect character keys..... 
            {
                selectedOpnion = op.topic.Split('_').Last();
                //Debug.Log("-------selectedOpnion" + selectedOpnion);
                if (flag == "INTRO")
                {
                    return op.NarrativeElements.intro;

                }
                if (flag == "AGREE")
                {
                    return op.NarrativeElements.bodyOne;
                }
                if (flag == "CON")
                {
                    return op.NarrativeElements.bodytwo;

                }

            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;
    }


    //1
    private string getInitialCNPCOpinionAsDialoug(string key,
        InterestingCharacters character, string flag)
    {//this is hard coded for now but later send in the conversational character (talking with )
    /*    Debug.Log("currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)] issss"
            + mapToCNPCMoralFactor(key));*/
        switch (currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)])
        {
            case ConversationalCharacter.RatingVlaues.High:
                return returnTopicText(highVaueOpinions, key, flag); //make this more generic - iof/else get topic or body (startingopinion) or contrasting one... nut need logic on each case i think of this is testign a thing for now 
            case ConversationalCharacter.RatingVlaues.Mid:
                return returnTopicText(midVaueOpinions, key, flag);
            default://low
                return returnTopicText(lowVaueOpinions, key, flag);
        }
    }


 

    private string ReturnAgreement(List<DialougStructure> highVaueOpinions, string key)
    {
        throw new NotImplementedException();
    }

    private string mapToCNPCMoralFactor(string key)
    {
        switch (key)
        {
            case ("departed"):
                return "LandISWhereThehrtIS";

            case ("familyPerson"):
                return "FamilyPerson";

            case ("InLovewithspouseoffriend"):
            case ("pregnantbutnotengaged"):
            case ("pregnantbutnotfromspuceorbutloveintrest"):
            case ("InLoveWirhAnothersspuce"):
            case ("WillActOnLove"):
                return "BTrueTYourHeart";

            case ("socialLife"):
            case "FriendsAreTheJoyOFlife":
            case ("friendwithabestfriendsenemy"):
            case ("hasAbestFriend"):
                return "FriendsAreTheJoyOFlife";

            case ("loner"):
                return "Loner";

            case ("IsWealthy"):
            case ("IsRichButNotGenrous"):
            case ("flipflop"):
                return "CarrerAboveAll";//add another klind of value here 

            case ("WorksInAlcohol"):
            case "Teetotasler":
                return "Teetotasler";
            case ("healerRole"):
            case ("CustodianJobs"):
                return "CarrerAboveAll";//change this into supporitng roles...
            case ("Teachingrole"):
                return "SchoolIsCool";

            case ("polluterRole"):
            case ("Enviromentalist"):
                return "Enviromentalist";
            case ("riskTaker"):
            case ("LoverOfRisks"):
                return "LoverOfRisks";
            case ("advancedCareer"):
            case ("hardWorker"):
            case "CarrerAboveAll":
                return "CarrerAboveAll";
            case ("MoneyMaker"):
                return "SupportingComunities";//change this
            case ("SchoolIsCool"):
            case ("butcherRole"):
                return "AnimalLover";
            case ("notworkingandrich"):
            case ("adultbutnotworking"):
            case ("widowedbutnotgrieving"):
            case ("exploteative"):
            case ("graduate"):
            case ("hasalotofenemies"):
            case ("generalJobs"):
                return "Loner";////these do not have anything tied to em, need to update this
            default:
                return "missed a tag SOMEWHERE!" + key;

        }
    }


    // helper functions 
    private void OrgnizeCNPCOpinions()
    {

        foreach (DialougStructure op in AllOpinions)
        {
            if (op.topic.Contains("High"))
            {
                highVaueOpinions.Add(op);
            }
            if (op.topic.Contains("Mid"))
            {
                midVaueOpinions.Add(op);
            }
            if (op.topic.Contains("Low"))
            {
                lowVaueOpinions.Add(op);
            }
        }
    }


    //gui stuff 
    private void DisplayplayCurrentOpinions(Tree currentCharacterTree) //text fields
    {
        foreach (Text t in CNPCoptionText) //send in flags to check if player or npc and get the rioght translation out
        {
            int r = UnityEngine.Random.Range(0, currentCharacterTree.root.children.Count - 1);
            t.text = currentCharacterTree.root.children[r].thoughtBubbleText;
        }
    }

    string getIntroductionTopicString(string key, InterestingCharacters character)
    {
        switch (key)
        {
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "guess what I heard! " + character.fullName + " left town!, ";
            case ("familyPerson"):
                return "hmm, did you know that  " + character.fullName + " has a "
                    + "family"; //to do add mcond stat for lar/small ,,,etc family 

            //add more values for the ones below --- seprate core values perhaps?mainly tags are used for flavor text but they fall for one core value 
            case ("InLovewithspouseoffriend"):
                return "I heard that " + character.fullName + " is in love with thier spouce's friend! ";

            case ("pregnantbutnotfromspuceorbutloveintrest"):
                return "I heard that " + character.fullName + "cheated on their siginificant cube with " + character.GetLoverName();
            case ("InLoveWirhAnothersspuce"):
                return "Not juding but I heard that " + character.fullName + "IS IN LOVE WITH ANOTHER CUBE'S spouse ";
            case ("WillActOnLove"):
                return "Not juding but I heard that " + character.fullName + "IS IN LOVE WITH ANOTHER CUBE that is not their spouce, a birdy told me they will act on it ><";

            case ("BTrueTYourHeart"):
            case ("pregnantbutnotengaged"):
                return "you know," +
                    " I think people in this town might be too much into love afairs, " +
                    "you would think we were in a dating sim of some kind..."; //TODO write specfic texts for scenarios 


            case ("socialLife"):
            case "FriendsAreTheJoyOFlife":
                return "oh oh " + playerName + "how is  " + character.fullName + "so popular!\n they sure have a lot of friends!"; // or they have a lot of fgriends 

            case ("friendwithabestfriendsenemy"):
                return "you know what is weird? " + playerName + "? \n   " + character.fullName + " is friends with thier friend's enemy :0"; // or they have a lot of fgriends 

            case ("hasAbestFriend"):
                return character.fullName + "'s best friend sure loves them! must be nice to have a best friend"; // or they have a lot of fgriends 

            case ("loner"):
                return "I wonder why " + character.fullName + "lives alone...";
            case ("hasalotofenemies"):
                return "I wonder why " + character.fullName + "has alot of enemies... ///assigned to firinship is the joy of life";


            case ("MoneyMaker"):
            case ("IsWealthy"):
                return " the " + character.fullName +
                    "are wealthy apparently, like super wealthy.";
            case ("IsRichButNotGenrous"):
                return "the " + character.fullName +
                    "are wealthy apparently, like super wealthy... but also so not the giving type!";


            case ("WorksInAlcohol"):
            case "Teetotasler":
                return character.fullName + "wokrs in alchohol, wonder what that field is like";

            case ("flipflop"):
                return "they are so indecisive when it comes to their career! ";

            case ("healerRole"):
            case ("CustodianJobs"):
            case ("SupportingComunities"):


            case ("polluterRole"):
            case ("Enviromentalist"):
                //Debug.Log("wooooooow "+ key);
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation;


            case ("riskTaker"):
            case ("LoverOfRisks"):

            case ("generalJobs"):


            case ("advancedCareer"):
            case ("hardWorker"):
            case "CarrerAboveAll":
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n moreso, I beliave that cube works so many hours! I htink they are a hardworking cube. ";

            case ("Teachingrole"):
            case ("SchoolIsCool"):

            case ("butcherRole"):
            case ("AnimalLover"):
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation +"in a farm";

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
            case ("widowedbutnotgrieving"):
            case ("exploteative"):


            case ("graduate"):
                return "NOTAUTHORED";
            default:
                return "missed a tag SOMEWHERE!" + key + "was not authored";


        }
    }

    //if occupation - then dialoug button - what baout where they work --- need to find some statments like the npc one but for players 
    //for the NPC --- 

    string translateOpinionIntoText(string key)
    {
        switch (key)
        {
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "they left town...";
            case ("familyPerson"):
                return "..have a family..";
            case ("InLovewithspouseoffriend"):
            case ("BTrueTYourHeart"):
            case ("pregnantbutnotengaged"):
            case ("pregnantbutnotfromspuceorbutloveintrest"):
            case ("InLoveWirhAnothersspuce"):
            case ("WillActOnLove"):
                return "matters of the heart...";
            case ("socialLife"):
            case "FriendsAreTheJoyOFlife":
            case ("friendwithabestfriendsenemy"):
            case ("hasAbestFriend"):
                return ".. they sure are popular.."; // or they have a lot of fgriends 
            case ("loner"):
                return "...live alone...";

            case ("IsWealthy"):
                return "wonder how rich ...";
            case ("IsRichButNotGenrous"):
                return "..selfish person... ";
            case ("flipflop"):
                return "they are so indecisive";
            case ("WorksInAlcohol"):
            case "Teetotasler":
                return "they work with alcohol";

            case ("healerRole"):
            case ("CustodianJobs"):
            case ("Teachingrole"):
                return "they help people...";
            case ("polluterRole"):
            case ("Enviromentalist"):
                return "not environmental friendly...";
            case ("riskTaker"):
            case ("LoverOfRisks"):
                return "tough job..";
            case ("advancedCareer"):
            case ("hardWorker"):
            case "CarrerAboveAll":
            case ("MoneyMaker"):
                return "..hard worker..";
            case ("SchoolIsCool"):
                return "they have a degree";

            case ("butcherRole"):
            case ("AnimalLover"):
                return "..animal rights";

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
                return key + "NOT AUTHORED";
            case ("widowedbutnotgrieving"):
                return "wonder why they are not grieving...";
            case ("exploteative"):
            case ("graduate"):
                return key + "NOT AUTHORED YET";
            case ("hasalotofenemies"):
                return "...they sure can't make friends";
            case ("generalJobs"):
            case ("SupportingComunities"):

                return "what did they work in again...";


            default:
                return "missed a tag!" + key;

        }
    }


    //for the player 
    string translatePlayerOptionIntoText(string key)
    {
        switch (key)
        {
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "I heard they left town, I wonder if  that is true?";
            case ("familyPerson"):
                return "don't they have a family";
            case ("InLovewithspouseoffriend"):
            case ("BTrueTYourHeart"):
            case ("pregnantbutnotengaged"):
            case ("pregnantbutnotfromspuceorbutloveintrest"):
            case ("InLoveWirhAnothersspuce"):
            case ("WillActOnLove"):
                return "what do you think of their love affair";
            case ("socialLife"):
            case "FriendsAreTheJoyOFlife":
            case ("friendwithabestfriendsenemy"):
            case ("hasAbestFriend"):
                return "--- question about sdocial life "; // or they have a lot of fgriends 
            case ("loner"):
                return "q about the being alone";

            case ("IsWealthy"):
                return " are they rich";
            case ("IsRichButNotGenrous"):
                return "are they selfsihh";
            case ("flipflop"):
                return "are they indecisive";
            case ("WorksInAlcohol"):
            case "Teetotasler":
                return "what do you oknow of their job?";

            case ("healerRole"):
            case ("CustodianJobs"):
            case ("Teachingrole"):
                return "what do you oknow of their job?";
            case ("polluterRole"):
            case ("Enviromentalist"):
                return "what do you oknow of their job?";
            case ("riskTaker"):
            case ("LoverOfRisks"):
                return "what do you oknow of their job?";
            case ("advancedCareer"):
            case ("hardWorker"):
            case "CarrerAboveAll":
            case ("MoneyMaker"):
                return "what do you know about thei work ethics?";
            case ("SchoolIsCool"):
                return "what do you know about _school";

            case ("butcherRole"):
            case ("AnimalLover"):
                return "animal question ";

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
            case ("widowedbutnotgrieving"):
            case ("exploteative"):
            case ("graduate"):
            case ("hasalotofenemies"):
            case ("generalJobs"):
            case ("SupportingComunities"):

                return "this case is not yet authored";


            default:
                return "missed a tag!" + key;

        }
    }



    //UI stuff

    public void setPlayerName(Text text)
    {
        playerName = text.text;
        instructionsText.text = "thanks " + playerName + "!";
        Invoke("disableInstructionsUI", 2);
        //InstructionsUI.SetActive(false);
    }

    void disableInstructionsUI()
    {
        InstructionsUI.SetActive(false);
        startTheInteraction();
    }

    private void startTheInteraction()
    {
        setUp();
    }

    void disableorEnablePlayerButtons()
    {

        foreach (Button b in PlayerButtons)
        {
            if (b.IsActive()) //if the button is active deactivate it
            {
                b.gameObject.SetActive(false);
            }
            else
            {
                b.gameObject.SetActive(true);
            }
        }
    }
    void enableClick()
    {
        foreach (Button b in PlayerButtons)
        {
            if (isTyping) //if the button is active deactivate it
            {
                b.interactable = false;

            }
            else
            {
                b.interactable = true;
            }
        }
    }

    [SerializeField] bool checkCorutine;
    bool isItTypying()
    {
        return checkCorutine;
    }
    IEnumerator TypeInDialoug(string dialkougText)
    {
        checkCorutine = false;
        for (int i = 0; i <= dialkougText.Length; i++)
        {
            isTyping = true;
            enableClick();
            GuidialougText.text = dialkougText.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        enableClick();
       // Debug.Log("i am ptring this in the loop");
        yield return new WaitForSeconds(2);
        //Debug.Log("printging this after two seconds...");

        checkCorutine = true;
    }

    IEnumerator waitAndEnableButtons()
    {
        yield return currentCorutine;
        Debug.Log("does this fucking print?");
        disableorEnablePlayerButtons();
    }

    IEnumerator waitAndPrint(Dialoug node)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(node.dialougText));

    }
    IEnumerator waitAndPrintAgreement(string text)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(text));
        yield return currentCorutine;
        if (!currentTree.FullyExplored)
        {
            startAconversation(currentTree);//at 0 
        }
        else
        { 
            Debug.Log("Done with the tree");
            converseAboutNextCharacter();
        }

    }

    IEnumerator waitAndPrintDisagreement(string text)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(text));
        yield return currentCorutine;
        if(currentNode.hatedFacts.Count <= 0 && currentTree.FullyExplored)
        {
            //i.e. we walked about all the hated facts! 
            converseAboutNextCharacter();
        }
        else
        {
            startAconversation(currentTree);
        }
    

    }

    IEnumerator WaitAndPrintcompoundedStatments(string textOne, string textTwo)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(textOne));
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(textTwo));
    }
    IEnumerator WaitAndPrintcompoundedStatments(Dialoug node1, Dialoug node2)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(node1.dialougText));
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(node2.dialougText));
    }
    Coroutine DisplayNodeIntroTopic(Dialoug d)
    {
        Coroutine c = StartCoroutine(TypeInDialoug(d.IntroducingATopicdialoug));
        return c;

    }

    IEnumerator waitAndTest()
    {
        yield return new WaitUntil(() => isTyping == true);

    }





 /*   private InterestingCharacters chooseaCharacterToTalkAbout()
    {
       



    }*/

}



public class Tree
{
    public Dialoug root { get; set; }
    public bool FullyExplored = false;
}

/*   private InterestingCharacters chooseaCharacterToTalkAbout()
    {
        InterestingCharacters chosenCharacter; 

        for(int i = 0; i < conversedAboutCharectersList.Count ; i++)
        {
            if (conversedAboutCharectersList[i].HasJuicyMoralFacts())
            {
                Debug.Log("found jucy characters ! ");
                chosenCharacter = conversedAboutCharectersList[i];
                conversedAboutCharectersList.RemoveAt(i);
                return chosenCharacter;
                
            }
            if (conversedAboutCharectersList[i].NumberOFFlags()> 5)
            {
                Debug.Log("found alot of things to talk about ");

                chosenCharacter = conversedAboutCharectersList[i];
                conversedAboutCharectersList.RemoveAt(i);
                return chosenCharacter;
            }
            else
            {
                Debug.Log("found normies");

                int r = UnityEngine.Random.Range(0, conversedAboutCharectersList.Count - 1);
                //TODO add logic to choose most intresting characrer

                 chosenCharacter = conversedAboutCharectersList.ElementAt(r);
                conversedAboutCharectersList.RemoveAt(r);//que and deque from a list --- add logic on how to choose a better character - random for now 
            }

        }
        return chosenCharacter;



    }
*/



//random for now but pops it out of the lkist if chose


//testing case: 
/*      currentIntrestingCharracter = chooseaCharacterToTalkAbout();
      Dialoug node = new Dialoug("thikning about " + currentIntrestingCharracter.fullName,
          "hmm I have been thinking about the cube " + currentIntrestingCharracter.fullName); //base node of a character. 
      TomsTree.root = node;
      TomsTree.root.children = returnListOfDialougNodes(currentIntrestingCharracter);
      allCharacterConversationsTrees.Add(TomsTree);
      DisplayplayCurrentOpinions(TomsTree);
      currentNode = choseADialougNode(TomsTree.root.children);
      currentCorutine = StartCoroutine(TypeInDialoug("hey there " + playerName +
    " \n thanks for meeting me for brunch! Boy has the town been eventful lately! "));

      StartCoroutine(WaitAndPrintcompoundedStatments(currentNode.IntroducingATopicdialoug, currentNode.dialougText));//so this works... but does nto when in larger scenartio.. 
         //// -- end of testing case 
*/

//DisplayThoughtBubbles();
