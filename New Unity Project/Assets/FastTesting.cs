using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.IO;

public class FastTesting : MonoBehaviour
{

    public Text text;
    public InputField input;

    // Start is called before the first frame update

    
    int characterLimit = 20000;

    public List<Tree> allCharacterConversationsTrees = new List<Tree>();
    private Tree currentTree;

    public List<Dialoug> fullCharacterRootNodes;
    public List<Dialoug> currentPlayerOptions;
    public List<Dialoug> CurrentIntersectingSFMcorePatterns;
    public List<Dialoug> CurrentBNPCgenericPatterns;
    public List<Dialoug> currentTopicChoices;
    public int treeCounter = 3;

    public JsonLoader jsn;

    public List<DialougStructure> AllOpinions; //not done the best way but for rapid testing for now 
    List<DialougStructure> highVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> midVaueOpinions = new List<DialougStructure>();
    List<DialougStructure> lowVaueOpinions = new List<DialougStructure>();

    public ConversationalCharacter agent;

    //add a node to check if explored 
    public BackgroundCharacter bgchar;
    string selectedOpnion;
    public List<InterestingCharacters> conversedAboutCharectersList;

    InterestingCharacters currentIntrestingCharracter;

    //high good bad is low
    Dialoug currentNode;

    int moralFocusCounter = 0;



    //testing:
    bool isInMoralModelArgumentLoop = false;
    int moralModelCounterLoop = 0;


    public static List<string> currentMoralModelExploredPatterns = new List<string>();
    public static List<string> currentMoralModelExploredPatterns_PLAYER = new List<string>(); //FIXC THIS 

    public List<Dialoug> CurrentBNPCPatterns;
    List<Dialoug> currentIntersectingNodes;
    List<Dialoug> nonIntersectingNodes_generic; //something is wrong in this... 
    Queue<Dialoug> currentPlayerOptionsInInnerConversation = new Queue<Dialoug>();

    List<string> currentBNPCPatterns = new List<string>(); //why oth dies and strings --- what did i do here?
    int innerConversationCounter = 0;
    string CurrentCNPCSTANCE = "high";
    string currentPlayerStance = "low";

    int counterLoopForSchema = 0;

    bool playerChoseAModel = false;
    bool playerArguesWithFatherModel = false;


    string playerName = "player";


    public void Start()
    {
        initiFile();
        Invoke("t", 2);



    }

    public void t()
    {
        bgchar = FindObjectOfType<BackgroundCharacter>();
        // DiagramWindow = FindObjectOfType<DaigramWindow>();
        AllOpinions = jsn.listOfConversations; 
        //Debug.Log(AllOpinions.Count);
        //Debug.Log("agent" + agent.ConversationalNpcName);
        OrgnizeCNPCOpinions();
        setUp();
        startGame(5f, treeCounter); //change this to the animations time / or agents path time
       Debug.Log("is it part of father mdoel " + agent.IsFatherModel);

        currentMoralModelExploredPatterns = new List<string>();
        currentMoralModelExploredPatterns_PLAYER = new List<string>();
    }

    private void resetConversationForNextCNPC()
    {
        //Debug.Log("reseting agent and conversations");
        CurrentCNPCSTANCE = "high";
        currentPlayerStance = "low";
        innerConversationCounter = 0;
        moralModelCounterLoop = 0;
        isInMoralModelArgumentLoop = false;
        counterLoopForSchema = 0;
        currentMoralModelExploredPatterns.Clear();
        currentMoralModelExploredPatterns_PLAYER.Clear();
          agent = new ConversationalCharacter();
        //agent.ResetTestingAgent();
        agent.PlayerScore = 0;
        agent.CNPCScore = 0;
        startNextRound();


    }
    public Dialoug returnCurrentNode()
    {
        return currentNode;
    }
    public void nextCubeInteractionTest()
    {
        Debug.Log(CharacterManager.Instance.ourConversationalCharacters.Count);
      //  startNextCubeConversation(3);

    }

    void startGame(float delay, int treeIndexer)
    {
        

        Dialoug introductionNode = getAnIntroductionNode();

        printText(false, introductionNode.mainOpinionOnAtopic );
      

        startAconversation(allCharacterConversationsTrees[treeIndexer]);

    }

    void startNextRound()
    {
        Dialoug introductionNode = getAnIntroductionNode();

        printText(false, introductionNode.mainOpinionOnAtopic);
        converseAboutNextCharacter();

        //startAconversation(allCharacterConversationsTrees[treeCounter+1]);
    }

    void printText (bool isNPC, string t)
    {
        if (isNPC)
        {
            
            text.text += "<color=#c6c5b9>" + t + "\n </color>";
            WriteToFile("npc text:  "+ t + " \n");
        }
        else
        {
       /*     Color c = Color.blue;
            text.color = c;*/
            text.text += "  <color=#62929e>" + t + "</color> \n";
            WriteToFile("PLAYER text:  " + t + " \n");

        }
    }

    private void startAconversation(Tree chosenTree)
    {
        //  moralCounter = 0; //reset it for next character

     
      currentNode = choseADialougNode(chosenTree.root.children);//i.e we are still in the same tree

        //  Debug.Log("current node" + currentNode.Pattern);


        printText(true, "<color=yellow> initial pattern: " + currentNode.Pattern + "</color> "+ currentNode.UnbiasedOpeningStatment +
                     "\n <color=yellow> Mapped SV:  " + currentNode.MappedSurfaceValue + " Rating: "+
                     agent.ConvCharacterMoralFactors[currentNode.MappedSurfaceValue].ToString() +
                    "</color> \n" +  currentNode.mainOpinionOnAtopic + "\n");
        text.text += "<color=orange> other possible mappings for the pattern </color> " + currentNode.Pattern + "  <color=orange>includes:\n</color>";
        WriteToFile(" other possible mappings for the pattern </color> " + currentNode.Pattern + "  <color=orange>includes:\n</color>");
        printAdditionalInformation(returnAllPossibleSubToSVMapping(currentNode));
        text.text += "<color=orange>other possible  SV sentences include:\n</color>";
        WriteToFile("<color=orange>other possible mappings for  SVsentences include:\n</color>");

        printAdditionalInformation(returnAllPossibleSVOpeningStrings(currentNode.MappedSurfaceValue));

        currentBNPCPatterns.Clear();//should this be cleared? check it out me psyduck
        foreach (Dialoug d in currentNode.parent.children)
        {
            currentBNPCPatterns.Add(d.Pattern);
            Debug.Log(d.MappedSurfaceValue + "!!!");
        }

        setPlayerPattern();
     //BAM   activatePlayerOptionsInButton(currentNode); //newcommentedout

    }
    private Dialoug getAnIntroductionNode()
    {
        Dialoug Intronode = new Dialoug(" introduction" ,
            getRandomIntroduction(), "graduate"); 
        return Intronode;


    }

