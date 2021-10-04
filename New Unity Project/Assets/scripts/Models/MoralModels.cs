using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoralModels : MonoBehaviour 
{
    JsonLoader loader;
    

    protected double getChances()
    {

        return 1.73f;
    }

    //Debug.Log("TESTING THIS OUT ---" + jsn.listOfArguments.Count);

    List<string> BTrueTYourHeart_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving"
        ,"likesToDate","leftFotLoveIntrest","InLoveWithAnothersspuce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove"
    };

    List<string> LoveIsForFools_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend", "widowedbutnotgrieving",
        "likesToDate","leftFotLoveIntrest","InLoveWithAnothersspuce",
        "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
        "WillActOnLove"
    };
    List<string> MoneyMaker_CoreValues = new List<string>() //else are the othervalues...
    {
        "graduate",
        "exploteative","adultbutnotworking","notworkingandrich","IsWealthy",
        "getsFiredAlot","RetiredYoung",
        "DiedBeforeRetired","IsRichButNotGenrous","selfMadeCubeByDedication","likedToExperinceCulture",
        "doesNotGiveToThoseInNeed","reserved","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication","worksWithFamily"
    };
    List<string> Enviromentalist_CoreValues = new List<string>() //else are the othervalues...
    {
        "ButcherButRegretful","conventional",
        "polluterRole"
    };
    List<string> EnviromentalistAnti_CoreValues = new List<string>() //else are the othervalues...
    {
        "conventional","ButcherButRegretful",
        "polluterRole"
    };
    List<string> AnimalLover_CoreValues = new List<string>() //else are the othervalues...
    {
      "ButcherButRegretful",
       "conventional","butcherRole"
        
    };
    List<string> AnimalLoverAnti_CoreValues = new List<string>() //else are the othervalues...
    {
      "ButcherButRegretful",
        "likedToExperinceCulture","conventional","butcherRole"

    };
    List<string> Teetotasler_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","hasalotofenemies","getsFiredAlot",
        "familyPerson","reserved","conventional","WorksInAlcohol"

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
        "conventional","nepotism"

    };
    List<string> schoolIsDrool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate","adultbutnotworking",
        "notworkingandrich","loner","IsWealthy","worksWithFamily","getsFiredAlot",
        "AdventureSeeker","selfMadeCubeByDedication","likedToExperinceCulture","conventional","reserved","flipflop",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker"
    };
    List<string> SchoolIsCool_CoreValues = new List<string>() //else are the othervalues...
    {
        "socialLife","graduate",
      "IsWealthy","worksWithFamily","getsFiredAlot",
        "AdventureSeeker","conventional",
        "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole","Teachingrole","advancedCareer","hardWorker"

    };
    List<string> SupportingComunities_CoreValues = new List<string>() //else are the othervalues...
    {
       "worksWithFamily","liklyToHelpTheHomeless","doesNotGiveToThoseInNeed"
        ,"supportsImmigration","conventional","reserved","healerRole","Teachingrole","CustodianJobs"

    };
    List<string> LoverOfRisks_CoreValues = new List<string>() //else are the othervalues...
    {
        "InLovewithspouseoffriend","hasalotofenemies",
        "InLoveWithAnothersspuce","MovesAlot","RetiredYoung","WillActOnLove","AdventureSeeker","TooTrustingOfEnemies",
        "flipflop","DiedBeforeRetired","riskTaker"

    };
    List<string> LandISWhereThehrtIS_CoreValues = new List<string>() //else are the othervalues...
    {"socialLife","familyPerson","loner" ,"hasAbestFriend","worksWithFamily",
        "hiredByAFamilymember","MovesAlot","SusMovments",
        "supportsImmigration","conventional","reserved"
    };
    List<string> CarrerAboveAll_CoreValues = new List<string>() //else are the othervalues...
    {
        "hasalotofenemies","exploteative","adultbutnotworking","notworkingandrich",
        "IsWealthy","worksWithFamily","hiredByAFamilymember","MovesAlot","getsFiredAlot",
        "RetiredYoung","DiedBeforeRetired","liklyToHelpTheHomeless"
        ,"conventional","nepotism","flipflop","WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication"

    };
    List<string> FriendsAreTheJoyOFlife_CoreValues = new List<string>() //else are the othervalues...
    {
       "InLovewithspouseoffriend","socialLife","hasalotofenemies","friendwithabestfriendsenemy","loner","hasAbestFriend"
            ,"MovesAlot","SusMovments","isolated","TooTrustingOfEnemies","reserved"
    };
    List<string> Loner_CoreValues = new List<string>()
    {"socialLife","hasalotofenemies","friendwithabestfriendsenemy","loner","hasAbestFriend","MovesAlot","SusMovments"
        ,"isolated","TooTrustingOfEnemies","reserved"

    };
    List<string> youthAreTheFuture_CoreValues = new List<string>()
    {"familyPerson","adultbutnotworking","worksWithFamily",
        "RetiredYoung","DiedBeforeRetired"
        ,"conventional","healerRole","Teachingrole","hardWorker"

    };
    List<string> ProHiringFamily_CoreValues = new List<string>()
    {
        "familyPerson","exploteative","adultbutnotworking",
        "startedAfamilyAtAyoungAge","worksWithFamily",
        "hiredByAFamilymember","conventional", "nepotism"
    };
    List<string> suchUncharactristicBehaviorOhMy_CoreValues = new List<string>()
    {
        "InLovewithspouseoffriend","hasalotofenemies","friendwithabestfriendsenemy","exploteative","widowedbutnotgrieving"
        ,"adultbutnotworking","loner","InLoveWithAnothersspuce","MovesAlot","getsFiredAlot","SusMovments","RetiredYoung","DevorcedManyPeople",
        "TooTrustingOfEnemies","conventional","nepotism","flipflop"

    };
    List<string> WeLiveForSpontaneity_CoreValues = new List<string>()
    {"InLovewithspouseoffriend","hasalotofenemies","likesToDate","leftFotLoveIntrest",
        "MovesAlot","SusMovments",
        "marriedForLifeStyleNotLove","startedAfamilyAtAyoungAge",
        "flipflop", "riskTaker",
        "MovesAlot","TooTrustingOfEnemies","likedToExperinceCulture"

    };
    List<string> AnAdventureWeSeek_CoreValues = new List<string>()
    {
        "InLovewithspouseoffriend","hasalotofenemies", "likesToDate","InLoveWithAnothersspuce","MovesAlot","riskTaker",
        "RetiredYoung","DevorcedManyPeople","WillActOnLove","AdventureSeeker","TooTrustingOfEnemies","likedToExperinceCulture","flipflop"



    };
    List<string> NiaeveteIsFiction_CoreValues = new List<string>() {

        "hasalotofenemies","friendwithabestfriendsenemy",
        "InLoveWithAnothersspuce",
        "SusMovments","DevorcedManyPeople","WillActOnLove","supportsImmigration",
        "liklyToHelpTheHomeless","TooTrustingOfEnemies","doesNotGiveToThoseInNeed"
    };
    List<string> AselfMadeShapeWeAspireToBe_CoreValues = new List<string>()
        {"graduate","adultbutnotworking","notworkingandrich",
            "worksWithFamily","hiredByAFamilymember","getsFiredAlot","RetiredYoung","DiedBeforeRetired",
            "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
             "Teachingrole","advancedCareer","hardWorker","CustodianJobs","generalJobs","selfMadeCubeByDedication" };
    List<string> Shapesarenothingifnotsocial_CoreValues = new List<string>()
        {"InLovewithspouseoffriend","socialLife","friendwithabestfriendsenemy","exploteative","hasAbestFriend","InLoveWithAnothersspuce","MovesAlot",
            "DevorcedManyPeople","isolated",
            "reserved"

        };   
    List<string> AntiFaviortisum_CoreValues = new List<string>()
        {"graduate","exploteative","IsWealthy","hiredByAFamilymember","selfMadeCubeByDedication","nepotism","WorksInAlcohol",
            "polluterRole","riskTaker","butcherRole","healerRole","Teachingrole","hardWorker","advancedCareer","generalJobs"

        };
    List<string> WeArewNothingIfWeAreNotReserved_CoreValues = new List<string>()
        {"likesToDate","InLoveWithAnothersspuce","startedAfamilyAtAyoungAge","SusMovments","DevorcedManyPeople","marriedForLifeStyleNotLove",
            "IsRichButNotGenrous","liklyToHelpTheHomeless","reserved","TooTrustingOfEnemies","doesNotGiveToThoseInNeed","supportsImmigration",
            "hasalotofenemies","InLovewithspouseoffriend"

        };
    List<string> ImmagretsWeGetTheJobDone_CoreValues = new List<string>()
        {

            "likedToExperinceCulture","supportsImmigration","conventional","reserved","flipflop",
        "familyPerson",
            "WorksInAlcohol","healerRole","polluterRole","riskTaker","butcherRole",
        "SusMovments","MovesAlot",
             "Teachingrole","advancedCareer","hardWorker","CustodianJobs",
        "generalJobs","selfMadeCubeByDedication","graduate",

        };


    protected Dictionary<string, List<string>> coreValuesDic = new Dictionary<string, List<string>>();


    public MoralModels()
    {
        coreValuesDic.Add("BTrueTYourHeart_CoreValues", BTrueTYourHeart_CoreValues);
        coreValuesDic.Add("LoveIsForFools_CoreValues", LoveIsForFools_CoreValues);
        coreValuesDic.Add("ImmagretsWeGetTheJobDone_CoreValues", ImmagretsWeGetTheJobDone_CoreValues);
        coreValuesDic.Add("WeArewNothingIfWeAreNotReserved_CoreValues", WeArewNothingIfWeAreNotReserved_CoreValues);
        coreValuesDic.Add("AntiFaviortisum_CoreValues", AntiFaviortisum_CoreValues);
        coreValuesDic.Add("Shapesarenothingifnotsocial_CoreValues", Shapesarenothingifnotsocial_CoreValues);
        coreValuesDic.Add("AselfMadeShapeWeAspireToBe_CoreValues", AselfMadeShapeWeAspireToBe_CoreValues);
        coreValuesDic.Add("NiaeveteIsFiction_CoreValues", NiaeveteIsFiction_CoreValues);
        coreValuesDic.Add("AnAdventureWeSeek_CoreValues", AnAdventureWeSeek_CoreValues);
        coreValuesDic.Add("WeLiveForSpontaneity_CoreValues", WeLiveForSpontaneity_CoreValues);
        coreValuesDic.Add("suchUncharactristicBehaviorOhMy_CoreValues", suchUncharactristicBehaviorOhMy_CoreValues);
        coreValuesDic.Add("ProHiringFamily_CoreValues", ProHiringFamily_CoreValues);
        coreValuesDic.Add("youthAreTheFuture_CoreValues", youthAreTheFuture_CoreValues);
        coreValuesDic.Add("Loner_CoreValues", Loner_CoreValues);
        coreValuesDic.Add("FriendsAreTheJoyOFlife_CoreValues", FriendsAreTheJoyOFlife_CoreValues);
        coreValuesDic.Add("CarrerAboveAll_CoreValues", CarrerAboveAll_CoreValues);
        coreValuesDic.Add("LandISWhereThehrtIS_CoreValues", LandISWhereThehrtIS_CoreValues);
        coreValuesDic.Add("LoverOfRisks_CoreValues", LoverOfRisks_CoreValues);
        coreValuesDic.Add("SupportingComunities_CoreValues", SupportingComunities_CoreValues);
        coreValuesDic.Add("SchoolIsCool_CoreValues", SchoolIsCool_CoreValues);
        coreValuesDic.Add("schoolIsDrool_CoreValues", schoolIsDrool_CoreValues);
        coreValuesDic.Add("FamilyPerson_CoreValues", FamilyPerson_CoreValues);
        coreValuesDic.Add("Teetotasler_CoreValues", Teetotasler_CoreValues);
        coreValuesDic.Add("TeetotaslerAnti_CoreValues", TeetotaslerAnti_CoreValues);
        coreValuesDic.Add("AnimalLoverAnti_CoreValues", AnimalLoverAnti_CoreValues);
        coreValuesDic.Add("AnimalLover_CoreValues", AnimalLover_CoreValues);
        coreValuesDic.Add("Enviromentalist_CoreValues", Enviromentalist_CoreValues);
        coreValuesDic.Add("EnviromentalistAnti_CoreValues", EnviromentalistAnti_CoreValues);
        coreValuesDic.Add("MoneyMaker_CoreValues", MoneyMaker_CoreValues);
    }

    List<string> NPCfmHighValues = new List<string>() //what the NPC looks for - ig its not herte NPC looks for low 
    {
        "BTrueTYourHeart",  "FamilyPerson"
       
    };

    protected string returnSchemaValue(string SV)
    {
        if (!NPCfmHighValues.Contains(SV))
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

 /*   protected string returnCurrentModelString(List<string> exploredflags, string subvalue, string surfaceValue, bool isCNPC)
    {
        List<String> exploredScehmas = new List<string>();
        string typeOfSchema = "low";
        if(isCNPC) { typeOfSchema = "high";}
        if (getCoreValueList(surfaceValue).Contains(subvalue))
        {
           
        }
        else
        {
        }// error
    }*/

    private List<string> getCoreValueList(string surfaceValue)
    {
        string sv = string.Concat(surfaceValue, "_CoreValues");
        foreach( KeyValuePair<string, List<String>> kvp in coreValuesDic)
        {
            if(kvp.Key == sv)
            {
                return kvp.Value;
            } 
          
        }
        Debug.LogError("we missed a sv mapping somehwew, assigning cv carreer abover all for " + sv);
        return CarrerAboveAll_CoreValues;
         }
    
    List <string> returnSchemaName(string typeOfSchema)
    {
        if (typeOfSchema == "high")
        {
            return new List<string>() { "highStrength", "highMoralBoundaries", "highMoralOrder","highMoralOrder", "highMoralWholeness", "highMoralEssence" };
        }else
        {
            return new List<string>() { "lowMoralBoundaries", "lowMoralOrder", "lowMoralWholeness", "lowMoralEssence", "lowStrength", "lowSelfIntrest" };
        }

    }
    //use this 
    string returnSchemaText(string schemaName, string surfuaceValueKey, string subvalueKey)
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


    string returnSchemaPattern(string schemaName, string surfuaceValueKey, string subvalueKey)
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
//

//        currentCorutine = StartCoroutine(TypeInDialoug(getPlayerResponce(mapToCNPCMoralFactor(currentNode.Pattern), //TOFRICKENDO changet his to currentnode.sv
// InLoveWithAnothersspuce

//*




/*

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
*/