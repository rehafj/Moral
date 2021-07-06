using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralModels 
{
    // Start is called before the first frame update
    protected double getChances()
    {

        return 1.73f;
    }


    List<string> BTrueTYourHeart_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving","adultbutnotworking",
        "loner","likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","conventional","reserved",
    }; // to exclude - adult but not working 
    // 
    List<string> LoveIsForFools_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving","adultbutnotworking",
        "loner","likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","conventional","reserved",
    };

    List<string> MoneyMaker_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife", "hasalotofenemies","graduate",
        "exploteative","adultbutnotworking","notworkingandrich","IsWealthy",
        "getsFiredAlot","SusMovments","RetiredYoung",
        "DiedBeforeRetired","IsRichButNotGenrous","ButcherButRegretful","selfMadeCubeByDedication","likedToExperinceCulture",
        "doesNotGiveToThoseInNeed","reserved","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication"
    };

    List<string> Enviromentalist_CoreValues = new List<string>() //else are the othervalues...
    {
        "IsWealthy", "ButcherButRegretful","conventional",
        "reserved","polluterRole","advancedCareer"
    };

    List<string> EnviromentalistAnti_CoreValues = new List<string>() //else are the othervalues...
    {
        "IsWealthy","conventional",
        "reserved","polluterRole","advancedCareer"
    };


    List<string> AnimalLover_CoreValues = new List<string>() //else are the othervalues...
    {
        "familyPerson","ButcherButRegretful",
        "likedToExperinceCulture","conventional","reserved","hiredByAFamilymembe","butcherRole"
        
    };

    List<string> AnimalLoverAnti_CoreValues = new List<string>() //else are the othervalues...
    {
        "familyPerson","ButcherButRegretful",
        "likedToExperinceCulture","conventional","reserved","hiredByAFamilymembe","butcherRole"

    };

    List<string> Teetotasler_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","hasalotofenemies","getsFiredAlot",
        "familyPerson","reserved","conventional","WorksInAlcohol","hardWorker"

    };

    List<string> TeetotaslerAnti_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","hasalotofenemies","familyPerson",
        "getsFiredAlot","reserved","conventional","WorksInAlcohol","hardWorker"

    };
    List<string> FamilyPerson_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","adultbutnotworking","familyPerson",
        "hasAbestFriend","worksWithFamily","hiredByAFamilymember","DevorcedManyPeople","marriedForLifeStyleNotLove","isolated",
        "conventional","reserved","nepotism","selfMadeCubeByDedication"

    };
    List<string> schoolIsDrool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate","adultbutnotworking",
        "notworkingandrich","loner","IsWealthy","worksWithFamily","hiredByAFamilymember","getsFiredAlot",
        "AdventureSeeker","selfMadeCubeByDedication","likedToExperinceCulture","conventional","reserved","flipflop",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker","selfMadeCubeByDedication"

    };
    List<string> SchoolIsCool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate","adultbutnotworking"
       ,"loner","IsWealthy","worksWithFamily","hiredByAFamilymember","getsFiredAlot",
        "AdventureSeeker",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker","selfMadeCubeByDedication"

    };
    List<string> SupportingComunities_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","hasalotofenemies","familyPerson",
        "loner","IsWealthy","worksWithFamily","MovesAlot","liklyToHelpTheHomeless","isolated",
        "likedToExperinceCulture","supportsImmigration","conventional","reserved","healerRole","Teachingrole","advancedCareer","CustodianJobs"

    };
    List<string> LoverOfRisks_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend","hasalotofenemies","friendwithabestfriendsenemy",
        "InLoveWirhAnothersspuce","MovesAlot","RetiredYoung","WillActOnLove","AdventureSeeker","TooTrustingOfEnemies",
        "flipflop","WorksInAlcohol","riskTaker",""

    };
    List<string> LandISWhereThehrtIS_CoreValues = new List<string>() //else are the othervalues...
    {"socialLife","familyPerson","loner","likesToDate" ,"hasAbestFriend","worksWithFamily",
        "hiredByAFamilymember","MovesAlot","SusMovments","RetiredYoung","AdventureSeeker",
        "likedToExperinceCulture","supportsImmigration","conventional","reserved"
    };
    List<string> CarrerAboveAll_CoreValues = new List<string>() //else are the othervalues...
    {
        "hasalotofenemies","exploteative","adultbutnotworking","notworkingandrich","loner","hasAbestFriend",
        "IsWealthy","worksWithFamily","hiredByAFamilymember","MovesAlot","getsFiredAlot","SusMovments",
        "RetiredYoung","DiedBeforeRetired","IsRichButNotGenrous","liklyToHelpTheHomeless","isolated",
        "doesNotGiveToThoseInNeed","conventional","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication"

    };
    List<string> FriendsAreTheJoyOFlife_CoreValues = new List<string>() //else are the othervalues...
    {
       "InLovewithspouseoffriend","socialLife","hasalotofenemies","friendwithabestfriendsenemy","exploteative","loner","hasAbestFriend"
            ,"MovesAlot","SusMovments","AdventureSeeker","isolated","TooTrustingOfEnemies","conventional","reserved"
    };
    List<string> Loner_CoreValues = new List<string>()
    {"socialLife","hasalotofenemies","friendwithabestfriendsenemy","exploteative","loner","hasAbestFriend","MovesAlot","SusMovments"
        ,"AdventureSeeker","isolated","TooTrustingOfEnemies","conventional","reserved"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };


/*

    //change these 
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","getsFiredAlot","SusMovments","RetiredYoung","DiedBeforeRetired","TooTrustingOfEnemies",
        "supportsImmigration","conventional","reserved","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };*/

}
