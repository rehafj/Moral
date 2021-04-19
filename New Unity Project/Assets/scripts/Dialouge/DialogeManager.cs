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


    public int x = 9;
    

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



    ConversationalCharacter currentCNPC; //change this depending on time and instantations --- also control the character 

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

    //test

    Dialoug currentNode;

    int moralCounter = 0;

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
        moralCounter = 0; //reset it for next character
        currentNode = choseADialougNode(chosenTree.root.children);//i.e we are still in the same tree

            /* currentNode = choseADialougNode(chosenTree.root.children); //new node selection from another tree/branch 
             currentTree = chosenTree;*/

        StartCoroutine(WaitAndPrintcompoundedStatments(currentNode.IntroducingATopicdialoug,
                   currentNode.dialougText));
        DisplayplayCurrentOpinions(currentTree);
        displayPlayerButtons(currentNode);

    }

    private void cNPCHoldingStance() //does not transfer control 
    {

        StartCoroutine(WaitAndPrintcompoundedStatments("surface valuef for this flag"+currentNode.agreementText,
                "presist on importance of moral flag "));
        DisplayplayCurrentOpinions(currentTree);
        displayPlayerButtons(currentNode);
        moralCounter += 1;
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
            PlayerButtons[2].onClick.AddListener(playerArgueAboutFLag);
            PlayerButtons[3].onClick.AddListener(askAboutAnotherCharacter);


        }
    }


    void playerAgrees()
    {
        //Debug.Log("player agreed!!!" + currentNode.ButtonText + "after i click on agree! " + currentNode.dialougText);
        Debug.Log("agreement text is =" + currentNode.agreementText);
        StartCoroutine(waitAndPrintAgreement(currentNode.agreementText));
    }

    private void playerDissAgrees()
    {
        //Debug.Log("player disagreed!" + currentNode.ButtonText + "after i click on agree! " + currentNode.dialougText);
        //TODO  Add methods for arguing on a flag  --- 

        StartCoroutine(waitAndPrintDisagreement(currentNode.GetAFact())); //currentNode.getRandomHatedFact())
        playerArgueAboutFLag();




        //setPlayerButtonTextDissagreement();//moves to the next node - --- STOP ITT FGGROM HOINH TO THE NEXT ONE 
    }

    Dialoug currentCNPCStance = new Dialoug("","");
    public void playerArgueAboutFLag()
    {
        Debug.Log("current stanc eis "+ currentNode.ButtonText);
        currentCNPCStance = currentNode;
        //present all flags for player as a sub menue 
        if(currentNode.getHeight() == 2) // change this into try /catch statments 
        {

            //FOR TESTING 
            for (int i = 0; i < PlayerButtons.Length; i++) //OTHER FLAGS
            {
                PlayerButtons[i].GetComponentInChildren<Text>().text = "argue for flag ->" + currentNode.parent.children[i].ButtonText;
                if (currentNode.parent.children[i] == null)
                {
                    PlayerButtons[i].GetComponentInChildren<Text>().text = "no more flags - add logic to hide this option ";
                }
            }
            argueForAflag();
        }

        else
        {
            Debug.LogError("something wonky happned - wrong height of tree ");
        }

    }

   
    
    private void askAboutAnotherCharacter()
    {
        currentNode = currentNode.parent; //went up on height --- 
        Debug.Log("checking:"+ currentNode.getHeight());
        if (currentNode.getHeight() == 1)
        {
            setPlayerButtonToAskAboutOtherCharacters();
            askAboutAcharacter();
        }
        else
        {
            Debug.LogError("you are on the wrong height of the tree !");
        }

    }
  
    //refactor these --- 
    private void setPlayerButtonText()
    {
        PlayerButtons[0].GetComponentInChildren<Text>().text = "I agree with " + currentNode.ButtonText;
        PlayerButtons[1].GetComponentInChildren<Text>().text = "I don't agree with you there";
        PlayerButtons[2].GetComponentInChildren<Text>().text = "you know I heard other things about that cube"; //pull from a list of random strings later //TODO
        PlayerButtons[3].GetComponentInChildren<Text>().text = "you know what, lets talk about something else";
    }
    private void setPlayerButtonTextDissagreement() //change this 
    {
        PlayerButtons[0].GetComponentInChildren<Text>().text = "yeah I guess you are right";
        PlayerButtons[1].GetComponentInChildren<Text>().text = "I still don't agree with you there";
        PlayerButtons[2].GetComponentInChildren<Text>().text = "Dish more about that cube"; //pull from a list of random strings later //TODO
        PlayerButtons[3].GetComponentInChildren<Text>().text = "you know what, lets talk about something else";
    }

    private void setPlayerButtonToAskAboutOtherCharacters()
    {
        int i = treeCounter;
        foreach(Button b in PlayerButtons)
        {
            Debug.Log("denugging thoughy buybblrd" + allCharacterConversationsTrees[i].root.thoughtBubbleText);
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

    private  void argueForAflag( ) //refactor this later --- just takes in thee last item clicked 
    {
        int i = 0;
        foreach (Button b in PlayerButtons)
        {
            b.onClick.RemoveAllListeners();
        }
        foreach (Button b in PlayerButtons)
        {
            Dialoug d = currentNode.parent.children[i];
           // Debug.Log(currentNode.parent.children[i].ButtonText);
            b.onClick.AddListener(() => moveConversationToAflag(d));
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

    private void moveConversationToAflag(Dialoug d ) { 
        StopAllCoroutines();
        GuidialougText.text = "";
        Debug.Log("!!!" +d.ButtonText);


        if (currentCNPC.IsMoralFocus(mapToCNPCMoralFactor(d.ButtonText)))//the flag the player presented is the moral focus of the NPC 
        { 
            Debug.Log("!!!" + d.ButtonText + "is a moral focus area");
            StartCoroutine(waitAndPrintAgreement("CNPC conceedes, player selected flag was moral focus of cnpc-- need to add text or pull from a list based on the flag "));
            
        }
        else //HAPPENS WHEN PPLAYER DOES NOT AGREE WITH NNPC
        {
            //---
            Debug.Log(d.ButtonText);
            Debug.Log(" returned flag ---  " +currentCNPC.FatherModel.ReturnSchema(d.ButtonText, d));
            Debug.Log("what is this value ? "+currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(currentCNPCStance.ButtonText)] +"on"+currentCNPCStance.ButtonText);

            string schema = currentCNPC.FatherModel.ReturnSchema(d.ButtonText, d);
            if (schema== "noSchemasFound")
            {

                // YOU WANT THE NPC TO ARGUE FOR GENERAL CURRENT FLAG ( AGREEMENT TEXT ) + THEN ADD A METHOD TO CHECK IF HIGH MID AND LOW AND CONSULT SCHEMAS.

                StartCoroutine(WaitAndPrintcompoundedStatments("","CNPC will present a fact for surface value As this does not really map to model well"));
       
            }
            else
            {
                Debug.Log("found a schema -huzzah, values are " + currentCNPC.FatherModel.CurrentArgument.pattern + "with" +
                    currentCNPC.FatherModel.CurrentArgument.matchingPattern + "arte they a model cit" + currentCNPC.FatherModel.CurrentArgument.modelCitizen
                    + "schema" + schema);


                StartCoroutine(WaitAndPrintcompoundedStatments(currentCNPC.FatherModel.CurrentArgument.expandedArgument, ""));
                /*      Debug.Log("will argue for surface value for inital  ");
                      StartCoroutine(WaitAndPrintcompoundedStatments("will argue for surface value ", d.agreementText));//as a start but same as disagreement ? 
      */
            }


        }

        //it is currently getting only ther last element - something is wrong with my logioc here ----TODO FROM HERE 
        //need to clear buttons and texts here - present argument and then movie on 
        //add logic to check if argues about flag is in conflict with npoc current node befort changing it 
        //add logic to check if pragmatic or .... 
        //add logic to check for moral focus arae 
        //set the currney node! 
        //  Debug.Log("kicled on flag " + d.ButtonText+ "+to fdouble check for the parent "+ d.parent.dialougText); //need to later move back the conversdation or resume from here(i.e. reset the buttions)
    }


    void converseAboutNextCharacter()
    {
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

        // Debug.Log(currentNode.ButtonText);
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
        Debug.Log(priority.Count);
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
                    getIntroductionTopicString(kvp.Key, character); //unbaised opening statment  --- this is actually yr inytoduction text 
              
                node.agreementText = getInitialCNPCOpinionAsDialoug(kvp.Key, character);//general feelings about a topic  --- this is actually high 

                node.disagreementText = getInitialCNPCOpinionAsDialoug(kvp.Key, character);//nope - con here is as in low.... 
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
        foreach (DialougStructure op in opinions) //ex: all high opp -- splits them up here as high mid and low 
        {
            if (op.topic.Contains(mapToCNPCMoralFactor(key))) //get the translatiopn of they key but not ditect character keys..... 
            {
                selectedOpnion = op.topic.Split('_').Last(); //surface value is returned here = - 
                
                //Debug.Log("-------selectedOpnion" + selectedOpnion);
                if (flag == "INTRO")
                {
                    return op.NarrativeElements.surfaceOpinionOnTopic;

                }
                if (flag == "AGREE")
                {
                    return op.NarrativeElements.agreementText;
                }
                if (flag == "CON")
                {
                    return op.NarrativeElements.disagreementtext;

                }

            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;
    }


    //1
    private string getInitialCNPCOpinionAsDialoug(string key, InterestingCharacters character, string flag)
    {//this is hard coded for now but later send in the conversational character (talking with )
       /* Debug.Log("currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)] issss"
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
            case ("startedAfamilyAtAyoungAge"):
                if (UnityEngine.Random.Range(0, 6) >= 3)
                {
                    return "BTrueTYourHeart";
                }
                else
                {
                    return "LoveIsForFools";
                }
            case ("MovesAlot"):
                return "LandISWhereThehrtIS";
            case ("SusMovments"):
                return "NiaeveteIsFiction";
            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication"):
                return "AselfMadeShapeWeAspireToBe";
            case ("departed"):
                return "LandISWhereThehrtIS";

            case ("familyPerson"):
                return "FamilyPerson";

            case ("InLovewithspouseoffriend"):
            case ("likesToDate"):
            case ("leftFotLoveIntrest"):
            case ("InLoveWirhAnothersspuce"):
            case ("WillActOnLove"):
                return "BTrueTYourHeart";

            case ("socialLife"):
            case "FriendsAreTheJoyOFlife":
            case ("friendwithabestfriendsenemy"):
            case ("hasAbestFriend"):
                return "FriendsAreTheJoyOFlife";

            case ("loner"):
            case ("Loner"):
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

            case ("worksWithFamily"):
            case ("hiredByAFamilymember"):
            case ("getsFiredAlot"):
            case ("RetiredYoung"):
            case ("DiedBeforeRetired"):
            case ("DevorcedManyPeople"):

            case ("marriedForLifeStyleNotLove"):
            case ("AdventureSeeker"):
            case ("liklyToHelpTheHomeless"):
            case ("isolated"):
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
            case ("TooTrustingOfEnemies"):
            case ("ArtSeller"):
          
            case ("likedToExperinceCulture"):
            case ("doesNotGiveToThoseInNeed"):
            case ("supportsImmigration"):
            case ("conventional"):
            case ("reserved"):
         //   case ("selfMadeCubeByDedication"):

                return "TBD";
            default:
                return "unknown";
                //return "missed a tag SOMEWHERE!" + key;

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
        //currentCNPC.ConvCharacterMoralFactors get the opinion t
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

            case ("leftFotLoveIntrest"):
                return "I heard that " + character.fullName + "cheated on their siginificant cube with " + character.GetLoverName();
            case ("InLoveWirhAnothersspuce"):
                return "Not juding but I heard that " + character.fullName + "IS IN LOVE WITH ANOTHER CUBE'S spouse ";
            case ("WillActOnLove"):
                return "Not juding but I heard that " + character.fullName + "IS IN LOVE WITH ANOTHER CUBE that is not their spouce, a birdy told me they will act on it ><";

            case ("BTrueTYourHeart"):
            case ("likesToDate"):
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
            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication"):
                return " rumor says " + character.fullName + "came from nothing, they actually managed to advance in their carreers by dedication alone";

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

            case ("MovesAlot"):
                return "apparently this shape never settles down, they sure moved alot ";
            case ("SusMovments"):
                return "shapes whisper that  " + character.fullName + " is quite odd";

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
            case ("widowedbutnotgrieving"):
            case ("exploteative"):




            case ("graduate"):
            case ("worksWithFamily"):
            case ("hiredByAFamilymember"):
            case ("getsFiredAlot"):
            case ("RetiredYoung"):
            case ("DiedBeforeRetired"):
            case ("DevorcedManyPeople"):
            case ("marriedForLifeStyleNotLove"):
            case ("AdventureSeeker"):
            case ("liklyToHelpTheHomeless"):
            case ("isolated"):
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
            case ("TooTrustingOfEnemies"):
            case ("ArtSeller"):
            case ("likedToExperinceCulture"):
            case ("doesNotGiveToThoseInNeed"):
            case ("supportsImmigration"):
            case ("conventional"):
            case ("reserved"):
          
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
            case ("likesToDate"):
            case ("leftFotLoveIntrest"):
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

            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication"):
                return ("they pulled themselves by their bootstraps");
           

             
            case ("MovesAlot"):
                return "they sure moved alot ";
            case ("SusMovments"):
                return "shapes talk about how odd the shape is... ";
            case ("worksWithFamily"):
            case ("hiredByAFamilymember"):
            case ("getsFiredAlot"):
            case ("RetiredYoung"):
            case ("DiedBeforeRetired"):
            case ("DevorcedManyPeople"):

            case ("marriedForLifeStyleNotLove"):
            case ("AdventureSeeker"):
            case ("liklyToHelpTheHomeless"):
            case ("isolated"):
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
            case ("TooTrustingOfEnemies"):
            case ("ArtSeller"):
            case ("likedToExperinceCulture"):
            case ("doesNotGiveToThoseInNeed"):
            case ("supportsImmigration"):
            case ("conventional"):
            case ("reserved"):
                return "TBD";

            default:
                return "missed a tag!" + key;

        }
    }


    //for the player 
    string translatePlayerOptionIntoText(string key)
    {
        switch (key)
        {
  

            case ("MovesAlot"):
            case ("SusMovments"):
                return "add some text here";
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "I heard they left town, I wonder if  that is true?";
            case ("familyPerson"):
                return "don't they have a family";
            case ("InLovewithspouseoffriend"):
            case ("BTrueTYourHeart"):
            case ("likesToDate"):
            case ("leftFotLoveIntrest"):
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
            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication")://OVER HERE 

                return "this case is not yet authored";


            default:
                return "missed a tag!" + key;

        }
    }

    enum TypeOfPlayerTexts
    {
        agreement, disagreement, moral
    }
    TypeOfPlayerTexts typeOfPlayerTexts;
    string getAgreementText(string surfaceValue, string rating, TypeOfPlayerTexts playerResponceType)
    {

        foreach (PlayerDialoug p in jsn.listOfPlayerDialougs)
        {
            if (p.playerSurfaceValue == surfaceValue && p.playerNarrativeElements.rating == rating)
            {
                if (playerResponceType == TypeOfPlayerTexts.agreement)
                {
                    return p.playerNarrativeElements.playerInAgreementText;

                }
                else 
                {
                    return p.playerNarrativeElements.playerInAgreementText;

                }
            }
        }
        return "No string found!";
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

    IEnumerator waitAndPrintMoralAreas(string text)
    {
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(text));
        yield return currentCorutine;
        cNPCHoldingStance();
    }

    IEnumerator waitAndPrintDisagreement(string text)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(text));
        yield return currentCorutine;

        //does the CNPC argue for the importance of their flag? 
        if (moralCounter == 0 && currentCNPC.IsMoralFocus(mapToCNPCMoralFactor(currentNode.ButtonText)))
        {
            StartCoroutine(waitAndPrintMoralAreas("CNPC argues for the importance of their flag")); //change this to a wait and print instead

        }
        if (moralCounter > 0 && currentCNPC.IsMoralFocus(mapToCNPCMoralFactor(currentNode.ButtonText)))
        {
            StartCoroutine(waitAndPrintAgreement("CNPC will not change there mind...,... transtion text")); //change this to a wait and print instead


        }else if (currentCNPC.FatherModel.isPragmatic) //check for whateverr model
        {

        } 
        else if (!currentCNPC.FatherModel.isPragmatic) //if it is not pragmatic //i.e. central 
        {

        }


        /*   helloworld-- this needs ti be as a default if not moral focus and not in conflict with the tasble then go here ??     if(currentNode.hatedFacts.Count <= 0 && currentTree.FullyExplored)
                {
                    //i.e. we walked about all the hated facts! 
                    converseAboutNextCharacter();
                }
                else
                {
                    startAconversation(currentTree);
                }*/


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