    private string getRandomIntroduction()
    {
        int x = UnityEngine.Random.Range(0, 5);
        string s = "Welcome  " + agent.ConversationalNpcName + "\n";
        string[] intro = {
          "Thanks for coming to argument Box, what rumors are on your mind?",
          "Thanks for coming to argument Box, home of(mostly) vauge arguments, whats on your mind",
          "Thanks for coming to the old AB, what's on your noodle doodle",
          "Thanks for coming in, I am more than just a greeter, (or punching bag), what topics brought you in? ",
          "Thanks for coming in lovly shape, your rumors are <mostly> safe here!, what's on your mind"
        };

        return s + intro[x];
    }




    public void setUp()
    {
        conversedAboutCharectersList = sortCharactersToBringUp(bgchar.GetFiltredCharerList());
        Debug.Log(conversedAboutCharectersList.Count);
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
            foreach (Dialoug d in tree.root.children)
            {
                d.parent = tree.root;
                //d.hatedFacts = getThingsHatedAboutBNPC(d,character);

            }
            allCharacterConversationsTrees.Add(tree);


        }
        foreach (Tree t in allCharacterConversationsTrees)
        {
            //for debugging.... 
        }


        currentTree = allCharacterConversationsTrees[treeCounter];
    }

    public void startTheFirstConversation()
    {
        startAconversation(allCharacterConversationsTrees[treeCounter]);

    }


    private void cNPCHoldingStance() //does not transfer control 
    {

        text.text +=  "surface valuef for this flag" + currentNode.agreementText + 
                "presist on importance of moral flag ";
        WriteToFile("surface valuef for this flag" + currentNode.agreementText +
                "presist on importance of moral flag ");

    }
    Dialoug choseADialougNode(List<Dialoug> bgCharacterNOdes)
    {

        int i = 0;
        foreach (Dialoug d in bgCharacterNOdes)
        {
           // Debug.Log("list size is " + bgCharacterNOdes.Count);

           // Debug.Log("value of node" + d.Pattern + "is " + d.Explored + "value of counter" + i);
            if (!d.Explored)//element is not explored 
            {
                d.Explored = true;
                return d;
            }
            i++;
            if (i >= bgCharacterNOdes.Count)//last element in the list 
            {
                Debug.Log("last element of the list");
                currentTree.FullyExplored = true;
                break;
            }
        }


        return currentNode;

    }



    List<Dialoug> temporaryListOfNodes = new List<Dialoug>();

  

    private void PlayerDisagreedOnFlag(Dialoug d)//player
    {
        waitAndPrintFatherModelDissagreemnt(d, playerArguesWithFatherModel); //1
    }


    private void playerChoseMotherModel()
    {

        currentIntersectingNodes = getIntersectingCoreValueNodesForATopic();
        currentIntersectingNodes = shuffleDialougNodes(currentIntersectingNodes);

        nonIntersectingNodes_generic = getNonIntersectingCoreValueNodesForATopic();
        nonIntersectingNodes_generic = shuffleDialougNodes(nonIntersectingNodes_generic);
        //currentPlayerOptionsInInnerConversation
        setCurrentPlayerSFOptions();
        presentFMMoptions();

        playerChoseAModel = true;
        playerArguesWithFatherModel = false;
    //bAM    activatePlayerOptionsInButton(currentNode);
    }


    private void playerChoseFatherModel()
    {

        currentIntersectingNodes = getIntersectingCoreValueNodesForATopic();
        currentIntersectingNodes =  shuffleDialougNodes(currentIntersectingNodes);

        nonIntersectingNodes_generic = getNonIntersectingCoreValueNodesForATopic();
        nonIntersectingNodes_generic = shuffleDialougNodes(nonIntersectingNodes_generic);

        setCurrentPlayerSFOptions();//node setup
        presentFMMoptions();
        playerChoseAModel = true;
        playerArguesWithFatherModel = true;
     //bam   activatePlayerOptionsInButton(currentNode);

    }
    //TICKTICKBOOM REACHED HERE ( TITLE OF MOVIE ) 
    void presentFMMoptions()
    {
        temporaryListOfNodes.Clear();
        int maxoptions = 3;
        for (int i = 0; i < maxoptions ; i++)
        {
            if (i <= maxoptions )
            {

                Dialoug d = currentPlayerOptionsInInnerConversation.Dequeue();
                text.text += (i+1).ToString() + getButtonTextTranslation(d.Pattern) +"under the topic[" + currentNode.MappedSurfaceValue +"]\n";
                WriteToFile((i + 1).ToString() + getButtonTextTranslation(d.Pattern) + "\n");
                temporaryListOfNodes.Add(d);


            }
        }
        text.text += "4. change topic \n";
        WriteToFile("4. change topic \n");
    }

    private List<Dialoug> shuffleDialougNodes(List<Dialoug> nodes)
    {
        //based on  Fisher-Yates shuffle algorithm - from delftstack
        List<Dialoug> newList = new List<Dialoug>();
        System.Random r = new System.Random();
        for (int i = nodes.Count -1; i > 0; i--)
        {
            int j = r.Next(i+1);
            Dialoug d = nodes[j];
            nodes[j] = nodes[i];
            nodes[i] = d;

            newList.Add(d);
        }

        return newList;
    }

    void setCurrentPlayerSFOptions()
    {

        currentPlayerOptionsInInnerConversation.Clear();
        foreach (Dialoug d in currentIntersectingNodes)
        {
            currentPlayerOptionsInInnerConversation.Enqueue(d);
        }
        foreach (Dialoug d in nonIntersectingNodes_generic)
        {
            currentPlayerOptionsInInnerConversation.Enqueue(d);
        }
    }

    private List<Dialoug> getIntersectingCoreValueNodesForATopic()
    {

        List<Dialoug> currentIntersectingNodesWithCoreValues = new List<Dialoug>();

        CurrentBNPCPatterns.Clear();
        foreach (Dialoug d in currentNode.parent.children)
        {
           // Debug.Log(d.Pattern + "::::" + d.MappedSurfaceValue);
            CurrentBNPCPatterns.Add(d);

        }
        currentIntersectingNodesWithCoreValues.Clear();

        if (agent.IsFatherModel)
        {
            foreach (Dialoug d in agent.FatherModel.returnIntersectingPatternNodes(CurrentBNPCPatterns, currentNode.MappedSurfaceValue, true))
            {
                currentIntersectingNodesWithCoreValues.Add(d);

            }
        }
        else
        {
            foreach (Dialoug d in agent.MotherModel.returnIntersectingPatternNodes(CurrentBNPCPatterns, currentNode.MappedSurfaceValue, true))
            {
                currentIntersectingNodesWithCoreValues.Add(d);

            }
        }

        if (currentIntersectingNodesWithCoreValues.Count <= 0)
        {
            currentIntersectingNodesWithCoreValues.Add(CurrentBNPCPatterns[0]);
            Debug.Log("if this shows up we have no intersectomg core patterns, just returning the first element");
        }
        return currentIntersectingNodesWithCoreValues;
    }



    private List<Dialoug> getNonIntersectingCoreValueNodesForATopic()
    {

        List<Dialoug> currentNonIntersectingNodesWithCoreValues = new List<Dialoug>();

        CurrentBNPCPatterns.Clear();
        foreach (Dialoug d in currentNode.parent.children)
        {
            //Debug.Log(d.Pattern + "::::" + d.MappedSurfaceValue);
            CurrentBNPCPatterns.Add(d);

        }
        currentNonIntersectingNodesWithCoreValues.Clear();
        if (agent.IsFatherModel)
        {
            foreach (Dialoug d in agent.FatherModel.returnIntersectingPatternNodes(CurrentBNPCPatterns, currentNode.MappedSurfaceValue, false))
            {
                //logical bug somehwre here... 
                currentNonIntersectingNodesWithCoreValues.Add(d);
               // Debug.Log(d.Pattern + "added for the sv (non intersecting)" + d.MappedSurfaceValue);

            }

        }
        else
        {
            foreach (Dialoug d in agent.MotherModel.returnIntersectingPatternNodes(CurrentBNPCPatterns, currentNode.MappedSurfaceValue, false))
            {
                //logical bug somehwre here... 
                currentNonIntersectingNodesWithCoreValues.Add(d);
                Debug.Log(d.Pattern + "added for the sv (non intersecting)" + d.MappedSurfaceValue);

            }
        }

        if (currentNonIntersectingNodesWithCoreValues.Count <= 0) //change this to something else opr don't use it via out ,ethod
        {
            Debug.Log("no nnon intersecting nodes found");
            currentNonIntersectingNodesWithCoreValues.Add(CurrentBNPCPatterns[0]);

        }
        return currentNonIntersectingNodesWithCoreValues;
    }
    private void giveMoralModelChoice()
    {
        text.text += "1." + "persuade with authority" + "\n"+  "2. Appeal to sensibility \n";
        WriteToFile("1." + "persuade with authority" + "\n" + "2. Appeal to sensibility \n");
    }

    void playerAgrees()
    {
        string[] texts = { currentNode.agreementText, currentNode.disagreementText };

        waitAndPrintAgreement(currentNode.agreementText);
    }
    private void playerDissAgrees()
    {   //for daigraming

       waitAndPrintDisagreement(currentNode.disagreementText); //currentNode.getRandomHatedFact())
                                                                                // // // addTextToTranscript(currentNode., false);

    }

    //Dialoug currentCNPCStance = new Dialoug("","", "graduate");
   

    //refactor these --- 
    private void setPlayerPattern()
    {
        text.text += " 1. I agree \n ";
        text.text += " 2. I don't agree with you there  \n";
    WriteToFile(" 1. I agree \n "+ " 2. I don't agree with you there  \n:");
 /*       text.text += "3. you know I heard other things about that cube  \n"; //pull from a list of random strings later //TODO
        text.text += "4. you know what, lets talk about something else  \n";*/
    }
    private void setPlayerPatternDissagreement() //change this 
    {
        text.text += "1 yeah I guess you are right \n ";
        text.text += " 2 I still don't agree with you therev \n";
        text.text += "3 Dish more about that cub \ne"; //pull from a list of random strings later //TODO
        text.text += "4 you know what, lets talk about something else \n";
       

    }

    private void moveConversationToSelectedTree(int treeIndex)
    {
        currentTree = allCharacterConversationsTrees[treeIndex];
        StopAllCoroutines();
       startAconversation(currentTree);
    }


    string getPlayerResponceToAflag(string surfaceValue, string arguedFlag, string rating, TypeOfPlayerTexts playerResponceType, bool isMoralARG) //change this intyo player responce
    {
        foreach (PlayerDialoug p in jsn.listOfPlayerDialougs)
        {


            if (p.playerSurfaceValue == surfaceValue && p.playerNarrativeElements.rating == rating) //prints approproate high/mid/low if its not the moral focus argument
            {
                if (!isMoralARG)
                {
                   


                }
                else 
                {
                    Debug.Log("player is refrencing NPC moral focus argument");
                    if (playerResponceType == TypeOfPlayerTexts.agreement)
                    {

                        return  p.playerNarrativeElements.playerInAgreementText;
                    }
                    else 
                    {
                     
                        if (moralFocusCounter == 0)
                        {
                            moralFocusCounter += 1;
                            return  p.playerNarrativeElements.playerMoralDisagreementText[0]; //player can agree or disagree here 

                        }
                        else
                        {
                            moralFocusCounter = 0;
                            return   p.playerNarrativeElements.playerMoralDisagreementText[1];

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
        Debug.Log(allCharacterConversationsTrees[treeCounter].root.children[0].Pattern); //psyduck 
       
        
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
            startAconversation(currentTree); //TODO add startcourutine 
            //go to another character. 
        }

        // bgCharacterNOdes.RemoveAt(i);//que and deque from a list ---OR FLAG VISITED add logic on how to choose a better character - random for now 
        return currentNode;

    }

    public int typedResult;
    bool firstConversation = true;

    void WriteToFile(string s)
    {
        string path = Application.dataPath + "/Output.txt";
        File.AppendAllText(path, s);
    }
    void initiFile()
    {
        string path = Application.dataPath + "/Output.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "logging data \n");
        }
        clearFile();
    }

    private void clearFile()
    {
        string path = Application.dataPath + "/Output.txt";

        File.AppendAllText(path, "new file!");

        using (var FileWriter = new StreamWriter(path, false))
        {
            FileWriter.WriteLine("");
        }
        UnityEditor.AssetDatabase.Refresh(); //psyduck
    }

    public void Update()
    {
        Debug.Log(text.text.Length);
        if (text.text.Length >= characterLimit)
        {
            //clearFile();
            text.text = "had to clear due to unity's charatcer limit of 65k verts"; 
            WriteToFile(text.text);
            text.text = "clearing text window and writing to file - check out file titiled output!";
        }
        if (Input.GetKeyDown(KeyCode.M)) {

            agent.ChangeModel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            WriteToFile(text.text);
            text.text = "clearing text and writing to file";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            typedResult = Convert.ToInt32(input.text);
            input.text = "";       

            if (!isInMoralModelArgumentLoop)
            {
                firstConversation = false;
                Debug.Log("not in moral model argument loop - regular loop");
                if (typedResult == 1)
                {

                    playerAgrees();
                    //prints out agreement 
                    //
                }
                else if (typedResult == 2)
                {
                    playerDissAgrees();
                    Debug.Log("!isInMoralModelArgumentLoop + typedResult== 2");
                }
            } else if (isInMoralModelArgumentLoop && !playerChoseAModel)
            {
                Debug.Log("isInMoralModelArgumentLoop && !playerChoseAModel");

                if (typedResult == 1)
                {
                    playerChoseFatherModel();
                    Debug.Log("playuer chose father model");
                }
                else if (typedResult == 2)
                {
                    playerChoseMotherModel();
                    Debug.Log("player chose mother model ");
                }
            } else if(isInMoralModelArgumentLoop && playerChoseAModel)
            {
                Debug.Log("inside options choice!");
                if (!PlayerChnagesTopic)
                {
                    switch (typedResult)
                    {

                        case (1):
                            Debug.Log("player pressed node 0");
                            Debug.Log(temporaryListOfNodes.Count);
                            PlayerDisagreedOnFlag(temporaryListOfNodes[0]);
                            break;
                        case (2):
                            Debug.Log("player pressed node 1");

                            PlayerDisagreedOnFlag(temporaryListOfNodes[1]); //TICKTICKBOOM - bug here 
                            break;
                        case (3):
                            Debug.Log("player pressed node 2");

                            PlayerDisagreedOnFlag(temporaryListOfNodes[2]);
                            break;
                        case (4):
                            PlayerSwitchesTopics();
                            break;
                        default:
                            break;
                    }
                } else
                {
                    if (typedResult <= CurrentBNPCPatterns.Count)
                    {
                        switch (typedResult)
                        {

                            case (1):

                                PlayerchangeTopic(CurrentBNPCPatterns[0]);
                                break;
                            case (2):


                                PlayerchangeTopic(CurrentBNPCPatterns[1]);
                                break;
                            case (3):


                                PlayerchangeTopic(CurrentBNPCPatterns[2]);
                                break;
                            case (4):
                                PlayerchangeTopic(CurrentBNPCPatterns[3]);

                                break;
                            case (5):
                                PlayerchangeTopic(CurrentBNPCPatterns[4]);

                                break;
                            case (6):
                                PlayerchangeTopic(CurrentBNPCPatterns[5]);

                                break;
                            case (7):
                                if (CurrentBNPCPatterns.Count <= typedResult)
                                    PlayerchangeTopic(CurrentBNPCPatterns[6]);

                                break;
                            default:
                                break;
                        }
                    }
                        

                    
                }
               

                
            }



        }



    }
    public bool PlayerChnagesTopic = false;
    
    private void PlayerSwitchesTopics()
    {
        PlayerChnagesTopic = true;
        int i = 0;
        foreach (Dialoug d in CurrentBNPCPatterns)
        {
            i++;
            text.text += i+".some transliation into topic for " + d.MappedSurfaceValue +"\n";
            WriteToFile(i + ".some transliation into topic for " + d.MappedSurfaceValue + "\n");
        }

    }

    private void displayDialougOpinion()
    {
        StopAllCoroutines();
        text.text += currentNode.mainOpinionOnAtopic +"\n";
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
                
                characterList.RemoveAt(i); therest.Add(chosenCharacter);

            }

        }
        sortedList.AddRange(priority);
        sortedList.AddRange(secondary);
        sortedList.AddRange(therest);

       // Debug.Log(sortedList.Count);
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


            }
        }
        return nodes;


    }



    private string setPlayerPattern(string key)
    {
        return key; //will add logic about current cnpc thoughts and modding this later 
    }

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
                if (flag == "MoralFocusOne")
                {
                    return op.NarrativeElements.moralFocusDisagreement[0];

                }
                if (flag == "MoralFocusTwo")
                {
                    return op.NarrativeElements.moralFocusDisagreement[1];

                }
            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;
    }

    string setNodeRating(string mappedSV)
    {
        switch (agent.ConvCharacterMoralFactors[mappedSV]) //TOFRICKENDO changet his to currentnode.sv
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
    private string setCnpcDialoug(string key, InterestingCharacters character, string flag, string mappedSV)
    {//this is hard coded for now but later send in the conversational character (talking with )
     // high - low and mid results --- cool 
        switch (agent.ConvCharacterMoralFactors[mappedSV])
        {
            case ConversationalCharacter.RatingVlaues.High:
                return returnTopicText(highVaueOpinions, key, flag, mappedSV); //make this more generic - iof/else get topic or body (startingopinion) or contrasting one... nut need logic on each case i think of this is testign a thing for now 
            case ConversationalCharacter.RatingVlaues.Mid:
                return returnTopicText(midVaueOpinions, key, flag, mappedSV);
            default://low
                return returnTopicText(lowVaueOpinions, key, flag, mappedSV);
        }
    }
    private string returnpatternToSurfaceMapiing(string pattern)// has a bestfriend---- check this me 
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




    string getIntroductionTopicString(string key, InterestingCharacters character)
    {
        //agent.ConvCharacterMoralFactors get the opinion t
        switch (key)
        {
            case ("startedAfamilyAtAyoungAge"):
                return "I heard some shapes think that " + character.fullName + " started their family way too early.";
            case ("departed"):
                return "I hear that " + character.fullName + " has left town to build a life elsewhere. Some shapes are very touchy about those who leaves their community.";
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

            case ("nepotism"):
                return "Everyone says that " + character.fullName + " got to where they are through nepotism.";
            case ("likedToExperinceCulture"):
                return "Oh yeah. If you ever had a conversation with " + character.fullName + ", you will find out how many places they’ve visited and how many different cultures they’ve experienced. They never seems to be home.";
            case ("BTrueTYourHeart"):
            case ("likesToDate"):
                return "Maybe it’s something in the air," +
                    "I feel like" + character.fullName +
                    ", like every shape in this town, is constantly either in love or chasing after someone";


            case ("socialLife"):
                return character.fullName + " has a lot of friends! They are really popular!";
            case "FriendsAreTheJoyOFlife":
                return "oh oh " + playerName + "how is  " + character.fullName + "so popular!\n they sure have a lot of friends!"; // or they have a lot of fgriends 

            case ("friendwithabestfriendsenemy"):
                return "I heard that " + character.fullName + " is friendly with their best friend’s enemy… That’s a bit awkward, to say the least.";
            case ("hasAbestFriend"):
                return "Whenever I see " + character.fullName + ", they are always hanging out with their best friend."; // or they have a lot of fgriends 

            case ("loner"):
                return character.fullName + " is always alone whenever I see them. I guess they like to keep to themselves. I heard some shapes call them a bit sus. Haha.";
            case ("hasalotofenemies"):
                return "I don’t know if " + character.fullName + " has any friends. But I do know a lot of shapes who see them as an enemy.";

            case ("MoneyMaker"):
            case ("IsWealthy"):
                return "Everyone knows that" + character.fullName + "is very wealthy.";
            case ("IsRichButNotGenrous"):
                return "Everyone knows that " + character.fullName + " is rich but is very stingy with money. Maybe that’s how they massed their fortune.";


            case ("WorksInAlcohol"):
                return "I think" + character.fullName + "sells booze for a living";
            case "Teetotasler":
                return character.fullName + "wokrs in alchohol, wonder what that field is like";

            case ("flipflop"):
                return "I feel like every time I talked to someone about  " + character.fullName + " I found out they are doing a different job.";

            case ("healerRole"):
            case ("CustodianJobs"):
                return "I think" + character.fullName + " work as " + character.Lastoccupation; ;
            case ("SupportingComunities"):
                return "text about supporting comunities..";

            case ("polluterRole"):
                return "I think" + character.fullName + "\'s job contributes to pollution\n I think they work as " + character.Lastoccupation;
            case ("Enviromentalist"):
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation;
            case ("selfMadeCube"):
            case ("selfMadeCubeByDedication"):
                return "Everyone can’t shut up about how successful and hard-working" + character.fullName + "is because they build their career from the ground up.";
            case ("riskTaker"):
            case ("LoverOfRisks"):
                return "Yeah let me tell you how much of a risktaker " + character.fullName + " is! They always take the riskest option in everything, from jobs to where they live.";

            case ("generalJobs"):
                return "I think" + character.fullName + " work as " + character.Lastoccupation; ;

            case ("advancedCareer"):
                return "I heard that  " + character.fullName + " just got a promotion. That means more money and a better title.";
            case ("hardWorker"):
                return "Everyone knows that" + character.fullName + "is a hard worker.";
            case "CarrerAboveAll":
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n moreso, I beliave that cube works so many hours! I htink they are a hardworking cube. ";
            case ("getsFiredAlot"):
                return "It’s not very kind to say this but I heard " + character.fullName + " can’t hold a job. I wonder if it’s due to their work ethics or just a lack of enthusiasm for work.";
            case ("Teachingrole"):
            case ("SchoolIsCool"):
                return "I believe" + character.fullName + "works in a school as a." + character.Lastoccupation;
            case ("butcherRole"):
                return "I think" + character.fullName + "works in a butcher shop.";
            case ("AnimalLover"):
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation + "in a farm";

            case ("MovesAlot"):
                return "Oh yeah, " + character.fullName + "moves a lot, like a lot. Some shapes call that adventurous and some call that lack of sense of belonging.";


            case ("SusMovments"):
                return "The word on the streets is that " + character.fullName + " is a bit sus. They keeps to themselves, moves a lot, and hard to talk to. You never know what they’re up to.";

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
                return "I heard that" + character.fullName + "lives and works with their family.";
            case ("hiredByAFamilymember"):
                return "I heard from someone that" + character.fullName + "got their job because of their family member";

            case ("RetiredYoung"):
                return "Yeah didn’t " + character.fullName + "retire super early? Maybe they made enough money? Or they could be trying to start something new?";//maybe add age here 
            case ("DiedBeforeRetired"):
                return "It’s not very nice to speak ill of the dead but " + character.fullName + " died before they could retire. Really makes you think about what their life could’ve been if they had done a different job or cared more about things other than work. Or maybe they liked their job and that’s what made them happy. Who can really say.";
            case ("DevorcedManyPeople"):

                return "This might be rude to say but I have lost count of how many partners" + character.fullName + "had";
            case ("marriedSomoneOlder"):
                return "they settled down with an older partner";
            case ("marriedForLifeStyleNotLove"):
                return "I hear tha" + character.fullName + "decided to settle down with a partner because of what society expects of them and not out of love.";
            case ("AdventureSeeker"):
                return "So I’ve heard that " + character.fullName + " is a thrill-seeker, they are always on the hunt for the next adventure instead of… you know... doing what other shapes do.";
            case ("liklyToHelpTheHomeless"):
                return "I bet if you asked anyone in this town, they will tell you that" + character.fullName +
                    "has a good heart. I heard they always help out those who are less fortunate";
            case ("isolated"):
                return "I know " + character.fullName + " lives alone and I’ve never seen them with anyone… I wonder what they do all day. A bit of out of the ordinary to say the least.";
            case ("WantsArtAsJob"):
            case ("ButcherButRegretful"):
                return "I heard" + character.fullName +
                    "told someone that they can’t sleep at night because of what they do… you know... killing animals. That must weighs heavily";
            case ("TooTrustingOfEnemies"):
                return character.fullName + " seems very naive and a bit too trusting… I sometimes fear that they’re too quick to trust.";
            case ("conventional"):
                return "Most shapes would describe" + character.fullName +
                    "as responsible, conventional, and a bit by-the - book or stubborn depending on who you ask.";

            case ("ArtSeller"):
            case ("doesNotGiveToThoseInNeed"):
                return "Well… this might be a bit mean to say but I hear that" + character.fullName + " is quite... independent, some people call that selfish but that’s not my word.";
            case ("supportsImmigration"):
                return "Oh yeah, " + character.fullName + " is very vocal about supporting outsiders moving and working in our city.";
            case ("reserved"):

                return "If you want to get to know " + character.fullName + ", it’s gonna take some time. They open up very slowly.";
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
                        return  p.playerNarrativeElements.playerInAgreementText;

                    }

                    else
                    {
                        return  p.playerNarrativeElements.playerDisagreementText;
                    }

                }
                else // moral 
                {
                    Debug.Log("player is refrencing NPC moral focus argument");
                    if (playerResponceType == TypeOfPlayerTexts.agreement)
                    {

                        return p.playerNarrativeElements.playerInAgreementText;
                    }
                    else //player in disagement of moral focus 
                    {
                        //special loop happens here 
                        if (moralFocusCounter == 0)
                        {
                            moralFocusCounter += 1;
                            return  p.playerNarrativeElements.playerMoralDisagreementText[0]; //player can agree or disagree here 

                        }
                        else
                        {
                            moralFocusCounter = 0;
                            return  p.playerNarrativeElements.playerMoralDisagreementText[1];

                        }

                    }

                }
            }
        }
        return "No string found!";
    }

    public void setPlayerName(Text t)
    {
        playerName = t.text;
        text.text = "thanks " + playerName + "!";
        Invoke("disableInstructionsUI", 2);
        //InstructionsUI.SetActive(false);
    }

    void disableInstructionsUI()
    {
        startTheInteraction();
    }

    private void startTheInteraction()
    {
        setUp();
    }

   


    public string getButtonTextTranslation(string pattern)
    {

        Debug.Log("called this for " + pattern);
        int r = UnityEngine.Random.Range(0, 2);
        foreach (CharacterSearchBarFacts fact in jsn.ListOfBNPCFacts)
        {
            //Debug.Log("!!!called this for " + pattern + "with the key" + fact.SearchBarKey);

            if (pattern.ToLower() == fact.SearchBarKey.ToLower())
            {
                Debug.Log("called this for " + pattern + "with the key" + fact.SearchBarKey);

                return "but" + fact.BNPCsearchTranslation[r];
            }
        }

        return "no translation found for the pattern " + pattern;
    }

    [SerializeField] bool checkCorutine;
    bool isItTypying()
    {
        return checkCorutine;
    }



    void  waitAndPrintAgreement(string t)   //so this works... but does nto when in larger scenartio.. 
    {


        if (!currentTree.FullyExplored)
        {
            printText(false, getPlayerResponce(currentNode.MappedSurfaceValue,
                                currentNode.Rating, TypeOfPlayerTexts.agreement, false));




            printText(true, t);

        } else
        {
            treeCounter++;
               currentTree = allCharacterConversationsTrees[treeCounter];
        }


        checkEndConversationAndMove();
    }

    private void checkEndConversationAndMove()
    {
      
        if (isInMoralModelArgumentLoop && !playerChoseAModel)
        {
            giveMoralModelChoice();
            // activatePlayerOptionsInButton(currentNode);
           // Debug.Log("need to check something here ----");
       
        } 
        else  if (isInMoralModelArgumentLoop && playerChoseAModel)
        {

            playerChoseAModel = false;
            giveMoralModelChoice();

        }
        else if (!currentTree.FullyExplored) //todo update this to next cnpc
        {
           startAconversation(currentTree);//at 0 
        }
        else
        { /*treeCounter++;
               currentTree = allCharacterConversationsTrees[treeCounter];*/
            converseAboutNextCharacter();//what cuases the bug --- ? works with agreeing tho .. 
        }

    }

    void testGiveInnerLoopPlayerOptions()
    {
        switch (typedResult)
        {

            case (1):
                Debug.Log("player pressed node 0");
                Debug.Log(temporaryListOfNodes.Count);
                PlayerDisagreedOnFlag(temporaryListOfNodes[0]);
                break;
            case (2):
                Debug.Log("player pressed node 1");

                PlayerDisagreedOnFlag(temporaryListOfNodes[1]);
                break;
            case (3):
                Debug.Log("player pressed node 2");

                PlayerDisagreedOnFlag(temporaryListOfNodes[2]);
                break;
            default:
                break;
        }
    }
    void  waitAndPrintMoralAreas(string t)
    {
        text.text += (t);
        cNPCHoldingStance();
    }

    void waitAndPrintDisagreement(string t)   //so this works... but does nto when in larger scenartio.. 
    { //first disagreemwent 
        if (!currentTree.FullyExplored)
        {

            bool isMf = agent.IsMoralFocus(currentNode.MappedSurfaceValue);
            text.text += "<color=orange> is this text part of the moral  focus area? " + isMf + "\n</color>";
            WriteToFile("<color=orange> is this text part of the moral  focus area? " + isMf + "\n</color>");

            //basic disagreement 1
            printText(false, getPlayerResponce(currentNode.MappedSurfaceValue, currentNode.Rating,
                                  TypeOfPlayerTexts.disagreement, isMf));

            WriteToFile(getPlayerResponce(currentNode.MappedSurfaceValue, currentNode.Rating,
                                  TypeOfPlayerTexts.disagreement, isMf));
            printText(true, t);
            WriteToFile(t);

            if (currentNode.Rating.ToLower() == "high")
            {
                string s = "";

                if (counterLoopForSchema <= 0)
                {
                    if (agent.IsFatherModel)
                    {
                        text.text += "<color=orange> what is the cnpc's stance (high/defend) (low/attack)" + agent.FatherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern) + "\n </color>";
                        WriteToFile("what is the cnpc's stance (high/defend) (low/attack)" + agent.FatherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern) + "\n ");
                        s = "using the father model  " +  agent.FatherModel.returnAppendedSchemaText(currentNode.MappedSurfaceValue, currentNode.Pattern, currentMoralModelExploredPatterns, true);
                    
                    }
                    else
                    {
                        text.text += "<color=orange> what is the cnpc's stance (high/defend) (low/attack)" + agent.MotherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern) + "\n </color>";
                        WriteToFile(" what is the cnpc's stance (high/defend) (low/attack)" + agent.MotherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern) + "\n ");

                        s = "using the nurturanParent model " + agent.MotherModel.returnAppendedSchemaText(currentNode.MappedSurfaceValue, currentNode.Pattern, currentMoralModelExploredPatterns, true);

                    }
                    counterLoopForSchema += 1;

                }
                string arg = "";

                if (agent.IsFatherModel)
                {


                    arg = "<color=red> using the father model  </color>" +agent.FatherModel.returnFatherModelFirstArgument(
                   currentNode.MappedSurfaceValue, currentNode.Pattern, currentMoralModelExploredPatterns, true);
                }
                else
                {
                    arg = "<color=blue> using the nurturanParent model </color>" + agent.MotherModel.returnNurturantModelArgumetnsText(
                  currentNode.MappedSurfaceValue, currentNode.Pattern, currentMoralModelExploredPatterns, true);
                }

                currentMoralModelExploredPatterns.Add(currentNode.Pattern);

                

                if (arg != "GenericResponceGiven")
                {
                    agent.CNPCScore += 1;
                  /*  text.text += "<color=yellow>  \n" + "current score --- player" + agent.PlayerScore + "npc score" +
                                agent.CNPCScore + " </color> \n";*/
                    //printText(true, arg);


                }
                else if (arg == "GenericResponceGiven")
                {
                   string p =  getarandomBNPCPattern();
                    arg = getGenricResponce(p,  CurrentCNPCSTANCE);
                    //printText(true, arg);

                }

                printText(true, s + "\n" + arg);
                if (agent.IsFatherModel)
                {

                    printAllCounterSrgumentsForAstance(agent.FatherModel.returnAllPossibleCounterArgumentsDebugging(getPlayerStance(), currentBNPCPatterns), CurrentCNPCSTANCE);
                    WriteToFileAllCounterSrgumentsForAstance(agent.FatherModel.returnAllPossibleCounterArgumentsDebugging(CurrentCNPCSTANCE), getPlayerStance());
                } else
                {
                    printAllCounterSrgumentsForAstance(agent.MotherModel.returnAllPossibleCounterArgumentsDebugging(getPlayerStance(), currentBNPCPatterns), CurrentCNPCSTANCE);

                    WriteToFileAllCounterSrgumentsForAstance(agent.MotherModel.returnAllPossibleCounterArgumentsDebugging(getPlayerStance()), CurrentCNPCSTANCE);

                }
                // currentMoralModelExploredPatterns.Add(currentNode.Pattern);
                isInMoralModelArgumentLoop = true;

                
             
            }


        } 
            else
            {
                treeCounter++;
                currentTree = allCharacterConversationsTrees[treeCounter];
            }
        
        checkEndConversationAndMove();

    }


    List<string> returnCurrentListOfBNPCPatternNames()
    {
        List<string> temp = new List<string>();
        foreach (Dialoug d in CurrentBNPCPatterns)
        {
            temp.Add(d.Pattern);
            
        }
        return temp;
    }



    void PlayerchangeTopic(Dialoug node)
    {
        currentNode = node;
        text.text += "player changed topics too" + node.MappedSurfaceValue;
        if (playerArguesWithFatherModel)
        {
            playerChoseFatherModel();
        } else
        {
            playerChoseMotherModel();
        }
        PlayerChnagesTopic = false;

    }

    string getPlayerStance()
    {
        if (CurrentCNPCSTANCE.ToLower() == "high")
        {
            return "low";
        }
        else
        {
            return "high";
        }
    }

    private string getarandomBNPCPattern()
    {

        int r = UnityEngine.Random.Range(0, currentBNPCPatterns.Count);
        return currentBNPCPatterns[r];
    }

    //ticktickboom

    void waitAndPrintFatherModelDissagreemnt(Dialoug node, bool playerChoseFM)   //2 // add isNPC/PLAYER? NO it's in already
    {

        //prints only player responce not npc??? 
       
/*        currentBNPCPatterns
*/        if (agent.IsFatherModel)
        {
            CurrentCNPCSTANCE = agent.FatherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern);

            if (CurrentCNPCSTANCE.ToLower() == "high")
            {
                text.text += "<color=yellow> NPC IS Defending/ likes this person</Color> using <color=red> father model</color> +\n" + "<color=yellow> Player IS Opposing BNPC </color> +\n";
                WriteToFile("<color=yellow> NPC IS Defending/ likes this person</Color> using <color=red> father model</color> +\n" + "<color=yellow> Player IS Opposing BNPC </color> +\n");
                currentPlayerStance = "low";

            }
            else
            {
                text.text += "<color=yellow> NPC IS attacking/ dislikes this person</Color> using <color=red> father model</color> +\n" + "<color=yellow> Player IS defending BNPC </color> +\n";
                currentPlayerStance = "high";
            }

        }
        else //refactor this into one method me //otter //bam
        {
            CurrentCNPCSTANCE = agent.MotherModel.returnCurrentCnpcStance(currentNode.MappedSurfaceValue, currentNode.Pattern);
          
            if (CurrentCNPCSTANCE.ToLower() == "high")
            {
                text.text += "<color=yellow> NPC IS Defending/ likes this person</Color> using <color=blue> nurturant model</color> +\n";
                WriteToFile("<color=yellow> NPC IS Defending/ likes this person</Color> using <color=blue> nurturant model</color> +\n");
                
                currentPlayerStance = "low";
            }
            else
            {
                text.text += "<color=yellow> NPC IS attacking/ dislikes this person</Color> using <color=blue> nurturant model</color> +\n";
                WriteToFile(" <color=yellow> NPC IS attacking / dislikes this person </ Color > using < color = blue > nurturant model </ color > +\n");

                currentPlayerStance = "high";

            }

        }


        if (!currentTree.FullyExplored)
        {

            bool isMf = agent.IsMoralFocus(currentNode.MappedSurfaceValue);

            //basic disagreement 1

            //refactor me and just call wait and print disagreement recursivly
            string s = "";
            if (counterLoopForSchema <= 0)
            {
                if (agent.IsFatherModel)
                {
                    s = agent.FatherModel.returnAppendedSchemaText(currentNode.MappedSurfaceValue, node.Pattern, currentMoralModelExploredPatterns_PLAYER, false);

                }
                else
                {
                    s = agent.MotherModel.returnAppendedSchemaText(currentNode.MappedSurfaceValue, node.Pattern, currentMoralModelExploredPatterns_PLAYER, false);

                }
                counterLoopForSchema += 1;
            }
            //otter - change based on if player chose father / mother model //change this!!! 


            string playerResponce = "";
           if (playerChoseFM)
            {
                playerResponce = s +  agent.FatherModel.returnFatherModelArgumetnsForAspecficString(
                 currentNode.MappedSurfaceValue, node.Pattern,
                 currentMoralModelExploredPatterns_PLAYER, currentPlayerStance);

            } else if (!playerChoseFM)
            {
                playerResponce = s + agent.MotherModel.returnNPtextForAGivenString(
               currentNode.MappedSurfaceValue, node.Pattern,
               currentMoralModelExploredPatterns_PLAYER, currentPlayerStance);
            }


            currentMoralModelExploredPatterns_PLAYER.Add(node.Pattern); //make sure it is no longer an optopn for the olayer otter

            if (playerResponce != "GenericResponceGiven")
            {
                agent.PlayerScore += 1;
                text.text += "<color=yellow>  \n" + "current score --- player" + agent.PlayerScore + " \n npc score -----" +
                            agent.CNPCScore + " </color> \n";
                WriteToFile("<color=yellow>  \n" + "current score --- player" + agent.PlayerScore + " \n npc score -----" +
                            agent.CNPCScore + " </color> \n");
                printText(false, playerResponce);
             

            }
            else if (playerResponce == "GenericResponceGiven")
            {
                playerResponce = getGenricResponce(node.Pattern, currentPlayerStance);
                printText(false, playerResponce);

            }
            //need to add a cond that returns us to this loop and check if list is empty for the qued up nodes 
            string NPCResponce = "";
            string r = " ";
            if (agent.IsFatherModel)
            {

                foreach(string n in currentMoralModelExploredPatterns)
                {
                    Debug.Log(n);
                }
                NPCResponce = agent.FatherModel.returnFatherModelArgumetnsText(
                              currentNode.MappedSurfaceValue, currentNode.Pattern,
                              currentMoralModelExploredPatterns, currentBNPCPatterns, true);

                
                /* r = NPCResponce.Split('_').Last();
                Debug.Log("this should give us a pattern " + r);
                NPCResponce = NPCResponce.Split('_').First();
*/


                Debug.Log("full npc responce" + NPCResponce);
                //currentMoralModelExploredPatterns.Add(r);

            }
            else
            {
                Debug.Log("went to mm responces");
                NPCResponce = agent.MotherModel.returnNurturantModelArgumetnsText(
              currentNode.MappedSurfaceValue, currentNode.Pattern, currentMoralModelExploredPatterns, true);
            }

            Debug.Log("NPC responce" + NPCResponce);

            if (NPCResponce != "GenericResponceGiven")
            {
                agent.CNPCScore += 1; //BUG HERE REPORT
                text.text += "<color=yellow>  \n" + "current score --- player" + agent.PlayerScore + "\n npc score" +
                    agent.CNPCScore + " </color> \n";
                    
                printText(true, NPCResponce);   

            }
            else if (NPCResponce == "GenericResponceGiven")
            {
                if (r != " ") //something buggy here.... otter
                {
                    NPCResponce = getGenricResponce(getarandomBNPCPattern(), currentPlayerStance);

                }else
                {
                    NPCResponce = getGenricResponce(getarandomBNPCPattern(), currentPlayerStance);

                }
                // currentMoralModelExploredPatterns.Add(node.Pattern);

                printText(true, NPCResponce);


            }

            // currentMoralModelExploredPatterns.Add(currentNode.Pattern);
            isInMoralModelArgumentLoop = true;

         
            innerConversationCounter += 1;
            Debug.Log("innerConversationCounter: " + innerConversationCounter);
           checkInnerConversationLoop();

        } 
            else
            {
                treeCounter++;
                currentTree = allCharacterConversationsTrees[treeCounter];
            }
        
        checkEndConversationAndMove();

    }


    string getGenricResponce(string pattern, string stance)
    {
        foreach (GenericMoralModelResponces genericText in jsn.ListOfMoralModelsGenricResponces)
        {
            if (pattern.ToLower() == genericText.key.ToLower())
            {
                int r = UnityEngine.Random.Range(0, genericText.highGnericResponces.Count);
                Debug.LogWarning(r);
                if (stance.ToLower() == "high")
                {
                    return "<color=green> the pattern " + pattern + "failed the model check and produced a generic defending statment </color>" +
                         genericText.highGnericResponces[r];
                }
                else
                {
                    return "<color=green> the pattern " + pattern + "failed the model check and produced a generic defending statment </color>" +
                     genericText.lowGnericResponces[r];

                }
            }
        }
        return "no generic responce found for the key" + pattern +  stance;
    }


    void checkInnerConversationLoop()
    {

        if (innerConversationCounter >= 2)
        {
            isInMoralModelArgumentLoop = false;
            innerConversationCounter = 0;
            counterLoopForSchema = 0;
            if (agent.PlayerScore >= agent.CNPCScore)
            {

                text.text += "\n " +  "player " + "\'" + "wins" + "\'" + "convo \n";// -- in future build start with next cnpc instead of current one/move to next convo
            }
            else
            {
                text.text += "\n " + "CNPC " + "\'" + "wins" + "\'" + "convo  \n";

            }

            resetConversationForNextCNPC();


        } else
        {
            checkEndConversationAndMove();
        }
    }
    // bdwabad //
    private void enterSubConversationOnDisagreement() //for now... 
    {
        // activateSubMenu();
    }

    private void populateDisagreementOptions()
    {
        // intersectingSFMcorePatterns.Add()


        throw new NotImplementedException();
    }


    void DisplayNodeIntroTopic(Dialoug d)
    {
        text.text += "\n " +d.UnbiasedOpeningStatment;
    

    }

    //additional debugging and fast testing methids 

    List<string> returnAllPossibleSubToSVMapping(Dialoug node)
    {
        List<string> temporarySurfaceValues = new List<string>();
        temporarySurfaceValues = SVCollection.returnCompatibleSurfaceValues(node.Pattern);
        return temporarySurfaceValues;
    }

    List<string> returnAllPossibleSVOpeningStrings(string SV)
    {
        List<string> LISToFsurfaceOpinions = new List<string>();
        string[] ratings = { "high", "mid", "low" };

        foreach (string r in ratings)
        {

            foreach (DialougStructure op in highVaueOpinions)
            {
                if (op.topic.Split('_').Last().ToLower() == SV.ToLower())
                {
                    LISToFsurfaceOpinions.Add("The SV \'" + SV + "\' with a rating of " + r + ":" + op.NarrativeElements.surfaceOpinionOnTopic);
                }
            }

        }

        return LISToFsurfaceOpinions;
    }
    void printAdditionalInformation(string s)
    {
        text.text += "<color=orange> Additional information: </color> <color=#ff6d00> " + s + "</color> \n";
    }
    void printAdditionalInformation(string[] sArray)
    {

        text.text += "<color=orange>Additional information:</color>";

        foreach (string s in sArray)
        {
            text.text += "<color=#ff6d00> [ " + s + " ]</color> ";

        }
        text.text += "\n ";

    }

    void WriteToFileAllCounterSrgumentsForAstance(List <KeyValuePair<string,string>> li, string stance)
    {
        int i = 0;
        text.text += "check out the textfile, the resultin text is too big for unity's UI!";
            
           string s =  "<color=orange>Additional information: --- NOTE: This list includes all possible responces (without BNPC intersections)  -- </color>\n";
       
            foreach (KeyValuePair<string, string> kvp in li)
            {
                i++;
                s+= "<color=#ff6d00> By using the " + i + ".  SV[" + kvp.Key + "]" + " Responce " + kvp.Value + "</color> \n";
            }
        WriteToFile(s);

    }
    void printAllCounterSrgumentsForAstance(List<KeyValuePair<string, string>> li, string stance)
    {
        int i = 0;

        string s = "<color=orange>Additional information: --- NOTE: This list includes just the intersecting patterns! for the stance  -- </color>\n "+ stance;
        string firstFive = "";
        foreach (KeyValuePair<string, string> kvp in li)
        {
            i++;
            if (i <= 5)
            {
                firstFive += "<color=#ff6d00> By using the " + i + ".  SV[" + kvp.Key + "]" + " Responce " + kvp.Value + "</color> \n";
            }
            s += "<color=#ff6d00> By using the " + i + ".  SV[" + kvp.Key + "]" + " Responce " + kvp.Value + "</color> \n";
        
        }
        if(s.Length > 30000 && text.text.Length >=30000)
        {
            text.text += "sadly printing the text here is too big! please refer to the output file! for the complete list";
            WriteToFile(s);

        } else
        {
            text.text += "printing the first five elements: refer to output file for more information \n";
            text.text += firstFive;
            WriteToFile(s);
        }
        

    }


    void printAdditionalInformation(List<string> sArray)
    {

        text.text += "<color=orange>Additional information:</color> ";
        WriteToFile("<color=orange>Additional information:</color> ");
        foreach (string s in sArray)
        {
            text.text += "<color=#ff6d00> [ " + s + " ]</color> ";
            WriteToFile("<color=#ff6d00> [ " + s + " ]</color>");
        }
        text.text += "\n ";
        WriteToFile("\n");


    }



}


