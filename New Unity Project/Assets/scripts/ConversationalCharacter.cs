using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationalCharacter : MonoBehaviour

  
{
    //make these random as long as two contradicting pairs do not get put in the same list 
   public  List<string> contradictingValuesHigh = new List<string>
        {
            "Enviromentalist" ,"BTrueTYourHeart","AnimalLover","SchoolIsCool"
        };/// <summary>
        /// you can add contradicitons to vlaues in these lists, High contains high values and low contains low values that are contradictiory to High
        /// </summary>
   public  List<string> contradictingValuesLow = new List<string>
        {
            "schoolIsDrool","EnviromentalistAnti","AnimalLoverAnti","LoveIsForFools"
        };


    public Dictionary<string, RatingVlaues> ConvCharacterMoralFactors = new Dictionary<string, RatingVlaues>();
    //att 2 --- 
    string[] keys = { "BTrueTYourHeart", "LoveIsForFools","MoneyMaker", "Enviromentalist", "EnviromentalistAnti","AnimalLover","AnimalLoverAnti",
    "Teetotasler","TeetotaslerAnti","FamilyPerson"  ,"SchoolIsCool","youthAreTheFuture","schoolIsDrool","LoverOfRisks"   ,"ProHiringFamily"
            ,"SupportingComunities", "LandISWhereThehrtIS","CarrerAboveAll", "suchUncharactristicBehaviorOhMy","WeLiveForSpontaneity","AnAdventureWeSeek","NiaeveteIsFiction",
        "FriendsAreTheJoyOFlife","ImmagretsWeGetTheJobDone","WeArewNothingIfWeAreNotReserved","AselfMadeShapeWeAspireToBe","Shapesarenothingifnotsocial","AntiFaviortisum",
        "Loner", };//removed trbd for now //29 total surface values 

    public StrictFatherMorality FatherModel ;
    public NurturantParentMorality MotherModel;

    public string ConversationalNpcName;

    [SerializeField] string moralFocus; // will equal one of the keys 

    
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
    public enum RatingVlaues
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



}



/*//// { "BTrueTYourHeart", "LoveIsForFools","MoneyMaker", "Enviromentalist", "EnviromentalistAnti","AnimalLover","AnimalLoverAnti",
"Teetotasler","TeetotaslerAnti","FamilyPerson"  ,"SchoolIsCool","youthAreTheFuture","schoolIsDrool","LoverOfRisks"   ,"ProHiringFamily"
            ,"SupportingComunities", "LandISWhereThehrtIS","CarrerAboveAll", "suchUncharactristicBehaviorOhMy","WeLiveForSpontaneity","AnAdventureWeSeek","NiaeveteIsFiction",
        "FriendsAreTheJoyOFlife","ImmagretsWeGetTheJobDone","WeArewNothingIfWeAreNotReserved","AselfMadeShapeWeAspireToBe","Shapesarenothingifnotsocial","AntiFaviortisum",
        "Loner", };/*/
/*
 * 
 * 
 * lis{"BTrueTYourHeart", 0 },
        {"MoneyMaker", 0 },
        {"Enviromentalist", 0 },
        {"AnimalLover", 0 },
        {"Teetotasler", 0 },
        {"BeKind", 0 },
        {"SchoolIsCool", 0 },
        {"LoverOfRisks", 0 },
        {"SupportingComunities", 0 },
        {"LandISWhereThehrtIS", 0 },
        {"CarrerAboveAll", 0 },
        {"FriendsAreTheJoyOFlife", 0 },
        {"Loner", 0 }
 * 
 * 
 * 
 * 
 * 
 *   foreach (string s in ConvCharacterMoralFactors.Keys)
        {
            ConvCharacterMoralFactors[s] = thirteenValues[i];
            Debug.Log("???+ " + ConvCharacterMoralFactors[s]);
            i++;
        }
 * 
 * 
 * 
 * 
 * ConvCharacterMoralFactors.Add("BTrueTYourHeart", 0);
ConvCharacterMoralFactors.Add("MoneyMaker", 0);
ConvCharacterMoralFactors.Add("Enviromentalist", 0);
ConvCharacterMoralFactors.Add("AnimalLover", 0);
ConvCharacterMoralFactors.Add("Teetotasler", 0);
ConvCharacterMoralFactors.Add("BeKind", 0);
ConvCharacterMoralFactors.Add("SchoolIsCool", 0);
ConvCharacterMoralFactors.Add("LoverOfRisks", 0);
ConvCharacterMoralFactors.Add("SupportingComunities", 0);
ConvCharacterMoralFactors.Add("LandISWhereThehrtIS", 0);
ConvCharacterMoralFactors.Add("CarrerAboveAll", 0);
ConvCharacterMoralFactors.Add("FriendsAreTheJoyOFlife", 0);
ConvCharacterMoralFactors.Add("Loner", 0);*/


/* public void checkContradictionsForSurfaceValues(string s )
    {

        /*   switch (s)
           {
               case ("Enviromentalist"):
                   if(k.Value == RatingVlaues.High)
                   {
                       ConvCharacterMoralFactors["EnviromentalistAnti"] = RatingVlaues.Low;
                   }else
                   {
                       ConvCharacterMoralFactors["EnviromentalistAnti"] = RatingVlaues.High;
                   } break;

               case ("LoveIsForFools"):
                   if (k.Value == RatingVlaues.High)
                   {
                       ConvCharacterMoralFactors["BTrueTYourHeart"] = RatingVlaues.Low;
                   }
                   else
                   {
                       ConvCharacterMoralFactors["BTrueTYourHeart"] = RatingVlaues.High;
                   }break;
               case ("AnimalLover"):
                   if (k.Value == RatingVlaues.High)
                   {
                       ConvCharacterMoralFactors["AnimalLoverAnti"] = RatingVlaues.Low;
                   }
                   else
                   {
                       ConvCharacterMoralFactors["AnimalLoverAnti"] = RatingVlaues.High;
                   } break;
               case ("Teetotasler"):
                   if (k.Value == RatingVlaues.High)
                   {
                       ConvCharacterMoralFactors["EnviromentalistAnti"] = RatingVlaues.Low;
                   }
                   else
                   {
                       ConvCharacterMoralFactors["EnviromentalistAnti"] = RatingVlaues.High;
                   }break;
               case ("SchoolIsCoolschoolIsDrool"):
                   if (k.Value == RatingVlaues.High)
                   {
                       ConvCharacterMoralFactors["schoolIsDrool","EnviromentalistAnti","AnimalLoverAnti",LoveIsForFools] = RatingVlaues.Low;
                   }
                   else
                   {
                       ConvCharacterMoralFactors["schoolIsDrool"] = RatingVlaues.High;
                   } break;*/

        // } */
    
    /*  Debug.Log("check these values out : EnviromentalistAnti " + ConvCharacterMoralFactors["EnviromentalistAnti"] + "and ebviromentalist" +
          ConvCharacterMoralFactors["Enviromentalist"] + "school is cool followed by drool " + ConvCharacterMoralFactors["SchoolIsCool"] + ConvCharacterMoralFactors["schoolIsDrool"]);*/
    