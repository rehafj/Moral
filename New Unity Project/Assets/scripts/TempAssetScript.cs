using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class DeserlizingJson : MonoBehaviour
{ }
    // --- unused

/*    public static string LoadJsonAsResource(string path)//returns the text file... 
    {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonFile.text;
    }*/
   /* string path;
    public void Awake()
    {

        *//*        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        *//*
        path = Application.streamingAssetsPath + "/town.json";
        Debug.Log(path);
        TalkOfTheTown town = JsonConvert.DeserializeObject<TalkOfTheTown>(File.ReadAllText(path));
        Debug.Log(town);

        using (StreamReader file = File.OpenText(path))
        {

            JsonSerializer serializer = new JsonSerializer();
            TalkOfTheTown town2 = (TalkOfTheTown)serializer.Deserialize(file, typeof(TalkOfTheTown));
            Debug.Log(town2);
        }

    }
}

    //Town t = JsonConvert.DeserializeObject<Town>(json);
    //Debug.Log();
    //Console.WriteLine(account.Email);
    [System.Serializable]
    public class TalkOfTheTown
    {
        public Town town { get; set; }
        public Dictionary<string, EventObjects> events { get; set; }
    }





    // the right structure ---- testing! 
    [System.Serializable]
    public class Town
    {
        public string name { get; set; }
        public Dictionary<string, Places> places { get; set; }
        public Dictionary<string, People> people { get; set; }

        //  public Places places { get; set; }//        public List<Places> places { get; set; }
       // public People poeple { get; set; }//not a list?
        public int[] settelers { get; set; }
        public int[] dceased { get; set; }
        public int[] companies { get; set; }
        public int[] formerCompanies { get; set; }
        public int[] cemetery { get; set; }

    }
    [System.Serializable]
    public class People
    {
        Dictionary<string, PeopleAttributes> attributes { get; set; }
    }
    [System.Serializable]
    public class PeopleAttributes
    {
        int id;
        string type;
        string town;
        int biologicalMotherID;
        int motherID;//-1 if null 
        int biologicalFatherID;
        int fatherID;
        int[] parentsID;//id of parents 
        int age;
        bool adult;
        bool inWorkForce;
        bool isMale;
        bool isFemale;
        bool isAlive;
        bool iSattractedToMen;
        bool iSattractedToWomen;
        Personality personality;
        struct routine
        {
            int personID;
            bool working;
            string occasion;
        }
        string firstName;
        string lastName;
        int[] immediateFamilyId;
        int[] grandparentsId;
        int[] auntsId;
        int[] unclesId;
        int[] siblingsId;
        int[] kids;
        int[] bioParents;
        int[] bioImmediateFamily;
        int spouse;
        bool widowed;
        int[] sexualPartnersid;
        int[] acquaintancesId;
        int[] friendsId;
        int[] enemiesId;
        int[] neighborsID;
        int[] coworkersId;
        int[] formerCoworkersID;
        int bestFriendiD;
        int worstEnemyID;
        int loveInterest;
        int significantOther;
        double chargeOfBestFriend;
        double chargeOfWorstEnemy;
        double sparkOfLoveInterest;
        bool isPregnant;
        int impregnatedById;
        int adoptionEventId;
        int marriageEventId;
        int[] marriagesEventId;
        int[] divorcesEventId;
        int[] adoptionsEventId;
        int[] movesEventId;
        int[] layOffsEventId;
        int[] nameChangesEventId;
        int[] buildingCommissionsEventId;
        int[] homePurchasesEventId;
        int retirementEventId;//-1 = null 
        int departureEventID;
        int death;
        double money;
        Occuptions[] occuptions;
        // int[] occuptions;//rework this --- 
        bool isRetired;
        bool isCollegeGraduate;
        bool grieving;
        bool weddingRingOnFinger;

    }
    [System.Serializable]
    public class Occuptions
    {
        string type;
        int personID;
        int companyID;
        string shift;
        int hiring;//-1 iof noty 
        int precededBy;
        int succeededBy;
        bool isSupplemental;
        bool hiredAsFavor;
        string vocation;
        int level;
    }
    [System.Serializable]
    public class Personality
    {
        int personID;
        double opennessToExperience;
        double conscientiousness;
        double extroversion;
        double agreeableness;
        double neuroticism;
        double interestInHistor;
        bool highOpennes;
        bool lowOpenness;
        bool highConscientiousness;
        bool lowConscientiousness;
        bool highEextroversion;
        bool lowExtroversion;
        bool highAgreeableness;
        bool lowAgreeableness;
        bool highNeuroticism;
        bool lowNeuroticism;
    }

    [System.Serializable]
    public class Places
    {
        //int id;
        // List<PlaceObjects> objects;
        //loop this and make a dictionary with ID and list of objects --- or set it 
        Dictionary<int, PlaceObjects> attributes { get; set; }

    }
    [System.Serializable]
    public class PlaceObjects
    {
        int id { get; set; }
        string type { get; set; }
        string[] services { get; set; }
        string town { get; set; }
        int[] employees { get; set; }
        int[] formerEmployees { get; set; }
        int[] formerOwners { get; set; }
        string name { get; set; }
        int[] peopleHereNow { get; set; }
        bool iSoutOfBusiness { get; set; }



    }
    [System.Serializable]
    public class Events
    {
        Dictionary<string, EventObjects> events { get; set; }

    }
    [System.Serializable]
    public class EventObjects
    {
        int event_id { get; set; }
        string type { get; set; }//maybe this should be its ownb class as event type 
        int subject { get; set; }

    }
*/

/*
 * 
 * 
    class face
    {
        int person;
        distinctiveFeatures features;

    }
    class distinctiveFeatures
    {

    }public class Settlers
    {

    }
    public class Deceased
    {

    }
    public class Companies
    {

    }
    public class FormerCompanies
    {

    }
    public class Cemetery
    {

    }*/


/* struct supplemental_vacancies //check if these are arrays of ints 
 {
     int[] day;
     int[] night;
 };
 int[] supplementalvacancies;
 int construction;
 string address;
 int housenumber;
 int streetaddressison;
 string name;
 int[] peopleherenow;
 bool outofbusiness;
 int closure;
 int closed; */