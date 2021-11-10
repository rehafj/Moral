using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ConversationalCharacter : MonoBehaviour
{
    //make these random as long as two contradicting pairs do not get put in the same list     /// <summary>
    /// you can add contradicitons to vlaues in these lists, High contains high values and low contains low values that are contradictiory to High
    /// </summary>

    public List<string> listOfContradictions = new List<string> {
                     "schoolIsDrool","EnviromentalistAnti","AnimalLoverAnti","LoveIsForFools","Loner","AntiFaviortisum ","WeArewNothingIfWeAreNotReserved","TeetotaslerAnti"

       , "Enviromentalist" ,"BTrueTYourHeart","AnimalLover","SchoolIsCool","Shapesarenothingifnotsocial","ProHiringFamily ","FriendsAreTheJoyOFlife","Teetotasler"
        };

    public List<string> contradictingValuesHigh = new List<string>
        {
            "Enviromentalist" ,"BTrueTYourHeart","AnimalLover","SchoolIsCool","Shapesarenothingifnotsocial","ProHiringFamily ","FriendsAreTheJoyOFlife","Teetotasler"
        };
    public  List<string> contradictingValuesLow = new List<string>
        {
            "schoolIsDrool","EnviromentalistAnti","AnimalLoverAnti","LoveIsForFools","Loner","AntiFaviortisum ","WeArewNothingIfWeAreNotReserved","TeetotaslerAnti"
        };
    public Dictionary<string, RatingVlaues> ConvCharacterMoralFactors = new Dictionary<string, RatingVlaues>();
    //att 2 --- 
    string[] keys = { "BTrueTYourHeart", "LoveIsForFools","MoneyMaker",
        "Enviromentalist", "EnviromentalistAnti","AnimalLover",
        "AnimalLoverAnti",
    "Teetotasler","TeetotaslerAnti","FamilyPerson"  ,
        "SchoolIsCool","youthAreTheFuture","schoolIsDrool",
        "LoverOfRisks"   ,"ProHiringFamily"
            ,"SupportingComunities", "LandISWhereThehrtIS","CarrerAboveAll",
        "suchUncharactristicBehaviorOhMy","WeLiveForSpontaneity","AnAdventureWeSeek",
        "NiaeveteIsFiction",
        "FriendsAreTheJoyOFlife","ImmagretsWeGetTheJobDone","WeArewNothingIfWeAreNotReserved"
            ,"AselfMadeShapeWeAspireToBe","Shapesarenothingifnotsocial","AntiFaviortisum",
        "Loner", };//removed trbd for now //29 total surface values 

    public StrictFatherMorality FatherModel ;
    public NurturantParentMorality MotherModel;

    public string ConversationalNpcName;

    public int CNPCScore = 0;
    public int PlayerScore = 0;


    [SerializeField] string moralFocus; // will equal one of the keys 

    public GameObject NPCCubeObject;
    public  NavigationControl agentPrefabControl;


    public void Awake()
    { 
    }

   
    public void setMoralFocusArea(string key)
    {
        moralFocus = key;
    }

    public bool IsMoralFocus(string flag)
    {
        if (moralFocus == flag)
        {
            return true;
        }
        else return false;
    } 
    public enum emotion
    {
        happy, sad, angry, shocked, shy
    }
    public emotion CnpcEmotion;
    public enum RatingVlaues //NPC ratings for conversation basis
    {
       High, Mid, Low
    }
    public RatingVlaues NpcValueRating;
  
    public ConversationalCharacter(string name ,RatingVlaues[] thirteenValues )
    {
        ConversationalNpcName = name;
        int i = 0;
      
        foreach(string s in keys)
        {
            ConvCharacterMoralFactors.Add(s, thirteenValues[i]);
            
            i++;
        }

        FatherModel = new StrictFatherMorality();
    }

    public ConversationalCharacter(string name, RatingVlaues[] thirteenValues, string FocusArea)
    {
        ConversationalNpcName = name;
        int i = 0;

        foreach (string s in keys)
        {
            ConvCharacterMoralFactors.Add(s, thirteenValues[i]);

            i++;
        }

        moralFocus = FocusArea;
        FatherModel = new StrictFatherMorality();

    }

    List<string> tempHighVlaue = new List<string>();
    //randomizing constructor /overloaded
    public ConversationalCharacter(string name) //method used for random
    {
      //  //used to set up moral focus on high value flags

        ConversationalNpcName = name;
        int i = 0;

        foreach (string s in keys)
        {
            int x = Random.Range(0, 3);
           
            NpcValueRating = getRandomNPCValue(x); //rating 
            if (NpcValueRating == RatingVlaues.High )
            {
                tempHighVlaue.Add(s);
            }
            ConvCharacterMoralFactors.Add(s, NpcValueRating);

            i++;
        }
      
        FatherModel = new StrictFatherMorality();
        setContradictingValues();
        int index = UnityEngine.Random.Range(0, tempHighVlaue.Count);
        setMoralFocusArea(tempHighVlaue[index]);
        Debug.Log("check these values out : EnviromentalistAnti " + ConvCharacterMoralFactors["EnviromentalistAnti"] + "and ebviromentalist" +
         ConvCharacterMoralFactors["Enviromentalist"] + "school is cool followed by drool " + ConvCharacterMoralFactors["SchoolIsCool"] + ConvCharacterMoralFactors["schoolIsDrool"]);

    }

    void setContradictingValues()
    {
        List<string> ourkeys = new List<string>(ConvCharacterMoralFactors.Keys);
        foreach(string s in ourkeys)
        {
            if (ConvCharacterMoralFactors[s] == RatingVlaues.High && listOfContradictions.Contains(s))
            {
                ConvCharacterMoralFactors[getContradictingString(s)] = RatingVlaues.Low;
            }


        }

        /*
         * 
         *     if (contradictingValuesHigh.Contains(s) && ConvCharacterMoralFactors[s] == RatingVlaues.High)
                    {
                        ConvCharacterMoralFactors[getContradictingString(s)] = RatingVlaues.Low; // this would still result in 
                        Debug.Log("the thing that changed was " + s + "this was changed: ConvCharacterMoralFactors[" + getContradictingString(s) + "]:" + ConvCharacterMoralFactors[getContradictingString(s)]);

                        tempHighVlaue.Remove(s);
                    }
                    else if (contradictingValuesLow.Contains(s) && ConvCharacterMoralFactors[s] == RatingVlaues.Low)
                    {
                        ConvCharacterMoralFactors[getContradictingString(s)] = RatingVlaues.High;
                        Debug.Log("the thing that changed was " + s + "this was changed: ConvCharacterMoralFactors[" + getContradictingString(s) + "]:" + ConvCharacterMoralFactors[getContradictingString(s)]);

                        tempHighVlaue.Add(s);
                    }
         * 
         * 
                foreach (KeyValuePair<string, RatingVlaues> k  in ConvCharacterMoralFactors)
                {
                    if (contradictingValuesHigh.Contains(k.Key) && k.Value == RatingVlaues.High)//loveisgood
                    {
                        ConvCharacterMoralFactors[getContradictingString(k.Key)] = RatingVlaues.Low;
                        tempHighVlaue.Add(k.Key);
                    }
                }*/


    }

    string getContradictingString(string s)
    {
        switch (s) //make this into a four loop --- agh this is bad
        {
            case ("Enviromentalist"):
                return "EnviromentalistAnti";
            case ("BTrueTYourHeart"):
                return "LoveIsForFools";
            case ("AnimalLover"):
                return "AnimalLoverAnti";
            case ("SchoolIsCool"):
                return "schoolIsDrool";
            case ("Shapesarenothingifnotsocial"):
                return "Loner";
            case ("ProHiringFamily"):
                return "AntiFaviortisum";
            case ("FriendsAreTheJoyOFlife"):
                return "WeArewNothingIfWeAreNotReserved";
            case ("Teetotasler"):
                return "TeetotaslerAnti";
            case ("EnviromentalistAnti"): 
                return "Enviromentalist"; 
            case ("LoveIsForFools"):
                return "BTrueTYourHear";
            case ("AnimalLoverAnti"):
                return "AnimalLover";
            case ("schoolIsDrool"):
                return "SchoolIsCool";
            case ("Loner"):
                return "Shapesarenothingifnotsocial";
            case ("AntiFaviortisum"):
                return "ProHiringFamily";
            case ("WeArewNothingIfWeAreNotReserved"):
                return "FriendsAreTheJoyOFlife";
            case ("TeetotaslerAnti"):
                return "Teetotasler";
            default:
                return "ERROR";
        }
    }

    private RatingVlaues getRandomNPCValue(int x )
    {
       
        switch (x)
        {
            case (0):
                return RatingVlaues.Low;
            case (1): return RatingVlaues.Mid;
            default:
                return RatingVlaues.High;
        }
    }

    void chooseARandomModel()
    {
        FatherModel = new StrictFatherMorality(); // make it on a precentage but for now I do not have a mother model implemented 
    }

    internal string getMORALfOCUSAREA()
    {
        return moralFocus;
    }

   
    
}

