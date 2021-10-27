using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterTopics : MonoBehaviour
{
    public static List<ConversationalTopic> allTopics = new List<ConversationalTopic>()
    {
         new ConversationalTopic("RomaticRelationshipTopics", new List<string> {"InLovewithspouseoffriend", "widowedbutnotgrieving",
            "likesToDate","leftFotLoveIntrest","InLoveWirhAnothersspouce",
            "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
            "WillActOnLove","marriedSomoneOlder"}, "romantic relationships"),

         new ConversationalTopic("FamilyAffairsTopics", new List<string> {"leftFotLoveIntrest", "InLoveWirhAnothersspouce",
            "startedAfamilyAtAyoungAge","DevorcedManyPeople","marriedForLifeStyleNotLove",
            "marriedSomoneOlder","worksWithFamily","hiredByAFamilymember",
            "nepotism"}, "family affairs" ),

         new ConversationalTopic("SocialRelationsTopics", new List<string> {"InLoveWirhAnothersspouce",   "InLovewithspouseoffriend",  "likesToDate",
             "hasAbestFriend", "loner"  , "isolated" ,   "hasalotofenemies" ,   "friendwithabestfriendsenemy", "TooTrustingOfEnemies" , 
             "socialLife" , "reserved"}, "social shapes" ),


        new ConversationalTopic("MoneyTopics", new List<string> { "graduate", "notworkingandrich","IsWealthy","exploteative",
            "adultbutnotworking", "hardWorker",  "selfMadeCubeByDedication",    "liklyToHelpTheHomeless",  "doesNotGiveToThoseInNeed", 
            "IsRichButNotGenrous"  }, "Money, mola" ),

         new ConversationalTopic("WorkAndCareerTopics", new List<string> {

         "graduate",   "notworkingandrich" ,  "IsWealthy" ,  "exploteative"  ,  "adultbutnotworking"  ,"hardWorker" , "selfMadeCubeByDedication", 
             "IsRichButNotGenrous", "worksWithFamily", "hiredByAFamilymember",    "nepotism" ,   "polluterRole",   "ButcherButRegretful", "butcherRole",
             "WorksInAlcohol" , "Teachingrole"   , "supportsImmigration" ,"getsFiredAlot",
             "RetiredYoung"   , "DiedBeforeRetired"   ,"flipflop"   , "advancedCareer",  "CustodianJobs",   "generalJobs"
         }, "a shape's carreer" ),

         new ConversationalTopic("CommunityTopics", new List<string> { "IsRichButNotGenrous",   "supportsImmigration", "liklyToHelpTheHomeless",
             "doesNotGiveToThoseInNeed",    "socialLife" , "familyPerson",    "conventional",    "MovesAlot",   "likedToExperinceCulture" ,"SusMovments", "departed" }, "Shape comunities" ),

         new ConversationalTopic("LifestyleTopics", new List<string> { "liklyToHelpTheHomeless" ,   "doesNotGiveToThoseInNeed" ,
             "socialLife",  "conventional",    "MovesAlot" ,  "likedToExperinceCulture", "SusMovments", "departed", 
             "IsWealthy" ,  "adultbutnotworking",  "RetiredYoung",    "flipflop" ,   "likesToDate", "hasAbestFriend" , "loner" ,  "isolated",
             "TooTrustingOfEnemies" ,   "reserved" ,   "startedAfamilyAtAyoungAge",  "marriedForLifeStyleNotLove",  "AdventureSeeker", "riskTaker" } ," a shape's lifestyle" ),

        new ConversationalTopic("GossipyTopics", new List<string> { "doesNotGiveToThoseInNeed", "SusMovments", "adultbutnotworking", 
            "TooTrustingOfEnemies",    "marriedForLifeStyleNotLove",  "AdventureSeeker", "riskTaker" ,  "IsRichButNotGenrous", "exploteative",
            "hiredByAFamilymember",    "nepotism",    "getsFiredAlot",   "DiedBeforeRetired",   "InLoveWirhAnothersspouce" ,  
            "InLovewithspouseoffriend",    "hasalotofenemies",    "friendwithabestfriendsenemy" ,"leftFotLoveIntrest",  "marriedSomoneOlder",
            "widowedbutnotgrieving",   "WillActOnLove" },"hot goss" )



    };

    
}

public class ConversationalTopic
{
     string topic = "";
     List<string> conversationalPatterns = new List<string>();
    string rumorAppendedTitil;

    public ConversationalTopic(string topicCluster, List<string> patternsUnderTopic, string rumorAppendedTopicTitle)
    {
        topic = topicCluster;
        conversationalPatterns = patternsUnderTopic;
        rumorAppendedTitil =  "speaking of " + rumorAppendedTopicTitle; //speaking off might change to something else 
    }
}

