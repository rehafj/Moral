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
    public List<string> contradictingValuesHigh = new List<string>
        {
            "Enviromentalist" ,"BTrueTYourHeart","AnimalLover","SchoolIsCool"
        };
    public  List<string> contradictingValuesLow = new List<string>
        {
            "schoolIsDrool","EnviromentalistAnti","AnimalLoverAnti","LoveIsForFools"
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

    [SerializeField] string moralFocus; // will equal one of the keys 

    public GameObject gameobj;



    public void Start()
    {
        gameobj = Resources.Load("prefabs/CNPCPrefab") as GameObject;
        instantiateCube();
        Debug.Log("how many times is this being called?");
    }

    public void instantiateCube()
    {
        Instantiate(gameobj, new Vector3(23.99f, -7.08f, 0f), Quaternion.identity);
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
    public RatingVlaues NpcValueFlag;
  
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


    //randomizing constructor /overloaded
    public ConversationalCharacter(string name) //method used for random
    {
        List<string> tempHighVlaue = new List<string>();//used to set up moral focus on high value flags



        ConversationalNpcName = name;
        int i = 0;

        foreach (string s in keys)
        {
            int x = Random.Range(0, 3);
            if (contradictingValuesHigh.Contains(s))
            {
               NpcValueFlag = RatingVlaues.High;

            }
            else if (contradictingValuesLow.Contains(s))
            {
                NpcValueFlag = RatingVlaues.Low;
            }
            else
            {
                NpcValueFlag = getRandomNPCValue(x);
            }

            ConvCharacterMoralFactors.Add(s, NpcValueFlag);

            if(NpcValueFlag == RatingVlaues.High) //again used for moral focus - will move this into it's opwn method soon 
            {
                tempHighVlaue.Add(s);
                
            }


            i++;
        }
        int index = UnityEngine.Random.Range(0, tempHighVlaue.Count);
        setMoralFocusArea(tempHighVlaue[index]);
        FatherModel = new StrictFatherMorality();
          Debug.Log("check these values out : EnviromentalistAnti " + ConvCharacterMoralFactors["EnviromentalistAnti"] + "and ebviromentalist" +
         ConvCharacterMoralFactors["Enviromentalist"] + "school is cool followed by drool " + ConvCharacterMoralFactors["SchoolIsCool"] + ConvCharacterMoralFactors["schoolIsDrool"]);

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

