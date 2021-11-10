using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SVCollection : MonoBehaviour
{

    //for mapping purposes 
   public static List<SValues> allSurfaceValues = new List<SValues>() {

       new SValues("BTrueTYourHeart", new List<string> {"InLovewithspouseoffriend",
"widowedbutnotgrieving","likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
"startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove","WillActOnLove","marriedSomoneOlder","BTrueTYourHeart"} ),
new SValues("LoveIsForFools", new List<string> {"InLovewithspouseoffriend",
"widowedbutnotgrieving","likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
"startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove","WillActOnLove","marriedSomoneOlder","LoveIsForFools"} ),
new SValues("MoneyMaker", new List<string> {"graduate","notworkingandrich","IsWealthy","exploteative","adultbutnotworking","hardWorker","selfMadeCubeByDedication","getsFiredAlot","RetiredYoung","DiedBeforeRetired","flipflop","advancedCareer","IsRichButNotGenrous","MoneyMaker"} ),
new SValues("Enviromentalist", new List<string> {"polluterRole","conventional","Enviromentalist"} ),
new SValues("EnviromentalistAnti", new List<string> {"polluterRole","conventional","EnviromentalistAnti"} ),
new SValues("AnimalLover", new List<string> {"ButcherButRegretful","butcherRole","conventional","AnimalLover"} ),
new SValues("AnimalLoverAnti", new List<string> {"ButcherButRegretful","butcherRole","conventional","AnimalLoverAnti"} ),
new SValues("Teetotasler", new List<string> {"WorksInAlcohol","Teetotasler"} ),
new SValues("TeetotaslerAnti", new List<string> {"WorksInAlcohol","TeetotaslerAnti"} ),
new SValues("FamilyPerson", new List<string> {"leftFotLoveIntrest","nepotism","marriedSomoneOlder","startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove","widowedbutnotgrieving","familyPerson","worksWithFamily","hiredByAFamilymember","conventional","FamilyPerson"} ),
new SValues("schoolIsDrool", new List<string> {"graduate","Teachingrole","schoolIsDrool"} ),
new SValues("SchoolIsCool", new List<string> {"graduate","Teachingrole","SchoolIsCool"} ),
new SValues("SupportingComunities", new List<string> {"liklyToHelpTheHomeless","doesNotGiveToThoseInNeed","supportsImmigration","MovesAlot","IsRichButNotGenrous","departed","SupportingComunities"} ),
new SValues("LoverOfRisks", new List<string> {"likesToDate","WillActOnLove","AdventureSeeker","riskTaker","LoverOfRisks"} ),
new SValues("LandISWhereThehrtIS", new List<string> {"AdventureSeeker","MovesAlot","likedToExperinceCulture","SusMovments","departed","LandISWhereThehrtIS"} ),
new SValues("CarrerAboveAll", new List<string> {"polluterRole","WorksInAlcohol","conventional","adultbutnotworking","Teachingrole","hardWorker","selfMadeCubeByDedication","riskTaker","getsFiredAlot","RetiredYoung","DiedBeforeRetired","flipflop","advancedCareer","CustodianJobs","generalJobs","IsRichButNotGenrous","CarrerAboveAll"} ),
new SValues("FriendsAreTheJoyOFlife", new List<string> {"hasAbestFriend","loner","isolated","hasalotofenemies","socialLife","reserved","FriendsAreTheJoyOFlife"} ),
new SValues("Loner", new List<string> {"hasAbestFriend","loner","isolated","hasalotofenemies","socialLife","reserved","Loner"} ),
new SValues("youthAreTheFuture", new List<string> {"familyPerson","Teachingrole","youthAreTheFuture"} ),
new SValues("ProHiringFamily", new List<string> {"exploteative","worksWithFamily","hiredByAFamilymember","selfMadeCubeByDedication","nepotism","ProHiringFamily"} ),
new SValues("WeLiveForSpontaneity", new List<string> {"InLovewithspouseoffriend","likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce","WillActOnLove","AdventureSeeker","riskTaker","MovesAlot","DiedBeforeRetired","flipflop","likedToExperinceCulture","departed","WeLiveForSpontaneity"} ),
new SValues("AnAdventureWeSeek", new List<string> {"adultbutnotworking","AdventureSeeker","riskTaker","MovesAlot","DiedBeforeRetired","likedToExperinceCulture","departed","AnAdventureWeSeek"} ),
new SValues("NiaeveteIsFiction", new List<string> {"exploteative","friendwithabestfriendsenemy","TooTrustingOfEnemies","SusMovments","NiaeveteIsFiction"} ),
new SValues("AselfMadeShapeWeAspireToBe", new List<string> {"worksWithFamily","hiredByAFamilymember","hardWorker","selfMadeCubeByDedication","getsFiredAlot","RetiredYoung","DiedBeforeRetired","flipflop","nepotism","AselfMadeShapeWeAspireToBe"} ),
new SValues("Shapesarenothingifnotsocial", new List<string> {"hasAbestFriend","loner","isolated","hasalotofenemies","socialLife","reserved","Shapesarenothingifnotsocial"} ),
new SValues("AntiFaviortisum", new List<string> {"exploteative","worksWithFamily","hiredByAFamilymember","conventional","selfMadeCubeByDedication","nepotism","AntiFaviortisum"} ),
new SValues("WeArewNothingIfWeAreNotReserved", new List<string> {"hasAbestFriend","loner","isolated","hasalotofenemies","TooTrustingOfEnemies","socialLife","reserved","SusMovments","WeArewNothingIfWeAreNotReserved"} ),
new SValues("ImmagretsWeGetTheJobDone", new List<string> {"supportsImmigration","ImmagretsWeGetTheJobDone"} ),
new SValues("suchUncharactristicBehaviorOhMy", new List<string> {"marriedForLifeStyleNotLove","marriedSomoneOlder","InLovewithspouseoffriend","widowedbutnotgrieving","leftFotLoveIntrest","InLoveWirhAnothersspouce","startedAfamilyAtAyoungAge","DevorcedManyPeople","WillActOnLove","exploteative","conventional","adultbutnotworking","AdventureSeeker","MovesAlot","getsFiredAlot","loner","isolated","hasalotofenemies","friendwithabestfriendsenemy","SusMovments","suchUncharactristicBehaviorOhMy"} )

   
};
    /*
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

        new SValues("AnimalLover", new List<string> { "ButcherButRegretful" ,"butcherRole"} ),

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



        };*/

    public static List<string> returnCompatibleSurfaceValues(string pattern)
    {
        List<string> listOfCompatibleSV = new List<string>();
        foreach(SValues sv in allSurfaceValues)
        {
            if (sv.coreValues.Contains(pattern))
            {
                listOfCompatibleSV.Add(sv.svName);
               Debug.Log("the pattern" + pattern + " was compatible with " + sv.svName);

            } 
        }
        if (listOfCompatibleSV.Count == 0)
        {
            listOfCompatibleSV.Add("suchUncharactristicBehaviorOhMy");

            //debugging --- changing it here for now, change this later 
            if(pattern== "InLoveWirhAnothersspouce")
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