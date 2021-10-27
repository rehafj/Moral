using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Reflection;
//loader was modfied from a video tutorial explaning the json.net asset used in this class
//found at https://www.youtube.com/watch?v=V8qf8GMT-b4&t=70s 
//helper methods and string reading is coded after the tutorial. 
//by author: first gear games. 
public class JsonLoader : MonoBehaviour
{

    //note to self: comment and uncomment below depending on what is needed in final version 

    [SerializeField]
    public  List<TownCharacter> backgroundcharacters = new List<TownCharacter>();
    [SerializeField]
    public List<Occupations> listofOccupations = new List<Occupations>();
    [SerializeField]
    public  List<TwoniePersonalities> listOfPersonalities = new List<TwoniePersonalities>();
    [SerializeField]
    public List<TownieRoutine> listOfPersonRutines = new List<TownieRoutine>();
    [SerializeField]
    public List<TownEvents> ListOfEventsInTown = new List<TownEvents>();
    [SerializeField]
    public GeneralTownInformation TownInformation = new GeneralTownInformation();
    [SerializeField]
    public List<TownPlaces> listOfTownLocations = new List<TownPlaces>();

    [SerializeField]
    public List<DialougStructure> listOfConversations = new List<DialougStructure>();


    [SerializeField]
    public List<ModelArguements> listOfArguments = new List<ModelArguements>();

    [SerializeField]
    public List<PlayerDialoug> listOfPlayerDialougs = new List<PlayerDialoug>();

    [SerializeField]
    public List<MoralModelArguments> listOffATHERArguments = new List<MoralModelArguments>(); //NEW ONE 


    [SerializeField]
    public List<CharacterSearchBarFacts> ListOfBNPCFacts = new List<CharacterSearchBarFacts>(); //NEW ONE 

    public static JsonLoader Instance { get; private set; }

    const string FILEXTEN = @".json";
    //comment and uncomment below depending on what is needed in final version 
    const string TOWNPEOPLEPATH = @"JSON/fullpeople";
    const string PERSONALITYJSONPATH = @"JSON/personalities.json";
    const string PERSONROUTINEPATH= @"JSON/routines";
    const string OCCUPATIONJSONPATH = @"JSON/Listofoccupations";
    const string EVENTSPATH = @"JSON/townevents";
    const string TOWNINFORMATION = @"JSON/townInformation";
    const string TOWNLOCATIONS = @"JSON/townPlaces";

    const string NARRATIVEPATH = @"JSON/surfaceLevelDialoug";

    const string MODELARGUEMNTS = @"JSON/newSfma"; // correct format :) - and works! 

    const string PLAYERDIALIUG = @"JSON/conversation/playerResponces";

    const string FATHERMODELARGUMENTS = @"JSON/testingnewfatherstructure";

    const string BNPCFACTS = @"JSON/BNPCInfoTranslation";




    List<string> exFlads = new List<string>(); 

    void Awake()
    {

        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //town general info
        TownInformation = returntownStructure<GeneralTownInformation>(TOWNINFORMATION);
        //character related information they all can be refrenced by id
        backgroundcharacters = returnJsonAttributesIntoList<TownCharacter>(TOWNPEOPLEPATH);//works
        listOfPersonalities = returnJsonAttributesIntoList<TwoniePersonalities>(PERSONALITYJSONPATH);
        listOfPersonRutines = returnJsonAttributesIntoList<TownieRoutine>(PERSONROUTINEPATH);
        listofOccupations = returnJsonAttributesIntoList<Occupations>(OCCUPATIONJSONPATH);
        //town related lists 
        ListOfEventsInTown = returnJsonAttributesIntoList<TownEvents>(EVENTSPATH);
        listOfTownLocations = returnJsonAttributesIntoList<TownPlaces>(TOWNLOCATIONS);

        listOfConversations = returnJsonAttributesIntoList<DialougStructure>(NARRATIVEPATH);
        listOfPlayerDialougs = returnJsonAttributesIntoList<PlayerDialoug>(PLAYERDIALIUG);
        listOfArguments = returnJsonAttributesIntoList<ModelArguements>(MODELARGUEMNTS);

        ListOfBNPCFacts = returnJsonAttributesIntoList<CharacterSearchBarFacts>(BNPCFACTS);
        listOffATHERArguments = returnJsonAttributesIntoList<MoralModelArguments>(FATHERMODELARGUMENTS);
        //FOR TESTING-REMOVE ME 
        PrintOutAConversation();

        Debug.Log(returnFatherModelArgumetnsText("BTrueTYourHeart",
            "InLovewithspouseoffriend", exFlads, true));
    }

    

