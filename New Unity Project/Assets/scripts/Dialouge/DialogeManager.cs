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
    public static DialogeManager Instance { get; private set; }

    public string TranscriptString = "Before you can begin to determine what the composition of a particular paragraph will be, you must first decide on an argument and a working thesis statement for your paper. What is the most important idea that you are trying to convey to your reader? The information in each paragraph must be related to that idea. In other words, your paragraphs should remind your reader that there is a recurrent relationship between your thesis and the information in each paragraph. A working thesis functions like a seed from which your paper, and your ideas, will grow. The whole process is an organic one—a natural progression from a seed to a full-blown paper where there are direct, familial relationships between all of the ideas in the paper";

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
    public int CNPCIndexer=0;
    Coroutine currentCorutine;
    Coroutine currentThoughtBubbleCorutine;


    InterestingCharacters currentIntrestingCharracter;
    public List<string> currentTopicsAboutCurrentCharacter; //this is what we are using and clearing 
    string startingSceneText = "";

    

  

    //pubic for testing only -- change later 
    Dialoug currentNode;

     int moralFocusCounter = 0; 
   public List<string> currentCNPCExploredSurfaceValues = new List<string>(); 
    List<string> currentPlayerAttemptedArgumentSV = new List<string>();
    int currentCNPCDisagreements = 0;


    //find the player character when we end a conversation --- TODO

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        public void Start()
    {
        bgchar = FindObjectOfType<BackgroundCharacter>();
        jsn = FindObjectOfType<JsonLoader>();
        AllOpinions = jsn.listOfConversations;
        currentCNPC = FindObjectOfType<CharacterManager>().characters[0]; //hard coded with tim for now 
        OrgnizeCNPCOpinions();
        CharacterManager.Instance.instantiateCube();
        setUp();
        StartCoroutine(startGame(5f,treeCounter)); //change this to the animations time / or agents path time

        turnOffButton();//test only

       // TranscriptString = "Before you can begin to determine what the composition of a particular paragraph will be, you must first decide on an argument and a working thesis statement for your paper. What is the most important idea that you are trying to convey to your reader? The information in each paragraph must be related to that idea. In other words, your paragraphs should remind your reader that there is a recurrent relationship between your thesis and the information in each paragraph. A working thesis functions like a seed from which your paper, and your ideas, will grow. The whole process is an organic one—a natural progression from a seed to a full-blown paper where there are direct, familial relationships between all of the ideas in the paper";

    }

    public void nextCubeInteractionTest()
    {
        Debug.Log(CharacterManager.Instance.characters.Count);
        startNextCubeConversation(3);
  
    }

    IEnumerator startGame(float delay, int treeIndexer)
    {
        if (currentCorutine != null) { yield return currentCorutine; }
        
        Dialoug introductionNode = new Dialoug(" introduction", " Player says: \n hey there Mr. " + currentCNPC.ConversationalNpcName +
       " \n welcome to AB. I persume you are here for an argument?", "graduate");
        yield return new WaitForSeconds(delay);
       //move this into its file 
        //conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());//fltred list of characters (with 5+ flags)


         currentCorutine = StartCoroutine(TypeInDialoug(introductionNode.mainOpinionOnAtopic));
        yield return currentCorutine;

        currentCorutine = StartCoroutine( startAconversation(allCharacterConversationsTrees[treeIndexer]));
        yield return currentCorutine;

    }
    IEnumerator startNextCubeConversation(float delay)
    {
        if (currentCorutine != null) { yield return currentCorutine; }

        Dialoug endingDilaoug = new Dialoug(" introduction", " you time is up! Mr. " + currentCNPC.ConversationalNpcName +
       " \n Please see yourself out, my next client is here", "graduate");
        yield return new WaitForSeconds(delay);
        //move this into its file 
        //conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());//fltred list of characters (with 5+ flags)


        currentCorutine = StartCoroutine(TypeInDialoug(endingDilaoug.mainOpinionOnAtopic));
        CNPCIndexer += 1;
        currentCNPC = FindObjectOfType<CharacterManager>().characters[CNPCIndexer];
        startGame(5, treeCounter);


    }


    public void setUp()
    {
        /* Dialoug introductionNode = new Dialoug(" introduction", " Player says: \n hey there Mr. " + currentCNPC.ConversationalNpcName +
        " \n welcome to AB. I persume you are here for an argument?", "graduate"); //move this into its file 
         conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());//fltred list of characters (with 5+ flags)


         currentCorutine = StartCoroutine(TypeInDialoug(introductionNode.mainOpinionOnAtopic));*/
        conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());//fltred list of characters (with 5+ flags)

        setUpTrees();
    }
    void setUpTrees()
    {
        foreach (InterestingCharacters character in conversedAboutCharectersList)
        {

            Dialoug node = new Dialoug("thikning about " + character.fullName,
                         "hmm I have been thinking about the cube " + character.fullName,
                         returnpatternToSurfaceMapiing("graduate")); //CHANGE THIS TO INTRO AND WRITE A DEFAULT FOR MAPPING
            Tree tree = new Tree();
            tree.root = node;
            tree.root.children = returnListOfDialougNodes(character); //nodes with flags
            foreach(Dialoug d in tree.root.children)
            {
                d.parent = tree.root;
                //d.hatedFacts = getThingsHatedAboutBNPC(d,character);
              
            }
            allCharacterConversationsTrees.Add(tree);

          
        }
        foreach( Tree  t in allCharacterConversationsTrees)
        {
            //for debugging.... 
        }

       
        displayThoughts(allCharacterConversationsTrees);
        currentTree = allCharacterConversationsTrees[treeCounter];
       //TODO add startcorutine startAconversation(allCharacterConversationsTrees[treeCounter]);//the first cgharacter in the list - yhionking about character....etc 
     
    }

    public void addTextToTranscript(string text, bool NPCText)
    {
        if (NPCText)
        {
            TranscriptString += currentCNPC.ConversationalNpcName + ": " + text + "\n";
        } else
        {
            TranscriptString += "We responded with" + ": " + text + "\n";
        }

    }

    public void startTheFirstConversation()
    {
       StartCoroutine( startAconversation(allCharacterConversationsTrees[treeCounter]));//the first cgharacter in the list - yhionking about character....etc 

    }

    public void setConversationAndCnpc(ConversationalCharacter character, int treeCounter)
    {
        currentCNPC = character;
        StartCoroutine(startAconversation(allCharacterConversationsTrees[treeCounter]));//the first cgharacter in the list - yhionking about character....etc 

    }