/*   void activatePlayerOptionsInButton(Dialoug node) //hardcoded for now - do this for the current height of the tree --- 
    {

        if (node.getHeight() == 2)
        {
            Debug.Log("heioght is 2");


            if (!isInMoralModelArgumentLoop)
            {
                Debug.Log("not in moral model argument loop");

                setPlayerPattern();
              
                if (typedResult == 1)
                {
                    Debug.Log("wtfff");

                    //playerAgrees();
                }
                else if (typedResult == 2) { playerDissAgrees(); Debug.Log("!isInMoralModelArgumentLoop + typedResult== 3"); }
            }
            else if (isInMoralModelArgumentLoop && !playerChoseAModel)
            {
                Debug.Log("isInMoralModelArgumentLoop && !playerChoseAModel ");

                giveMoralModelChoice(); //BAM check where to actually give this option... 
                if (typedResult == 1) {
                    playerChoseFatherModel();
                    Debug.Log("isInMoralModelArgumentLoop && !playerChoseAModel +  playerChoseFatherModel(); ");
                } else if (typedResult == 2) { playerChoseMotherModel(); Debug.Log("isInMoralModelArgumentLoop && !playerChoseAModel +  mothermodel(); ");
                }
            }
            else if (isInMoralModelArgumentLoop && playerChoseAModel) // after clicking moral models...? // && !mainModelChoice otter
            {

                Debug.Log("isInMoralModelArgumentLoop && playerChoseAModel ");


                if (typedResult == 1) { playerChoseFatherModel(); } else if (typedResult == 2) { playerChoseMotherModel(); }



                int maxoptions = 2;
                temporaryListOfNodes.Clear();
                for ( int i =0; i<=2; i++)
                {
                    if (i <= maxoptions)
                    {

                        Dialoug d = currentPlayerOptionsInInnerConversation.Dequeue();
                        text.text += i.ToString() + getButtonTextTranslation(d.Pattern);
                        Debug.Log("adding node" + d.Pattern);
                        temporaryListOfNodes.Add(d);


                    }
                    switch (typedResult)
                    {
                        case (1):
                            PlayerDisagreedOnFlag(temporaryListOfNodes[0]);
                            break;
                        case (2):
                            PlayerDisagreedOnFlag(temporaryListOfNodes[1]);
                            break;
                        case (3):
                            PlayerDisagreedOnFlag(temporaryListOfNodes[2]);
                            break;
                        default:
                            break; 
                    }

                }

            }
        }

        typedResult = 0;

    }*/