    private void PrintOutAConversation()
    {

        Debug.Log(ListOfBNPCFacts.Count);
       foreach( CharacterSearchBarFacts FACT in ListOfBNPCFacts)
        {
            if(FACT.SearchBarKey == "butcherRole")
            Debug.Log(FACT.BNPCsearchTranslation[UnityEngine.Random.Range(0,2)]);
        }
        /*foreach (ModelArguements argument in listOfArguments)
        {
            Debug.Log("FFS - schema =   " + argument.schema + "surface values are " + argument.Surfacevalues[0].key  +" AND THE SUB VALUE KEY IS "+ argument.Surfacevalues[0].surfaceValueObj[0].subvalue);
         
        }

        Debug.Log("testing this out " + testingOutSomething("highMoralBoundaries", "LoveIsForFools", "WillActOnLove"));
        Debug.Log("ZZZZZplayer dialoug sample"+ getTheCorrectPlayerRespounce("MoneyMaker","High", "playerDisAgreementOnAflag", "familyPerson" ));


        //getting a player responce 
        //BTrueTYourHeart
        //rating 
        //disagreemnt 

        
*/
        string sv = "FamilyPerson";

        foreach(PlayerDialoug p in listOfPlayerDialougs)
        {
            if (p.playerSurfaceValue == sv && p.playerNarrativeElements.rating == "High" )
            {
                //Debug.Log("check me out -"+ p.playerNarrativeElements.playerInAgreementText);
            }
        }
    }
    //works - move this into father/mother and change type of strings into enums
    string getTheCorrectPlayerRespounce(string surfaceFlag, string rating, string typeOfText, string key) //chamnge type of stgring into enums and make these lists into static~ 
    {
        foreach (PlayerDialoug p in listOfPlayerDialougs)
        {
            if(p.playerSurfaceValue == surfaceFlag)
            {
                if(p.playerNarrativeElements.rating == rating)
                {
                    switch (typeOfText)
                    {
                        case ("playerInAgreementText"):
                            return
                                p.playerNarrativeElements.playerInAgreementText;
                        case ("playerDisagreementText"):
                            return
                                p.playerNarrativeElements.playerDisagreementText;
                        case ("playerMoralDisagreementText"):
                            return
                                p.playerNarrativeElements.playerMoralDisagreementText[0];
                        case ("playerMoralDisagreementTextTwo"):
                            return
                                p.playerNarrativeElements.playerMoralDisagreementText[1];
                        case ("playerDisAgreementOnAflag"):
                            foreach(PlayerDisAgreementOnAflag k in p.playerNarrativeElements.playerDisAgreementOnAflag)
                            {
                                if(k.flag == key)
                                {
                                    return k.textValue;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return "missed a flag somewhere! ";
    }
    string getTheCorrectPlayerRespounce(string surfaceFlag, string typeOfText, string rating) //retuirns agreement/disagreement 
    {
        return "";
    }
    string testingOutSomething(string schemaName, string surfuaceValueKey, string subvalueKey)
    {
        //t this works --- move this method to father model script or the model parents :) but this returns the appropriate text bad O tho
        foreach (ModelArguements argument in listOfArguments)
        {
            if(argument.schema== schemaName)
            {
                foreach(Surfacevalue S in argument.Surfacevalues)
                {
                    if(S.key == surfuaceValueKey)
                    {
                        foreach(SurfaceValueObj O in S.surfaceValueObj)
                        {
                            if(O.subvalue == subvalueKey)
                            {
                                return O.text;
                            }
                        }
                    }
                }
            }
         //   Debug.Log("FFS - schema =   " + argument.schema + "surface values are " + argument.Surfacevalues[0].key + " AND THE SUB VALUE KEY IS " + argument.Surfacevalues[0].surfaceValueObj[0].subvalue);

        }
        return "no text found!";  
    }

    List<string> NPCfmHighValues = new List<string>() //what the NPC looks for - ig its not herte NPC looks for low 
    {
        "BTrueTYourHeart",  "FamilyPerson"

    };

    List<string> NPCSVfORbOTHhIGHaNDlOW = new List<string>() { };
    // IF ITS PART THIS MIXED LIST ---> the npc looks for ohigh or low -
    //or returna  list of strings 
    // the player then only goes to generics. 
    // do this in an overloaded method 


    //make an overloaded method -- 
    string returnFatherModelArgumetnsText (string surfaceValue, string subvalue,
                                          List<string> exploredSterings,  bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 
        string NPCType = "High";

        if (isNPC)
        {
            if (NPCfmHighValues.Contains(surfaceValue))
            {
                NPCType = "High";
            }
            else
            {
                NPCType = "Low";
            }
        }

       // Debug.Log("!!!!!+ NPC WILL LOOK FOR SCHEMAS THAT HAVE" + NPCType);
        string currentPatternCheck = subvalue;

        int i = 0;
        foreach (MoralModelArguments arg in listOffATHERArguments)
        {
           // Debug.Log("arg.SVkey "+ arg.SVkey);

            if (arg.SVkey == surfaceValue) //found the sv we wanted 
            {
               // Debug.Log("INSIDE SV KEY"+ arg.SVkey);
               // Debug.Log("size of  arg.surfaceValueObjList"  + arg.SurfaceValueObject.Count);

                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    string r = sobject.schema.Split('_').First();
                    //Debug.Log("!!!!!+ r value" + r);

              /*      Debug.Log("!!!!!+ !exploredSterings.Contains(currentPatternCheck) value" +
                   !exploredSterings.Contains(currentPatternCheck)); *///after the furst time it becomes false 

                    if (sobject.subvalue == subvalue && NPCType.ToLower() == r)
                    {
                        exploredSterings.Add(subvalue);
                        return sobject.text;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck)) 
                    {
                     //   Debug.Log(" inside if it does not cvontained explorex strings !!!!!+ ! does thios happen ?  " + r + "and npc type" + NPCType.ToLower());

                        if (NPCType.ToLower() == r)
                        {
                            return sobject.text + "_" + sobject.subvalue; //else return the first thing that is high 
                        }
                        exploredSterings.Add(currentPatternCheck);

                        foreach (string s in exploredSterings)
                        {
                           // Debug.Log("!!!!!+ exploredSterings npw adds" + s);

                        }
                        i++;
                        currentPatternCheck = arg.SurfaceValueObject[i].subvalue; //slight logic bug in counter if the current checked one was in rthe middle of the list -- 
                    }

                }
            }
        }

        return "!!!will check generic series ";

    }
    string returnFile(string path)
    {
        path = removeFileEX(path);
        path = removeDirectorySeparator(path);
        if (path == string.Empty)
        {
            Debug.Log("PATH IS EMPTY");
            return "";

        }

        TextAsset textasset = Resources.Load(path) as TextAsset;
        if (textasset != null)
        {
            return textasset.text;
        }
        else
        {
            Debug.Log("it did not read it as a proper jsonfile path ");
            return "";

        }
    }


    List<T> returnJsonAttributesIntoList<T>(string path)
    {
        string result = returnFile(path);
        if (result.Length != 0)
        {
            return JsonConvert.DeserializeObject<List<T>>(result).ToList();
        }
        else
        {
            Debug.Log("resultant text is empty");
            return new List<T>();
        }

    }

    GeneralTownInformation returntownStructure<T>(string path)
    {
        string result = returnFile(path);
        if (result.Length != 0)
        {
            return JsonConvert.DeserializeObject<GeneralTownInformation>(result);
        }
        else
        {
            Debug.Log("resultant text is empty");
            return new GeneralTownInformation();
        }

    }


    string removeFileEX(string path)
    {
        if (path.ToLower().Substring(path.Length - FILEXTEN.Length, FILEXTEN.Length) == FILEXTEN.ToLower())
        {
            return path.Substring(0, path.Length - FILEXTEN.Length);
        }
        else
        {
            return path;
        }

    }

    string removeDirectorySeparator(string path)
    {
        if (char.Parse(path.Substring(0, 1)) == Path.DirectorySeparatorChar
            || char.Parse(path.Substring(0, 1)) == Path.AltDirectorySeparatorChar)
        {
            return path.Substring(1);
        }
        else
        {
            return path;
        }
    }
}




//old code for reconverting classes into objs / uses reflection, trying other approuches TBD
//  List<SFMA> sfma = new List<SFMA>();


/*   for (int i =0; i < listOfArguments.Count; i++)
   {
       Type type = listOfArguments[i].Surfacevalues.GetType();
       PropertyInfo[] properties = type.GetProperties();

       SFMA temp = new SFMA();
       temp.schema = listOfArguments[i].schema;


       for(int j = 0; j< properties.Length; j++)
       {
           NewSurfaceValues tnsv = new NewSurfaceValues();
           tnsv.surfaceValueName = properties[j].Name;
           for(int z = 0; z< 2; z++)
           {
               newSubValues tempsubvalue = new newSubValues();
               tempsubvalue.subValues = new KeyValuePair<string, string>("key", "value");
               KeyValuePair<string, newSubValues> monika = new KeyValuePair<string, newSubValues>(properties[j].Name, tempsubvalue.subValues);
               tnsv.listOfSubvalues.Add(monika);
           }
           temp.surfuceValues.Add(tnsv);
       }
       sfma.Add(temp);
       foreach(SFMA S in sfma)
       {
           *//*foreach(NewSurfaceValues d in S.surfuceValues)
           {
               Debug.Log("please god let this print it right !" + S.surfuceValues);

           }*//*

       }

     *//*  foreach (PropertyInfo property in properties)
       {
           Debug.Log("Name: " + property.Name + ", Value: " + property.GetValue(listOfArguments[0].Surfacevalues, null));

       }*//*






   }*/



/*        Type type = listOfArguments[0].Surfacevalues.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            Debug.Log("Name: " + property.Name + ", Value: " + property.GetValue(listOfArguments[0].Surfacevalues, null));
        }*/

/*
        List<SFMA> sfma = new List<SFMA>();
        List<NewSurfaceValues> newSurfaceValues = new List<NewSurfaceValues>();

        foreach (ModelArguements modelArguements in listOfArguments)
        {
            SFMA temp = new SFMA();
            temp.schema = modelArguements.schema;

            for (int i = 0; i < listOfArguments.Count; i++) //go though all the list of arguments 
            {
                NewSurfaceValues newSurfaceValue = new NewSurfaceValues();
                foreach (string s in newSurfaceValue.surfaceKeys)
                {
                }
            }



                sfma.Add(temp);
        }

        Debug.Log("new value is right here! yo "+sfma[1].schema);*/
/*      foreach ( ModelArguements s in listOfArguments) //argument includes a schema and a list of surface values values 
      {
          SFMA temp = new SFMA();
          temp.schema = s.schema;
          for(int i =0; i <listOfArguments.Count; i++)
          {
              NewSurfaceValues t2 = new NewSurfaceValues();
              for (int j = 0; j <= t2.surfaceKeys.Length ; j++)
              {
                  string x = t2.surfaceKeys[j];
                  t2.surfaceValueName = s.Surfacevalues.
                  //send it in as a key 
                //  t2.listOfSubvalues.Add(s.Surfacevalues.AnAdventureWeSeek.friendwithabestfriendsenemy);
              }


          }
          Debug.Log(s.Surfacevalues);
      }*/

//   Debug.Log(listOfArguments[0].Surfacevalues.AnAdventureWeSeek.WillActOnLove) ;
// Debug.Log(listOfArguments[0].Modelelements.arguments[0]);