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
    public bool IsFatherModel = false;

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

    public Animator anim; 
    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        Init(setCharacterName());
    }

    public void ChangeModel()
    {
        if (IsFatherModel)
        {
            IsFatherModel = false;
        } else
        {
            IsFatherModel = true;

        }
    }
    private List<string> firstNamesList = new List<string> { "Acute", "Arc", "Conic", "Cy", "Vert", "Point", "Hex", "Polly" };
    private List<string> lastNamesList = new List<string> { "Segment", "Strip", "Gon", "Angle", "Metric", "Millimetre ", "Decimal" };

    string setCharacterName()
    {
        string name = firstNamesList[UnityEngine.Random.Range(0, firstNamesList.Count - 1)] + " " +
              lastNamesList[UnityEngine.Random.Range(0, lastNamesList.Count - 1)];

        return name;
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
        happy, sad, angry, shocked, shy, wtf, acceptanceDefeat, happyDance
    }

    public emotion CnpcEmotion;


    public enum RatingVlaues //NPC ratings for conversation basis
    {
       High, Mid, Low
    }
    public RatingVlaues NpcValueRating;
  

    List<string> tempHighVlaue = new List<string>();
  



    public void Init (string name) //method used for random
    {
       
        ConversationalNpcName = name;
 
        int i = 0;

        foreach (string s in keys)
        {
            int x = Random.Range(0, 100);

            NpcValueRating = getRandomNPCValue(x); //rating 
            if (NpcValueRating == RatingVlaues.High)
            {
                tempHighVlaue.Add(s);
            }
            ConvCharacterMoralFactors.Add(s, NpcValueRating);

            i++;
        }

        FatherModel = new StrictFatherMorality();
        MotherModel = new NurturantParentMorality();
        setContradictingValues();
        int index = UnityEngine.Random.Range(0, tempHighVlaue.Count);
        setMoralFocusArea(tempHighVlaue[index]);
        ahereToModel();


        /*Debug.Log("check these values out : EnviromentalistAnti " + ConvCharacterMoralFactors["EnviromentalistAnti"] + "and ebviromentalist" +
         ConvCharacterMoralFactors["Enviromentalist"] + "school is cool followed by drool " + ConvCharacterMoralFactors["SchoolIsCool"] + ConvCharacterMoralFactors["schoolIsDrool"]);
*/
    }

    private void ahereToModel()
    {
        int r = UnityEngine.Random.Range(0, 100);
        this.IsFatherModel = (r <= 50)?  true : false;
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
            case int n when (n <= 10):
                return RatingVlaues.Low;
            case int n when   (n > 10 && n <40):
                return RatingVlaues.Mid;
            case int n when (n >=40):
                return RatingVlaues.High;
            default:
                return RatingVlaues.High;
        }


    }

    void chooseARandomModel()
    {
        FatherModel = new StrictFatherMorality(); 
    }

    internal string getMORALfOCUSAREA()
    {
        return moralFocus;
    }

    //when we set father/mother model use the boolian value ----NOTETOSELF
   
    public  bool doesLikeBNPC(bool Defending) //appply mother model later //otter
    {
        bool likesBNPC = false;

        if (IsFatherModel && Defending)
        {
            likesBNPC = true; //i.e. high valyue 
        } else
        {
            likesBNPC = false; //i.e. low values
        }
        return likesBNPC;
    }


    public bool evaluatePlayer()
    {
        return false;

    }

    public void playAnimation(emotion emot )
    {
        switch (emot)
        {
            case (emotion.sad):
          {
            anim.Play("cube_notAgree"); //otter change me! add another animation 
            break;
                }
            case (emotion.shocked):
                {
                    anim.Play("cube_shocked");
                    break;
                }
            case (emotion.shy):
                {
                    anim.Play("Cube_shyleyGivesUp");
                    break;
                }
            case (emotion.angry):
                {
                    anim.Play("cube_Angry");
                    break;
                }
            case (emotion.wtf):
                {
                    anim.Play("cube_notAgree");
                    break;
                }
            case (emotion.acceptanceDefeat):
                {
                    anim.Play("Cube_shyleyGivesUp");
                    break;
                }
            case (emotion.happy):
                {
                    anim.Play("Cube_shyleyGivesUp");
                    break;
                }
            case (emotion.happyDance):
                {
                    anim.Play("cube_happyDance");
                    break;
                }
            default:
                anim.Play("Cube_Happy");

                break;

        }
    }

    public enum animatorFlags
    {
        isAngry, IsConceeded
    }
    public void changeConstanteMood(animatorFlags  animFlaf)
    {
        switch (animFlaf)
        {
            case (animatorFlags.isAngry)://right now angry is the only state that needs an exit flag rto return to normal 
                {
                    anim.SetBool("isAngry", true);
                    anim.SetBool("IsShocked", false);
                    anim.SetBool("isConceeded", false);
                    anim.SetBool("isSad", false);
                    break;
                }
            default:
                {
                    anim.SetBool("isAngry", false);
                    anim.SetBool("IsShocked", false);
                    anim.SetBool("isConceeded", false);
                    anim.SetBool("isSad", false);
                    break;
                }

        }
      
    }

}

