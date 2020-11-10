using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using jsonnet

public class DeserlizingJson : MonoBehaviour
{


    //Town t = JsonConvert.DeserializeObject<Town>(json);

    //Debug.Log();
    //Console.WriteLine(account.Email);

    public class Town
    {
        public string name { get; set; }
        public int founded { get; set; }
        public List<Places> places;
        public IList<string> Roles { get; set; }
    }

    public class Places
    {
        int id;
        List<PlaceObjects> objects;
        //loop this and make a dictionary with ID and list of objects --- 
    }

    public class PlaceObjects
    {

        int id;
        string type;
        int demise;
        string[] services;
        string town;
        int founded;
        int[] employees;
        int[] former_employees;
        int[] former_owners;
        struct supplemental_Vacancies //check if these are arrays of ints 
        {
            int[] day;
            int[] night;
        };
        int[] supplementalVacancies;
        int construction;
        string address;
        int houseNumber;
        int streetAddressIsOn;
        string name;
        int[] peopleHereNow;
        bool outOfBusiness;
        int closure;
        int closed;

    }

    public class people
    {
        int id;
        peopleAttribute attributes;
    }

    class peopleAttribute
    {
        int id;
        string type;
        int birth;
        string town;
        int motherID;
        int mother;
        int datherID;
        int[] parentIDs;
        int birthday;
        int birthmonth;
        int birthyear;
        int age;
        bool workForce;
        bool male;
        bool female;
        string tag;
        bool alive;
        int deathYear;
        int gravestone;
        int home;
        bool infertle;
        bool attractedToMen;
        bool attractedToWomen;


    }

    class face
    {
        int person;
        distinctiveFeatures features;

    }
    class distinctiveFeatures
    {

    }
    public class Events
    {

    }

}
