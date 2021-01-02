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

    List<Tree> allCharacterConversationsTrees = new List<Tree>();


    public List<Dialoug> fullCharacterRootNodes;
    public List<Dialoug> currentPlayerOptions;

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


 
    InterestingCharacters currentIntrestingCharracter;
    List<string> currentTopicsAboutCurrentCharacter;

    string startingSceneText = "";


    //for tests 
    Tree TomsTree = new Tree();


    public void Awake()
    {

        playerName = "Rehaf";
        StartCoroutine(TypeInDialoug("hey there" + playerName +
            " \n thanks for meeting me for brunch! Boy has the town been eventful lately! "));
    }
    public void Start()
    {
        bgchar = FindObjectOfType<BackgroundCharacter>();
        jsn =FindObjectOfType<JsonLoader>();


        disableorEnablePlayerButtons();
        AllOpinions = jsn.listOfConversations;
        currentCNPC = FindObjectOfType<CharacterManager>().characters[0]; //hard coded with tim for now 
        OrgnizeCNPCOpinions();
        setUp();

       
    }




    Dialoug currentNode;
    /// new code base here 
    //////////////////////////////////////////////////////////////////////////////////////\
    ///

    public void setUp()
    {
        conversedAboutCharectersList = bgchar.GetFiltredCharerList();//fltred list of characters (with 5+ flags)
        currentIntrestingCharracter = chooseaCharacterToTalkAbout(); //random for now but pops it out of the lkist if chosen
        Dialoug node = new Dialoug("thikning about tom,","hmm I have been thinking about character X");
        TomsTree.root = node;
        Debug.Log(TomsTree.root.ButtonText);
        TomsTree.root.children = returnListOfDialougNodes(currentIntrestingCharracter);
        foreach(Dialoug s in TomsTree.root.children)
        {
            Debug.Log("current thoughts are" + s.ButtonText);
        }
        Debug.Log(TomsTree.root.children.Count);




        //chjildren all remaningoptions 
        // string chosenTopic = chooseAnInitialTopic(currentIntrestingCharracter);
        // setUprootNodes();
    }

    Coroutine activeCorutine;

    void DisplayNodeIntroTopic(Dialoug d)
    {
        StartCoroutine(TypeInDialoug(d.IntroducingATopicdialoug));

    }

   
    Dialoug choseADialougNode(List<Dialoug> bgCharacterNOdes)
    {

        int i = UnityEngine.Random.Range(0, bgCharacterNOdes.Count - 1);
        Dialoug node = bgCharacterNOdes.ElementAt(i);
        bgCharacterNOdes.RemoveAt(i);//que and deque from a list --- add logic on how to choose a better character - random for now 
        currentNode = node;
        return node;

    }
  
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(TomsTree.root.children.Count > 0)
            {//cool works!  perhjaps before popping it out of que in display intro topic have it have child nodes about that topic, don't pop it out of list fix this me! 
                DisplayplayCurrentOpinions(TomsTree); //for testing - later put all of these into a bigger tree collection
                DisplayNodeIntroTopic(choseADialougNode(TomsTree.root.children));
      
            }


        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            displayDialougOpinion();
        }
    }

    private void displayDialougOpinion()
    {
        StopAllCoroutines();
        StartCoroutine(TypeInDialoug(currentNode.dialougText));
    }

    private InterestingCharacters chooseaCharacterToTalkAbout()
    {
        int i = UnityEngine.Random.Range(0, conversedAboutCharectersList.Count - 1);
        InterestingCharacters character = conversedAboutCharectersList.ElementAt(i);
        conversedAboutCharectersList.RemoveAt(i);//que and deque from a list --- add logic on how to choose a better character - random for now 
        return character;

    }

    private List<Dialoug> returnListOfDialougNodes( InterestingCharacters character) //change name later 
    {
        List<Dialoug> nodes = new List<Dialoug>();
        foreach(KeyValuePair<string,bool> kvp in character.characterFlags)
        {
            if (kvp.Value)
            {
                Dialoug node = new Dialoug(
                   translateOpinionIntoText(kvp.Key), 
                   getInitialCNPCOpinionAsDialoug(kvp.Key, character));//general feelings about a topic 
                //--- gives the general feelings about a thing, add to the same node the pther structure elements... 

                node.IntroducingATopicdialoug =
                    getIntroductionTopicString(kvp.Key, character);

                nodes.Add(node);
            }
        }
        return nodes;

        //int i = UnityEngine.Random.Range(0, character.characterFlags.Count);

    }

    string returnIntroTopic(List<DialougStructure> opinions, string key) //the sent in list is of high./mid or low 
    {//selecting intro and the first part of the body here --- 
        foreach (DialougStructure op in opinions) //ex: all high opp
        {
            Debug.Log("-------"+ op.topic +" and the key wsas maped to" + mapToCNPCMoralFactor(key)); //error in mappuing check this out tomorrow ---- 
            if (op.topic.Contains(mapToCNPCMoralFactor(key))) //get the translatiopn of they key but not ditect character keys..... 
            {
                selectedOpnion = op.topic.Split('_').Last();
                Debug.Log("-------selectedOpnion" + selectedOpnion);

                return op.NarrativeElements.intro; 
            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;
    }



    private string getInitialCNPCOpinionAsDialoug(string key, InterestingCharacters character)
    {//this is hard coded for now but later send in the conversational character 
        Debug.Log(key);

        Debug.Log(currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)]);//returns values high/med /l;ow of a key...
        switch (currentCNPC.ConvCharacterMoralFactors[mapToCNPCMoralFactor(key)])
        {
            case ConversationalCharacter.RatingVlaues.High:
                return returnIntroTopic(highVaueOpinions, key); //make this more generic - iof/else get topic or body (startingopinion) or contrasting one... nut need logic on each case i think of this is testign a thing for now 
            case ConversationalCharacter.RatingVlaues.Mid:
                return returnIntroTopic(midVaueOpinions, key);
            default://low
                return returnIntroTopic(lowVaueOpinions, key);
        }
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
                return"Enviromentalist";
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
        Debug.Log("has this been called?");
        foreach (Text t in CNPCoptionText) //send in flags to check if player or npc and get the rioght translation out
        {
            int r = UnityEngine.Random.Range(0, currentCharacterTree.root.children.Count - 1);
            t.text = currentCharacterTree.root.children[r].ButtonText;
        }
    }

    string getIntroductionTopicString(string key, InterestingCharacters character)
    {
        switch (key)
        {
            case ("departed"):
            case "LandISWhereThehrtIS":
                return "guess what I heard! " +character.fullName + " left town!, ";
            case ("familyPerson"):
                return "hmm, did you know that  "+ character.fullName + " has a "  
                    +"family"; //to do add mcond stat for lar/small ,,,etc family 

            //add more values for the ones below --- seprate core values perhaps?mainly tags are used for flavor text but they fall for one core value 
            case ("InLovewithspouseoffriend"):
                return "I heard that " + character.fullName + " is in love with thier spouce's friend! ";
     
            case ("pregnantbutnotfromspuceorbutloveintrest"):
                return "I heard that " + character.fullName + "cheated on their siginificant cube with " + character.GetLoverName();
            case ("InLoveWirhAnothersspuce"):
                return "Not juding but I heard that " + character.fullName +  "IS IN LOVE WITH ANOTHER CUBE'S spouse " ;
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
                return  character.fullName + "'s best friend sure loves them! must be nice to have a best friend" ; // or they have a lot of fgriends 

            case ("loner"):
                return "I wonder why " +character.fullName + "lives alone...";
            case ("hasalotofenemies"):
                return "I wonder why " + character.fullName + "has alot of enemies... /// not assigned to a CNPC value yet";


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
                return "You know what I want to talk about!" + character.fullName + "\'s job! \n I think they work as " + character.Lastoccupation;

            case ("notworkingandrich"):
            case ("adultbutnotworking"):
            case ("widowedbutnotgrieving"):
            case ("exploteative"):


            case ("graduate"):
                return "NOTAUTHORED";
            default:
                return "missed a tag SOMEWHERE!" + key +"was not authored";


        }
    }

    //if occupation - then dialoug button - what baout where they work --- need to find some statments like the npc one but for players 
    //for the NPC --- 

    string translateOpinionIntoText(string key)
    {
        switch (key)
        {
            case ("departed"):
            case  "LandISWhereThehrtIS":
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
                return key +"NOT AUTHORED";
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
    }

   void disableorEnablePlayerButtons()
    {
       
        foreach( Button b in PlayerButtons)
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
                b.interactable =false;
                
            }
            else
            {
                b.interactable = true;
            }
        }
    }

    bool checkCorutine;
    IEnumerator TypeInDialoug(string dialkougText)
    {
        for (int i = 0; i<= dialkougText.Length;  i++)
        {
            isTyping = true;
            enableClick();
            GuidialougText.text = dialkougText.Substring(0, i);
             yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        enableClick();
        activeCorutine = null;
        checkCorutine = true;
    }
}

    



public class Tree
{
    public Dialoug root { get; set; }
}

