using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;


//loader was modfied from a video tutorial explaning the json.net asset used in this class
//found at https://www.youtube.com/watch?v=V8qf8GMT-b4&t=70s 
//helper methods and string reading is coded after the tutorial. 
//by author: first gear games. 
public class JsonLoader : MonoBehaviour
{
    //note to self: comment and uncomment below depending on what is needed in final version 

    [SerializeField]
    private List<TownCharacter> backgroundcharacters = new List<TownCharacter>();
    [SerializeField]
    private List<Occupations> listofOccupations = new List<Occupations>();
    [SerializeField]
    private List<TwoniePersonalities> listOfPersonalities = new List<TwoniePersonalities>();
    [SerializeField]
    private List<TownieRoutine> listOfPersonRutines = new List<TownieRoutine>();
    [SerializeField]
    private List<TownEvents> ListOfEventsInTown = new List<TownEvents>();
    [SerializeField]
    private GeneralTownInformation TownInformation = new GeneralTownInformation();
    [SerializeField]
    private List<TownPlaces> listOfTownLocations = new List<TownPlaces>();

    const string FILEXTEN = @".json";
    //comment and uncomment below depending on what is needed in final version 
    const string TOWNPEOPLEPATH = @"JSON/fullpeople";
    const string PERSONALITYJSONPATH = @"JSON/personalities.json";
    const string PERSONROUTINEPATH= @"JSON/routines";
    const string OCCUPATIONJSONPATH = @"JSON/Listofoccupations";
    const string EVENTSPATH = @"JSON/townevents";
    const string TOWNINFORMATION = @"JSON/townInformation";
    const string TOWNLOCATIONS = @"JSON/townPlaces";

//working poeple --- is the teesting file used :) 




    void Awake()
    {
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

