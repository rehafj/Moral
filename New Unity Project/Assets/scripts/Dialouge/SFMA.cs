using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFMA : MonoBehaviour
{
    // Start is called before the first frame update
    //this file holds the proper structure of arguments 
    //argument 
    // ///schema
    // list of surfaceValues
    // // // // <key, list of subvalues >
    /// // the subvalues contain the key and arguments 

    public string schema; 
    public List<NewSurfaceValues> surfuceValues;

}
public class NewSurfaceValues
{
    public string surfaceValueName = "";

    public string[] surfaceKeys = { "Btruetoyourheart", "LoveIsForFools", "MoneyMaker", "Enviromentalist", "EnviromentalistAnti" 
    ,"AnimalLover","AnimalLoverAnti","Teetotasler","TeetotaslerAnti",
    "FamilyPerson", "SchoolIsCool", "schooliaDrool", "SupportingComunities", "LoverOfRisks" ,
    "LandISWhereThehrtIS", "CarrerAboveAll", "FriendsAreTheJoyOFlife", "Loner", "youthAreTheFuture" ,
       "ProHiringFamily", "suchUncharactristicBehaviorOhMy", "WeLiveForSpontaneity", "AnAdventureWeSeek", "NiaeveteIsFiction",
       "ImmagretsWeGetTheJobDone", "WeArewNothingIfWeAreNotReserved", "AselfMadeShapeWeAspireToBe", "Shapesarenothingifnotsocial", "AntiFaviortisum" };


    public List<KeyValuePair<string, newSubValues>> listOfSubvalues { get; set; }


}

public class newSubValues
{
    public KeyValuePair<string, string> subValues = new KeyValuePair<string, string>();

}

/*

public string schema = "";
public List<SurfaceValues> Surfacevalues;
}

public class SurfaceValues
{
    public List<KeyValuePair<string, SubValues>> subvalues { get; set; }

}
public class SubValues
{
    public KeyValuePair<string, string> subValues = new KeyValuePair<string, string>();
}



*/