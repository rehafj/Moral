using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralModels 
{
    JsonLoader loader;

    protected double getChances()
    {

        return 1.73f;
    }

    //Debug.Log("TESTING THIS OUT ---" + jsn.listOfArguments.Count);


    List<string> BTrueTYourHeart_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving",
        "loner","likesToDate","leftFotLoveIntrest","InLoveWithAnothersspuce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","conventional","reserved"
    }; 
    List<string> LoveIsForFools_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving",
        "loner","likesToDate","leftFotLoveIntrest","InLoveWithAnothersspuce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove","conventional","reserved"
    };
    List<string> MoneyMaker_CoreValues = new List<string>() //else are the othervalues...
    {
        "graduate",
        "exploteative","adultbutnotworking","notworkingandrich","IsWealthy",
        "getsFiredAlot","SusMovments","RetiredYoung",
        "DiedBeforeRetired","IsRichButNotGenrous","selfMadeCubeByDedication","likedToExperinceCulture",
        "doesNotGiveToThoseInNeed","reserved","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication","worksWithFamily"
    };
    List<string> Enviromentalist_CoreValues = new List<string>() //else are the othervalues...
    {
        "IsWealthy", "ButcherButRegretful","conventional",
        "reserved","polluterRole","advancedCareer"
    };
    List<string> EnviromentalistAnti_CoreValues = new List<string>() //else are the othervalues...
    {
        "IsWealthy","conventional","ButcherButRegretful",
        "reserved","polluterRole","advancedCareer"
    };
    List<string> AnimalLover_CoreValues = new List<string>() //else are the othervalues...
    {
        "familyPerson","ButcherButRegretful",
        "likedToExperinceCulture","conventional","reserved","butcherRole"
        
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
        "adultbutnotworking","familyPerson",
        "worksWithFamily","hiredByAFamilymember","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "conventional","reserved","nepotism","selfMadeCubeByDedication"

    };
    List<string> schoolIsDrool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate","adultbutnotworking",
        "notworkingandrich","loner","IsWealthy","worksWithFamily","getsFiredAlot",
        "AdventureSeeker","selfMadeCubeByDedication","likedToExperinceCulture","conventional","reserved","flipflop",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker","selfMadeCubeByDedication"

    };
    List<string> SchoolIsCool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate",
      "IsWealthy","worksWithFamily","getsFiredAlot",
        "AdventureSeeker","conventional",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker","selfMadeCubeByDedication"

    };
    List<string> SupportingComunities_CoreValues = new List<string>() //else are the othervalues...
    {
       "worksWithFamily","liklyToHelpTheHomeless","doesNotGiveToThoseInNeed"
        ,"supportsImmigration","conventional","reserved","healerRole","Teachingrole","advancedCareer","CustodianJobs"

    };
    List<string> LoverOfRisks_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend","hasalotofenemies","friendwithabestfriendsenemy",
        "InLoveWithAnothersspuce","MovesAlot","RetiredYoung","WillActOnLove","AdventureSeeker","TooTrustingOfEnemies",
        "flipflop","WorksInAlcohol","riskTaker"

    };
    List<string> LandISWhereThehrtIS_CoreValues = new List<string>() //else are the othervalues...
    {"socialLife","familyPerson","loner" ,"hasAbestFriend","worksWithFamily",
        "hiredByAFamilymember","MovesAlot","SusMovments",
        "supportsImmigration","conventional","reserved"
    };
    List<string> CarrerAboveAll_CoreValues = new List<string>() //else are the othervalues...
    {
        "hasalotofenemies","exploteative","adultbutnotworking","notworkingandrich","loner",
        "IsWealthy","worksWithFamily","hiredByAFamilymember","MovesAlot","getsFiredAlot",
        "RetiredYoung","DiedBeforeRetired","liklyToHelpTheHomeless","isolated"
        ,"conventional","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication"

    };
    List<string> FriendsAreTheJoyOFlife_CoreValues = new List<string>() //else are the othervalues...
    {
       "InLovewithspouseoffriend","socialLife","hasalotofenemies","friendwithabestfriendsenemy","exploteative","loner","hasAbestFriend"
            ,"MovesAlot","SusMovments","AdventureSeeker","isolated","TooTrustingOfEnemies","conventional","reserved"
    };
    List<string> Loner_CoreValues = new List<string>()
    {"socialLife","hasalotofenemies","friendwithabestfriendsenemy","exploteative","loner","hasAbestFriend","MovesAlot","SusMovments"
        ,"isolated","TooTrustingOfEnemies","reserved"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","graduate","adultbutnotworking","worksWithFamily","SusMovments","RetiredYoung","DiedBeforeRetired",
        "supportsImmigration","conventional","healerRole","Teachingrole","hardWorker","selfMadeCubeByDedication"

    };
    List<string> ProHiringFamily_CoreValues = new List<string>()
    {
        "familyPerson","exploteative","adultbutnotworking","notworkingandrich","startedAfamilyAtAyoungAge","worksWithFamily",
        "hiredByAFamilymember","conventional", "nepotism"
    };
    List<string> suchUncharactristicBehaviorOhMy_CoreValues = new List<string>()
    {
        "InLovewithspouseoffriend","hasalotofenemies","friendwithabestfriendsenemy","exploteative","widowedbutnotgrieving"
        ,"adultbutnotworking","loner","InLoveWithAnothersspuce","MovesAlot","getsFiredAlot","SusMovments","RetiredYoung","DevorcedManyPeople",
        "TooTrustingOfEnemies","conventional","nepotism","flipflop"

    };
    List<string> WeLiveForSpontaneity_CoreValues = new List<string>()
    {"InLovewithspouseoffriend","hasalotofenemies","likesToDate","leftFotLoveIntrest","MovesAlot","SusMovments","RetiredYoung",
        "marriedForLifeStyleNotLove","startedAfamilyAtAyoungAge",
        "flipflop", "riskTaker",
        "MovesAlot","getsFiredAlot","TooTrustingOfEnemies","likedToExperinceCulture"

    };
    List<string> AnAdventureWeSeek_CoreValues = new List<string>()
    {
        "InLovewithspouseoffriend","hasalotofenemies", "likesToDate","InLoveWithAnothersspuce","MovesAlot","riskTaker",
        "RetiredYoung","DevorcedManyPeople","WillActOnLove","AdventureSeeker","TooTrustingOfEnemies","likedToExperinceCulture","supportsImmigration","flipflop"



    };
    List<string> NiaeveteIsFiction_CoreValues = new List<string>() {

        "InLovewithspouseoffriend","hasalotofenemies","friendwithabestfriendsenemy","InLoveWithAnothersspuce",
        "SusMovments","DevorcedManyPeople","WillActOnLove","supportsImmigration",
        "liklyToHelpTheHomeless","TooTrustingOfEnemies","doesNotGiveToThoseInNeed"
    };
    List<string> AselfMadeShapeWeAspireToBe_CoreValues = new List<string>()
        {"graduate","adultbutnotworking","notworkingandrich",
            "worksWithFamily","hiredByAFamilymember","getsFiredAlot","RetiredYoung","DiedBeforeRetired","IsRichButNotGenrous","conventional",
            "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
             "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication" };
    List<string> Shapesarenothingifnotsocial_CoreValues = new List<string>()
        {"InLovewithspouseoffriend","socialLife","friendwithabestfriendsenemy","exploteative","hasAbestFriend","InLoveWithAnothersspuce","MovesAlot",
            "DevorcedManyPeople","isolated",
            "reserved"

        };   
    List<string> AntiFaviortisum_CoreValues = new List<string>()
        {"graduate","exploteative","IsWealthy","hiredByAFamilymember","selfMadeCubeByDedication","conventional","nepotism","WorksInAlcohol",
            "polluterRole","riskTaker","butcherRole","healerRole","Teachingrole","hardWorker","advancedCareer","generalJobs"

        };
    List<string> WeArewNothingIfWeAreNotReserved_CoreValues = new List<string>()
        {"likesToDate","InLoveWithAnothersspuce","startedAfamilyAtAyoungAge","worksWithFamily","hiredByAFamilymember","getsFiredAlot","SusMovments","DevorcedManyPeople","marriedForLifeStyleNotLove",
            "IsRichButNotGenrous","liklyToHelpTheHomeless","reserved","TooTrustingOfEnemies","doesNotGiveToThoseInNeed","supportsImmigration","conventional",
            "hasalotofenemies","InLovewithspouseoffriend"

        };
    List<string> ImmagretsWeGetTheJobDone_CoreValues = new List<string>()
        {

            "AdventureSeeker","likedToExperinceCulture","supportsImmigration","conventional","reserved","flipflop","socialLife","familyPerson",
            "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","SusMovments","MovesAlot",
             "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication","graduate",

        };



    List<string> PositiveSVList = new List<string>() //used to count these sv as positive -= later on search for high values in strings 
    {
        "BTrueTYourHeart", "MoneyMaker", "Enviromentalist", "AnimalLover", "Teetotasler", "FamilyPerson", "SchoolIsCool", "SupportingComunities", "LoverOfRisks", "AselfMadeShapeWeAspireToBe",
        "LandISWhereThehrtIS", "CarrerAboveAll", "FriendsAreTheJoyOFlife", "youthAreTheFuture", "ProHiringFamily", "WeLiveForSpontaneity", "AnAdventureWeSeek", "ImmagretsWeGetTheJobDone", "Shapesarenothingifnotsocial"
    };

    protected string returnSchemaValue(string SV)
    {

        if (!PositiveSVList.Contains(SV))
        {
            return "low";
        }
        else 
            return "high";
  
        
    }
    string returnSchemaType(List<string> exploredSchema, string surfaceValue, string subPattern) 
    {

        return "";
    }

    protected 


    string testingOutSomething(string schemaName, string surfuaceValueKey, string subvalueKey)
    {
        //t this works --- move this method to father model script or the model parents :) but this returns the appropriate text bad O tho
        foreach (ModelArguements argument in loader.listOfArguments)
        {
            if (argument.schema == schemaName)
            {
                foreach (Surfacevalue S in argument.Surfacevalues)
                {
                    if (S.key == surfuaceValueKey)
                    {
                        foreach (SurfaceValueObj O in S.surfaceValueObj)
                        {
                            if (O.subvalue == subvalueKey)
                            {
                                return O.text;
                            }
                        }
                    }
                }
            }
            //   Debug.Log("FFS - schema =   " + argument.schema + "surface values are " + argument.Surfacevalues[0].key + " AND THE SUB VALUE KEY IS " + argument.Surfacevalues[0].surfaceValueObj[0].subvalue);

        }
        return "no text found!";
    }

}


//returnListOfDialougNodes
//mappedKey
//setCnpcDialoug

//getPlayerResponce
//mappedSV
//mappedKey
//setCnpcDialoug
//        currentCorutine = StartCoroutine(TypeInDialoug(getPlayerResponce(mapToCNPCMoralFactor(currentNode.Pattern), //TOFRICKENDO changet his to currentnode.sv