/*    private List<string> getThingsHatedAboutBNPC(Dialoug d, InterestingCharacters character)
    {
        List<string> hatedFactsAboutThisCharacter = new List<string>();
        List<string> alreadyVistedFlags = new List<string>();
        foreach (KeyValuePair<string, bool> kvp in character.characterFlags)
        {
            if (kvp.Value) //ex:love affairs 
            {
                if (!alreadyVistedFlags.Contains(mapToCNPCMoralFactor(kvp.Key))) //TOFRICKENDO changet his to currentnode.sv
                {
                    if (mapToCNPCMoralFactor(kvp.Key) == "Enviromentalist" || mapToCNPCMoralFactor(kvp.Key) == "AnimalLover" ||
                        mapToCNPCMoralFactor(kvp.Key) == "LandISWhereThehrtIS")
                    { //only these two are kinda the oppasate
                        string fact = returnTopicText(highVaueOpinions, kvp.Key, "CON", "graduate");
                        hatedFactsAboutThisCharacter.Add(fact);
                    }
                    else
                    {
                        string fact = returnTopicText(lowVaueOpinions, kvp.Key, "CON", "graduate");
                        hatedFactsAboutThisCharacter.Add(fact);
                    }
                }

             alreadyVistedFlags.Add(mapToCNPCMoralFactor(kvp.Key));

            }
        }
        return hatedFactsAboutThisCharacter;

    }*/

    private void displayControlOprions()
    {

    }
    private IEnumerator startAconversation(Tree chosenTree)
    {
        //  moralCounter = 0; //reset it for next character
        yield return currentCorutine;

        currentNode = choseADialougNode(chosenTree.root.children);//i.e we are still in the same tree

        /* currentNode = choseADialougNode(chosenTree.root.children); //new node selection from another tree/branch 
         currentTree = chosenTree;*/

        currentCorutine = StartCoroutine(WaitAndPrintcompoundedStatments(currentNode.UnbiasedOpeningStatment,
                   currentNode.mainOpinionOnAtopic));
        addTextToTranscript(currentNode.UnbiasedOpeningStatment, true);
        addTextToTranscript(currentNode.mainOpinionOnAtopic, true);

        yield return currentCorutine;
        turnOnButtons();
        DisplayplayCurrentOpinions(currentTree);
        activatePlayerOptionsInButton(currentNode); //newcommentedout


    }
    private void cNPCHoldingStance() //does not transfer control 
    {

        StartCoroutine(WaitAndPrintcompoundedStatments("surface valuef for this flag"+currentNode.agreementText,
                "presist on importance of moral flag "));
        DisplayplayCurrentOpinions(currentTree);
        // activatePlayerOptionsInButton(currentNode); //newcommentedout
        //moralCounter += 1;
    }
    Dialoug choseADialougNode(List<Dialoug> bgCharacterNOdes)
    {

        // int r = UnityEngine.Random.Range(0, bgCharacterNOdes.Count - 1);
        int i = 0;
        foreach (Dialoug d in bgCharacterNOdes)
        {
            Debug.Log("list size is " + bgCharacterNOdes.Count) ;

            Debug.Log("value of node" + d.Pattern + "is "+ d.Explored + "value of counter"+i);
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
    void activatePlayerOptionsInButton (Dialoug node) //hardcoded for now - do this for the current height of the tree --- 
    {
        // Debug.Log(node.getHeight());//if the higfght is 2 then i can agree/ dissagree..etc with an option //height 1 then i am thinking aboyut the character ( height 2 is the flag / character topic) 
        // StartCoroutine(waitAndEnableButtons());
        /*        disableorEnablePlayerButtons();
        */ 
        //move varibles here 
        if (node.getHeight() == 2)
        {
            setPlayerPattern();
            //for testing 
            foreach(Button b in PlayerButtons)
            {
                b.onClick.RemoveAllListeners();
            }
            PlayerButtons[0].onClick.AddListener(playerAgrees);
            PlayerButtons[1].onClick.AddListener(playerDissAgrees);
            PlayerButtons[2].onClick.AddListener(playerArgueAboutFLag);//TOFIX
            PlayerButtons[3].onClick.AddListener(askAboutAnotherCharacter);//TOFIX
        }
    }
    void playerAgrees()
    {
     
        StartCoroutine(waitAndPrintAgreement(currentNode.agreementText)); // need to restrucre this ---
        addTextToTranscript(currentNode.agreementText, false);
    }
    private void playerDissAgrees()
    {

        
        StartCoroutine(waitAndPrintDisagreement(currentNode.disagreementText)); //currentNode.getRandomHatedFact())
        addTextToTranscript(currentNode.disagreementText, false);

    }

    Dialoug currentCNPCStance = new Dialoug("","", "graduate");
    public void playerArgueAboutFLag()
    {

    
        Debug.Log("current stanc eis "+ currentNode.Pattern);
        currentCNPCStance = currentNode;
        //present all flags for player as a sub menue 
        if(currentNode.getHeight() == 2) // change this into try /catch statments 
        {

            //FOR TESTING 
            for (int i = 0; i < PlayerButtons.Length; i++) //OTHER FLAGS
            {
                PlayerButtons[i].GetComponentInChildren<Text>().text = "argue for flag ->" + currentNode.parent.children[i].Pattern;
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
    private void setPlayerPattern()
    {
        PlayerButtons[0].GetComponentInChildren<Text>().text = "I agree with " + currentNode.Pattern;
        PlayerButtons[1].GetComponentInChildren<Text>().text = "I don't agree with you there";
        PlayerButtons[2].GetComponentInChildren<Text>().text = "you know I heard other things about that cube"; //pull from a list of random strings later //TODO
        PlayerButtons[3].GetComponentInChildren<Text>().text = "you know what, lets talk about something else";
    }
    private void setPlayerPatternDissagreement() //change this 
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
            b.onClick.AddListener(() =>  moveConversationToAflag(d));
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
       StartCoroutine( startAconversation(currentTree));
    }

    private void moveConversationToAflag(Dialoug d)
    {
        currentCorutine = StartCoroutine(moveConversationToAflag(d.Pattern));
    }

    private IEnumerator moveConversationToAflag(string pattern)
    {
        Debug.Log("TRIED TO GO HERE");
       // StopAllCoroutines();
        GuidialougText.text = "";
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(getPlayerResponceToAflag(currentNode.MappedSurfaceValue, pattern, currentNode.Rating, TypeOfPlayerTexts.disAgreeOnAflag, false)));
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(currentNode.disagreementText));
        if (currentNode.Rating.ToLower() == "high")
        {
            currentCorutine = StartCoroutine(TypeInDialoug(
           currentCNPC.FatherModel.returnFatherModelArgumetnsText(
               currentNode.MappedSurfaceValue, currentNode.Pattern, new List<string>() { }, true)));
        }


        turnOffButton();//new test comment 
        checkEndConversationAndMove();





    }

    string getPlayerResponceToAflag(string surfaceValue, string arguedFlag, string rating, TypeOfPlayerTexts playerResponceType, bool isMoralARG) //change this intyo player responce
    {
        foreach (PlayerDialoug p in jsn.listOfPlayerDialougs)
        {
            if (p.playerSurfaceValue == surfaceValue && p.playerNarrativeElements.rating == rating) //prints approproate high/mid/low if its not the moral focus argument
            {
                if (!isMoralARG)
                {
                    foreach (PlayerDisAgreementOnAflag f in p.playerNarrativeElements.playerDisAgreementOnAflag)
                    {
                        Debug.Log("THE f.flag " + f.flag + "and the patytern is " + arguedFlag);

                        if (f.flag == arguedFlag)
                        {

                            return "PLAYER SAYS: \n" + f.textValue;
                        }

                    }


                }
                else // moral 
                {
                    Debug.Log("player is refrencing NPC moral focus argument");
                    if (playerResponceType == TypeOfPlayerTexts.agreement)
                    {

                        return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerInAgreementText;
                    }
                    else //player in disagement of moral focus 
                    {
                        //special loop happens here 
                        if (moralFocusCounter == 0)
                        {
                            moralFocusCounter += 1;
                            return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerMoralDisagreementText[0]; //player can agree or disagree here 

                        }
                        else
                        {
                            moralFocusCounter = 0;
                            return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerMoralDisagreementText[1];

                        }

                    }

                }
            }
        }
        return "No string found!";
    }




    void converseAboutNextCharacter()
    {
        treeCounter++;
       StartCoroutine( startAconversation(allCharacterConversationsTrees[treeCounter]));//the first cgharacter in the list - yhionking about character....etc 

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
            startAconversation(currentTree); //TODO add startcourutine 
            //go to another character. 
        }

        // bgCharacterNOdes.RemoveAt(i);//que and deque from a list ---OR FLAG VISITED add logic on how to choose a better character - random for now 
        return currentNode;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            nextCubeInteractionTest();
        }
        // Debug.Log(currentNode.Pattern);
        /*    Debug.Log("INSIDE UPDATE --- CURNODE currentNode.Pattern  " +currentNode.Pattern + "  topic introduction: " + currentNode.IntroducingATopicdialoug
                +"intro baised<dtext>"+ currentNode.dialougText +"a node's agreement"+currentNode.agreementText +"nodes dissagreement text"+ 
                currentNode.hatedFacts[0]+"fully explored this node"+currentNode.Explored);
            Debug.Log(treeCounter);
    */
    }

    private void displayDialougOpinion()
    {
        StopAllCoroutines();
        StartCoroutine(TypeInDialoug(currentNode.mainOpinionOnAtopic));
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
                string mappedKey = returnpatternToSurfaceMapiing(kvp.Key);
                Dialoug node = new Dialoug(
                   translateOpinionIntoText(kvp.Key), //thought bubble 
                   setCnpcDialoug(kvp.Key, character, "BiasedSVOpin", mappedKey),
                   mappedKey);

              
                node.UnbiasedOpeningStatment =
                    getIntroductionTopicString(kvp.Key, character); //unbaised opening statment  --- this is actually the inytoduction text 
              
                node.agreementText = setCnpcDialoug(kvp.Key, character, "PlayerAgreesWithCNPC", mappedKey);//general feelings about a topic  --- this is actually high 

                node.disagreementText = setCnpcDialoug(kvp.Key, character, "PlayerDisAgreesWithCNPC", mappedKey);//nope - con here is as in low.... 
                node.Pattern = setPlayerPattern(kvp.Key); //flag or pattern for now but this mighht actually be the button text for the player to click on... 
                node.Rating = setNodeRating(mappedKey);

                if (node.Rating.ToLower() == "high")
                {
                   node.setupMoralFocusArguments(returnTopicText(highVaueOpinions, node.Pattern, "MoralFocusOne", node.MappedSurfaceValue),
                       returnTopicText(highVaueOpinions, node.Pattern, "MoralFocusTwo", node.MappedSurfaceValue));


                }
                if (node.Rating.ToLower() == "mid")
                {
                    node.setupMoralFocusArguments(returnTopicText(midVaueOpinions, node.Pattern, "MoralFocusOne", node.MappedSurfaceValue),
                      "should not appear");
                }
                else
                {
                    node.setupMoralFocusArguments("error shouled not appear", "should not appear");

                }
             


                nodes.Add(node);


              /*  Debug.Log("TEST: the node " + node.Pattern + " was mapped to SV: " + node.MappedSurfaceValue + " has a rating of " + node.Rating + "and a moral argument of " +
                    node.moralDisagreementText[0] +"and moral focus arg 2" + node.moralDisagreementText[1]);*/
            }
        }
        return nodes;

        //int i = UnityEngine.Random.Range(0, character.characterFlags.Count);

    }

    

    private string setPlayerPattern(string key)
    {
        //will change depeninding on input for now its set to just the key 
        return key ; //will add logic about current cnpc thoughts and modding this later 
    }

    //2 opinionsa re split up into high mid and low 
    string returnTopicText(List<DialougStructure> opinions, string key, string flag, string mappedSV) //the sent in list is of high./mid or low 
    {//selecting intro and the first part of the body here --- 
        foreach (DialougStructure op in opinions) //ex: all high opp -- splits them up here as high mid and low 
        {
            

            if (op.topic.Contains(mappedSV)) //get the translatiopn of they key but not ditect character keys.....  //TOFRICKENDO changet his to currentnode.sv
            {
               // Debug.Log(op.topic.Contains(mapToCNPCMoralFactor(key)) + "for the key " + key);
                selectedOpnion = op.topic.Split('_').Last(); //surface value is returned here = -  sv selected opinion 
               // Debug.Log("selectedOpnion" + selectedOpnion +" AND FLAG "+ flag +"TOPIC"+op.topic);// FLAG isd what i send over to classify as intro text pt agreement or .... 
                //selectedOpinion is the surface value

                //redo this super bad method 
                if (flag == "BiasedSVOpin")
                {
                    return op.NarrativeElements.surfaceOpinionOnTopic;
                }
                if (flag == "PlayerAgreesWithCNPC")
                {
                    return op.NarrativeElements.agreementText;
                }
                if (flag == "PlayerDisAgreesWithCNPC")
                {
                   // Debug.Log("setting disagreement text as " + op.NarrativeElements.disagreementtext);
                    return op.NarrativeElements.disagreementtext;
                } 
                if( flag == "MoralFocusOne")
                {
                    return op.NarrativeElements.moralFocusDisagreement[0];

                }
                if ( flag == "MoralFocusTwo")
                {
                    return op.NarrativeElements.moralFocusDisagreement[1];

                }
            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;
    }

    string setNodeRating(string mappedSV)
    {
        switch (currentCNPC.ConvCharacterMoralFactors[mappedSV]) //TOFRICKENDO changet his to currentnode.sv
        {
            case ConversationalCharacter.RatingVlaues.High:
                return "High"; //make this more generic - iof/else get topic or body (startingopinion) or contrasting one... nut need logic on each case i think of this is testign a thing for now 
            case ConversationalCharacter.RatingVlaues.Mid:
                return "Mid";
            default://low
                return "Low";
        }
    }
        //1
    private string setCnpcDialoug(string key, InterestingCharacters character, string flag, string mappedSV )
     {//this is hard coded for now but later send in the conversational character (talking with )
       // Debug.Log("yo what does this print?" + currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)]); ////gives us high mid oir low 
        // high - low and mid results --- cool 
        switch (currentCNPC.ConvCharacterMoralFactors[mappedSV])
        {
            case ConversationalCharacter.RatingVlaues.High:
                return returnTopicText(highVaueOpinions, key, flag, mappedSV); //make this more generic - iof/else get topic or body (startingopinion) or contrasting one... nut need logic on each case i think of this is testign a thing for now 
            case ConversationalCharacter.RatingVlaues.Mid:
                return returnTopicText(midVaueOpinions, key, flag, mappedSV);
            default://low
                return returnTopicText(lowVaueOpinions, key, flag, mappedSV);                
        }
    }
    private string returnpatternToSurfaceMapiing (string pattern)// has a bestfriend---- check this me 
    {
        List<string> temporarySurfaceValues = new List<string>();
        string mappedSV = "";
        temporarySurfaceValues = SVCollection.returnCompatibleSurfaceValues(pattern);
        mappedSV = temporarySurfaceValues[UnityEngine.Random.Range(0, temporarySurfaceValues.Count())];      
        return mappedSV;

    }
    private string ReturnAgreement(List<DialougStructure> highVaueOpinions, string key)
    {
        throw new NotImplementedException();
    }
    //HELOME - update below mapping for more than 1 to 1 

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

    string getIntroductionTopicString(string key, InterestingCharacters character )
    {
        //currentCNPC.ConvCharacterMoralFactors get the opinion t
        switch (key)
        {
            case ("startedAfamilyAtAyoungAge"):
                return "I heard some shapes think that " + character.fullName + " started their family way too early.";
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "guess what I heard! " + character.fullName + " left town!, ";
            case ("familyPerson"):
                return "hmm, did you know that  " + character.fullName + " has a " + "big family and that is a lot of young shapes in one household.";

            //add more values for the ones below --- seprate core values perhaps?mainly tags are used for flavor text but they fall for one core value 
            case ("InLovewithspouseoffriend"):
                return "I heard a rumor that" + character.fullName + "is in love with their partner’s friend!";

            case ("leftFotLoveIntrest"):

               return " I heard that " + character.fullName + "left their significant other with " + character.GetLoverName();
            case ("InLoveWirhAnothersspouce"):
                return "I heard from somewhere that " + character.fullName + " is in love with their partner's friend! ";
            case ("WillActOnLove"):
                return "Not judging but I heard that " + character.fullName + "is chasing after a shape who is not their partner";


            case ("BTrueTYourHeart"):
            case ("likesToDate"):
                return "Maybe it’s something in the air," +
                    "I feel like" +character.fullName + 
                    ", like every shape in this town, is constantly either in love or chasing after someone"; 


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
                return "Everyone knows that" + character.fullName + "is very wealthy.";
            case ("IsRichButNotGenrous"):
                return "the " + character.fullName +
                    "are wealthy apparently, like super wealthy... but also so not the giving type!";


            case ("WorksInAlcohol"):
                return "I think" +character.fullName + "sells booze for a living";
            case "Teetotasler":
                return character.fullName + "wokrs in alchohol, wonder what that field is like";

            case ("flipflop"):
                return "they are so indecisive when it comes to their career! ";

            case ("healerRole"):
            case ("CustodianJobs"):
            case ("SupportingComunities"):


            case ("polluterRole"):
                return "I think" + character.fullName + "\'s job contributes to pollution\n I think they work as " + character.Lastoccupation; 
            case ("Enviromentalist"):
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation;
            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication"):
                return " rumor says " + character.fullName + "came from nothing, they actually managed to advance in their carreers by dedication alone";

            case ("riskTaker"):
            case ("LoverOfRisks"):

            case ("generalJobs"):


            case ("advancedCareer"):
            case ("hardWorker"):
                return "Everyone knows that" + character.fullName + "is a hard worker.";
            case "CarrerAboveAll":
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n moreso, I beliave that cube works so many hours! I htink they are a hardworking cube. ";

            case ("Teachingrole"):
            case ("SchoolIsCool"):
                return "I believe" + character.fullName + "works in a school as a." + character.Lastoccupation;
            case ("butcherRole"):
                return "I think" +character.fullName + "works in a butcher shop.";
            case ("AnimalLover"):
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation +"in a farm";

            case ("MovesAlot"):
                return "apparently this shape never settles down, they sure moved alot ";
                
            case ("SusMovments"):
                return "shapes whisper that  " + character.fullName + " is quite odd";

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
                return "It’s kinda well known that" + character.fullName + "is still not working despite how much older they are getting";

            case ("widowedbutnotgrieving"):
                return "It’s probably not my place to say this but this...\n" + character.fullName + " \n" +
                    "might be moving on a bit too fast from their recently deceased partner";

            case ("exploteative"):
                return "I heard shapes saying that\n" + character.fullName + " \n" +
                    "got to where they are now by exploiting and cheating the system… there’s even a rumor that they got their job through nepotism...";


            case ("graduate"):
                return "I heard" + character.fullName + "has a fancy degree. Shapes often think that a good degree is the key to a good job and a comfortable life.";
            case ("worksWithFamily"):
                return "I heard that" +character.fullName + "lives and works with their family.";
            case ("hiredByAFamilymember"):
                return "I heard from someone that" + character.fullName + "got their job because of their family member";
            case ("getsFiredAlot"):
                return "oh boy, wonder why this cube always gets canned";
            case ("RetiredYoung"):
                return "how DID They retire that young!";//maybe add age here 
            case ("DiedBeforeRetired"):
                return "it's so sad, but " + character.fullName + "died before retiring";
            case ("DevorcedManyPeople"):

                return "This might be rude to say but I have lost count of how many partners" +character.fullName + "had";
            case ("marriedSomoneOlder"):
                return "they settled down with an older partner";
            case ("marriedForLifeStyleNotLove"):
                return "I hear tha" + character.fullName + "decided to settle down with a partner because of what society expects of them and not out of love.";
            case ("AdventureSeeker"):
                return "ah to have an adventurur's heart";
            case ("liklyToHelpTheHomeless"):
                return "I heard that this shape likes to volenteer at homeless shelters ";
            case ("isolated"):
                return "this shape does not like to socilize, to a point where theya void other shapes";
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
                return "I heard" + character.fullName +
                    "told someone that they can’t sleep at night because of what they do… you know... killing animals. That must weighs heavily";
            case ("TooTrustingOfEnemies"):
                return "I do not know if" + character.fullName + "is Naieve or too kid but they are too trusting of their enemies";
            case ("conventional"):
                return "Most shapes would describe" + character.fullName +
                    "as responsible, conventional, and a bit by-the - book or stubborn depending on who you ask.";
            case ("likedToExperinceCulture"):
                return character.fullName + " likes to experince diffrient cultures";
            case ("ArtSeller"):
            case ("doesNotGiveToThoseInNeed"):
                return "you know, despite " + character.fullName + "being wealthy, they do not donate";
            case ("supportsImmigration"):
                return character.fullName + " is often known for their support of immigration";
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
            case ("InLoveWirhAnothersspouce"):
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
                return "how do they live and not work?";
            case ("widowedbutnotgrieving"):
                return "wonder why they are not grieving...";
            case ("exploteative"):
                return " they know how to work a room";
            case ("graduate"):
                return "rthey graduated!";
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
                return "works with family..";
            case ("hiredByAFamilymember"):
                return "hired by family...";
            case ("getsFiredAlot"):
                return "cant keep a job...";
            case ("RetiredYoung"):
                return "must be nice, retired at a young age";
            case ("DiedBeforeRetired"):
                return "died while on the job";
            case ("DevorcedManyPeople"):
                return "this shape devorced other shapes a significant number of times";
            case ("marriedSomoneOlder"):
                return "this shape married somone older";
            case ("marriedForLifeStyleNotLove"):
                return "settled down for lifestyle";
            case ("AdventureSeeker"):
                return "such an adventure seeker";
            case ("liklyToHelpTheHomeless"):
                return "helps the homeless";
            case ("isolated"):
                return "likes to stay alone";
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
                return "regretful butcher";
            case ("TooTrustingOfEnemies"):
                return "Too trusting of enemies";
            case ("conventional"):
                return "conventional shape";
            case ("likedToExperinceCulture"):
                return "likes diff cultures";
            case ("ArtSeller"):
            case ("doesNotGiveToThoseInNeed"):
                return "does not donate...";
            case ("supportsImmigration"):
                return " this shape is often foun dsupporting immagrents";
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
            case ("InLoveWirhAnothersspouce"):
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
        agreement, disagreement, disAgreeOnAflag
    }
    TypeOfPlayerTexts typeOfPlayerTexts;

    string getPlayerResponce(string surfaceValue, string rating, TypeOfPlayerTexts playerResponceType, bool isMoralARG) //change this intyo player responce
    {

        foreach (PlayerDialoug p in jsn.listOfPlayerDialougs)
        {
            if (p.playerSurfaceValue == surfaceValue && p.playerNarrativeElements.rating == rating) //prints approproate high/mid/low if its not the moral focus argument
            {
                if (!isMoralARG)
                {
                    if (playerResponceType == TypeOfPlayerTexts.agreement)
                    {
                        return "PLAYER SAYS: \n"+   p.playerNarrativeElements.playerInAgreementText;

                    }
                  
                    else {
                        return "PLAYER SAYS: \n" +  p.playerNarrativeElements.playerDisagreementText;
                    }
                 
                } 
                else // moral 
                {
                    Debug.Log("player is refrencing NPC moral focus argument");
                    if (playerResponceType == TypeOfPlayerTexts.agreement)
                    {

                        return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerInAgreementText;
                    }
                    else //player in disagement of moral focus 
                    {
                        //special loop happens here 
                        if (moralFocusCounter == 0)
                        {
                            moralFocusCounter += 1;
                            return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerMoralDisagreementText[0]; //player can agree or disagree here 

                        }
                        else
                        {
                            moralFocusCounter = 0;
                            return "PLAYER SAYS: \n" + p.playerNarrativeElements.playerMoralDisagreementText[1];

                        }

                    }

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

    void turnOffButton()
    {
        foreach (Button b in PlayerButtons)
        {
            b.gameObject.SetActive(false);
            b.interactable = false;
        }
    }

    void turnOnButtons()
    {
        foreach (Button b in PlayerButtons)
        {
            b.gameObject.SetActive(true);
            b.interactable = true;
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
        yield return new WaitForSeconds(2);
        checkCorutine = true;
    }



    IEnumerator waitAndPrint(Dialoug node)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(node.mainOpinionOnAtopic));

    }

    IEnumerator waitAndPrint(string text)   //so this works... but does nto when in larger scenartio.. 
    {

        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(text));

    }

    IEnumerator waitAndPrintAgreement(string text)   //so this works... but does nto when in larger scenartio.. 
    {

        if (!currentTree.FullyExplored)
        {
            yield return currentCorutine;
            currentCorutine = StartCoroutine(TypeInDialoug(getPlayerResponce(currentNode.MappedSurfaceValue, 
                                currentNode.Rating, TypeOfPlayerTexts.agreement, false))); 
            yield return currentCorutine;
            currentCorutine = StartCoroutine(TypeInDialoug(text));
            turnOffButton();//new test comment 
        }
         

        checkEndConversationAndMove();
    }

    private void checkEndConversationAndMove()
    {
        if (!currentTree.FullyExplored)
        {
          StartCoroutine(  startAconversation(currentTree));//at 0 
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
        //remove this later but for now... 
        List<string> exploredPattensForDefense = new List<string>(); //move to global level and clear it later 
        if (!currentTree.FullyExplored)
        {   

            bool isMf = currentCNPC.IsMoralFocus(currentNode.MappedSurfaceValue); 

            //basic disagreement 1
            yield return currentCorutine;
            currentCorutine = StartCoroutine(TypeInDialoug(getPlayerResponce(currentNode.MappedSurfaceValue, currentNode.Rating,
                                 TypeOfPlayerTexts.disagreement, isMf))); // CAN REFACTOR THIS  //TOFRICKENDO changet his to currentnode.sv
            yield return currentCorutine;
            currentCorutine = StartCoroutine(TypeInDialoug(text));
            yield return currentCorutine;
           
            if(currentNode.Rating.ToLower() == "high")
            {
                currentCorutine = StartCoroutine(TypeInDialoug(
               currentCNPC.FatherModel.returnFatherModelArgumetnsText(
                   currentNode.MappedSurfaceValue, currentNode.Pattern, new List<string>() { }, true)));
            }
     

        }
        checkEndConversationAndMove();

        //new 
        /*       if (isMf) //if the last loop was a moral focus 
               {
                   currentCorutine = StartCoroutine(TypeInDialoug(currentNode.moralDisagreementText[0]));

               } else
               {
                   currentCorutine = StartCoroutine(TypeInDialoug(currentNode.moralDisagreementText[1]));

               }*/


    }


    /*// if currentconversation loop <= 1,,, 
    else if(currentConversationalLoopSingle > 1 && currentConversationalLoopSingle <=3)
    {   if(currentNode.Rating =="High") //refrence father model 
        {
            currentCNPCExploredSurfaceValues.Add(currentNode.Pattern);

        }
        else // mid or low just move to the next conversation if it is not a moral focus area. 
        { 

        }

        //refrence father model 

    } else if (currentConversationalLoopSingle >4) //move it away if npc is not convinced 
    {
        if (currentNode.Rating == "High") //if its after fm and high then move to another node and clear the list of explored nodes for subvalues in  schemas.
        {

        }
    }
     if (moralCounter == 0 && currentCNPC.IsMoralFocus(currentNode.MappedSurfaceValue)) //TOFRICKENDO changet his to currentnode.sv
    {
        StartCoroutine(waitAndPrintMoralAreas("CNPC argues for the importance of their flag")); //change this to a wait and print instead

    }
    if (moralCounter > 0 && currentCNPC.IsMoralFocus(currentNode.MappedSurfaceValue)) //TOFRICKENDO changet his to currentnode.sv
    {
        StartCoroutine(waitAndPrintAgreement("CNPC will not change there mind...,... transtion text")); //change this to a wait and print instead
    }*/



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
        currentCorutine = StartCoroutine(TypeInDialoug(node1.mainOpinionOnAtopic));
        yield return currentCorutine;
        currentCorutine = StartCoroutine(TypeInDialoug(node2.mainOpinionOnAtopic));
    }
    Coroutine DisplayNodeIntroTopic(Dialoug d)
    {
        Coroutine c = StartCoroutine(TypeInDialoug(d.UnbiasedOpeningStatment));
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



/// <summary>
///  TODO.
///  REFRENCE fATHER MODEl Method 
///  check for avaible tags 
///  
/// </summary>

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

      StartCoroutine(WaitAndPrintcompoundedStatments(currentNode.IntroducingATopicdialoug, currentNode.unbaisedIopinion));//so this works... but does nto when in larger scenartio.. 
         //// -- end of testing case 
*/

//DisplayThoughtBubbles();
