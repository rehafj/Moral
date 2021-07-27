using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SVCollection : MonoBehaviour
{

    //for mapping purposes 
   public static List<SValues> allSurfaceValues = new List<SValues>() {
    new SValues("BTrueTYourHeart", new List<string> {"InLovewithspouseoffriend", "widowedbutnotgrieving",
        "likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","marriedSomoneOlder"} ),

    new SValues("LoveIsForFools", new List<string> {"InLovewithspouseoffriend", "widowedbutnotgrieving",
        "leftFotLoveIntrest","InLoveWirhAnothersspouce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","marriedSomoneOlder"} ),

    new SValues("MoneyMaker", new List<string> { "graduate","MoneyMaker",
        "notworkingandrich","IsWealthy","exploteative"} ),

    new SValues("Enviromentalist", new List<string> {"polluterRole","Enviromentalist"} ),
    new SValues("EnviromentalistAnti", new List<string> { "IsWealthy","polluterRole"} ),

    new SValues("AnimalLover", new List<string> { "ButcherButRegretful"
        ,"butcherRole"} ),

    new SValues("AnimalLoverAnti", new List<string> {"ButcherButRegretful"
       ,"butcherRole"} ),
    new SValues("Teetotasler", new List<string> {"WorksInAlcohol","Teetotasler"} ),
    new SValues("TeetotaslerAnti", new List<string> {
        "WorksInAlcohol"} ),

    new SValues("FamilyPerson", new List<string> {"familyPerson",
        "worksWithFamily","hiredByAFamilymember",
        "conventional"} ),
    
   new SValues("schoolIsDrool", new List<string> { "graduate","adultbutnotworking",
        "notworkingandrich" } ),
    
       
   new SValues("SchoolIsCool", new List<string> {
       "graduate", "Teachingrole","hardWorker","selfMadeCubeByDedication","SchoolIsCool"} ),

   new SValues("SupportingComunities", new List<string> {"liklyToHelpTheHomeless","doesNotGiveToThoseInNeed" ,"supportsImmigration"} ),
       
   new SValues("LoverOfRisks", new List<string> {"WillActOnLove","AdventureSeeker","WorksInAlcohol","riskTaker","LoverOfRisks"} ),



   new SValues("LandISWhereThehrtIS", new List<string> {"MovesAlot","departed" } ),


   new SValues("CarrerAboveAll", new List<string> {"getsFiredAlot",
        "RetiredYoung","DiedBeforeRetired"
        ,"conventional","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication","IsRichButNotGenrous","WantsArtAsJob"} ),


    new SValues("FriendsAreTheJoyOFlife", new List<string> {"hasAbestFriend","FriendsAreTheJoyOFlife"} ),


    new SValues("Loner", new List<string> {"loner" ,"isolated", "Loner","hasalotofenemies"} ),


    new SValues("youthAreTheFuture", new List<string> {"familyPerson","DiedBeforeRetired","conventional","Teachingrole" }),
    new SValues("ProHiringFamily", new List<string> {  "worksWithFamily", "hiredByAFamilymember", "nepotism"} ),

    new SValues("WeLiveForSpontaneity", new List<string> { "InLovewithspouseoffriend","leftFotLoveIntrest","MovesAlot"} ),
    new SValues("AnAdventureWeSeek", new List<string> { "likesToDate","MovesAlot","riskTaker",   "AdventureSeeker","likedToExperinceCulture"} ),
    new SValues("NiaeveteIsFiction", new List<string> {"friendwithabestfriendsenemy", "TooTrustingOfEnemies"} ),
    new SValues("AselfMadeShapeWeAspireToBe", new List<string> {"advancedCareer","hardWorker","selfMadeCubeByDedication","selfMadeCube"} ),

    new SValues("Shapesarenothingifnotsocial", new List<string> { "socialLife"} ),

    new SValues("AntiFaviortisum", new List<string> { "hiredByAFamilymember", "conventional","nepotism" } ),

    new SValues("WeArewNothingIfWeAreNotReserved", new List<string> {  "reserved","conventional"  } ),
    new SValues("ImmagretsWeGetTheJobDone", new List<string> { "supportsImmigration" } ),
    new SValues("suchUncharactristicBehaviorOhMy", new List<string> {"SusMovments","DevorcedManyPeople","widowedbutnotgrieving","marriedSomoneOlder"} ),



    };

    public static List<string> returnCompatibleSurfaceValues(string pattern)
    {
        List<string> listOfCompatibleSV = new List<string>();
        foreach(SValues sv in allSurfaceValues)
        {
            if (sv.coreValues.Contains(pattern))
            {
                listOfCompatibleSV.Add(sv.svName);
               // Debug.Log("the pattern" + pattern + " was compatible with " + sv.svName);

            } 
        }
        if (listOfCompatibleSV.Count == 0)
        {
            listOfCompatibleSV.Add("suchUncharactristicBehaviorOhMy");
            Debug.Log("THIS PATTERN HAD NO MAPPING " + pattern);
        }
        return listOfCompatibleSV;
    }

}

public class SValues {
    public string svName;
    public List<string> coreValues = new List<string>();
    public SValues(string surfaveValueName, List<string> coreValueList)
    {
        svName = surfaveValueName;
        coreValues = coreValueList;
    }
  

    
}