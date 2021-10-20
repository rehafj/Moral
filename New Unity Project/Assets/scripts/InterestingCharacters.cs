using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InterestingCharacters : MonoBehaviour
{
    public Dictionary<string, bool> characterFlags = new Dictionary<string, bool>();
    public  int personID;
    public int lovedOneID;
    //public int bestfriendID;
    public string fullName;
    public string firstName;
    public string LastName;
    List<TownCharacter> bgcharList;
    public struct LoveTraingle
    {
        public int LovedOneID;
        public int OtherPersonID;
        public int MainPersonID;
    }


    LoveTraingle LoveTraingleIfOthersAreMarried;
    LoveTraingle LoveTraingleBetweenFriends;

    public string Lastoccupation;
    //maybe a couple of structs depending on the flag: D

    public InterestingCharacters()
    {
        characterFlags.Add("departed", false);//found
        characterFlags.Add("familyPerson", false);//found
        characterFlags.Add("InLovewithspouseoffriend", false);//done -NOT FOUND - CHECK LOGIC 
        characterFlags.Add("socialLife", false);//found
        characterFlags.Add("hasalotofenemies", false);//found
        characterFlags.Add("graduate", false);//TO DO -- some shhould have diff values... but my json does not 
        characterFlags.Add("friendwithabestfriendsenemy", false);//found
        characterFlags.Add("exploteative", false); //TODO needs a special loop = check if hired by family memeber and hired as a favor 
        characterFlags.Add("widowedbutnotgrieving", false);//found 1
        characterFlags.Add("adultbutnotworking", false);// as everyone is working -= did not find - in json too
        characterFlags.Add("notworkingandrich", false); // evryone is working in this sim.... 
        characterFlags.Add("loner", false); //found  no naibours and small friend ids
        characterFlags.Add("likesToDate", false);//found 1
        characterFlags.Add("leftFotLoveIntrest", false); //TODO not in this run --- check if this ever gets simulated ? 
        characterFlags.Add("hasAbestFriend", false);//found
        characterFlags.Add("InLoveWirhAnothersspouce", false);//found
        characterFlags.Add("IsWealthy", false);//found 
        characterFlags.Add("startedAfamilyAtAyoungAge", false);//
        characterFlags.Add("MovesAlot", false);
        characterFlags.Add("SusMovments", false);
        characterFlags.Add("getsFiredAlot ", false);
        characterFlags.Add("worksWithFamily", false);
        characterFlags.Add("hiredByAFamilymember", false);//as a favor 
        characterFlags.Add("RetiredYoung", false);
        characterFlags.Add("DiedBeforeRetired", false);
        characterFlags.Add("DevorcedManyPeople", false);
        characterFlags.Add("marriedSomoneOlder", false);
        characterFlags.Add("marriedForLifeStyleNotLove", false);
        characterFlags.Add("selfMadeCubeByDedication", false);
       // characterFlags.Add("InLoveWirhAnothersspouce", false);

    
        //personalityrelated 
        characterFlags.Add("WillActOnLove", false);
        characterFlags.Add("IsRichButNotGenrous", false);
        characterFlags.Add("AdventureSeeker", false);
        characterFlags.Add("liklyToHelpTheHomeless", false);
        characterFlags.Add("isolated", false);
        characterFlags.Add("ButcherButRegretful", false);
        characterFlags.Add("TooTrustingOfEnemies", false);
        characterFlags.Add("reserved", false);
        characterFlags.Add("conventional", false);
        characterFlags.Add("likedToExperinceCulture", false);
        characterFlags.Add("supportsImmigration", false);
        characterFlags.Add("doesNotGiveToThoseInNeed", false);
        /*      
             
              characterFlags.Add("WantsArtAsJob", false);
            
              characterFlags.Add("ArtSeller", false);

      */






        //job based
        // characterFlags.Add("nepotism", false);/TODO NEED TO DO THIS ONE.... 
        characterFlags.Add("flipflop", false);//found
        characterFlags.Add("WorksInAlcohol", false); //found
        characterFlags.Add("healerRole", false);//found
        characterFlags.Add("polluterRole", false);//found
        characterFlags.Add("riskTaker", false);//found
        characterFlags.Add("butcherRole", false);//found
        characterFlags.Add("Teachingrole", false);//found
        characterFlags.Add("advancedCareer", false);//found
        characterFlags.Add("hardWorker", false); //high level in job 
        characterFlags.Add("CustodianJobs", false); //high level in job 
        characterFlags.Add("generalJobs", false); //high level in job 
        characterFlags.Add("selfMadeCube", false);


    }

    public void SetLoveTraingleValues( int lovedoneid, int OtherPersonID)
    {
        LoveTraingleIfOthersAreMarried.MainPersonID = personID;
        LoveTraingleIfOthersAreMarried.OtherPersonID = OtherPersonID;
        LoveTraingleIfOthersAreMarried.LovedOneID = lovedoneid;
      
    }

    public string GetLoverName() //change this into something more generic 
    {
        TownCharacter townie =   bgcharList.Find(x => x.id == lovedOneID);
        return townie.firstName;

    }

    public bool HasJuicyMoralFacts()
    {

        if (characterFlags["InLovewithspouseoffriend"] || characterFlags["leftFotLoveIntrest"]
            || characterFlags["butcherRole"] || characterFlags["IsRichButNotGenrous"])
        {
            return true;
        }
        else return false;
    }

    public int NumberOFFlags()
    {
        int size = 0;
        foreach(KeyValuePair<string,bool> kvp in characterFlags)
        {
            if (kvp.Value)
            {
                size++;
            }
            
        }
        return size;
    }


    // Start is called before the first frame update
    void Start()
    {
        bgcharList = FindObjectOfType<JsonLoader>().backgroundcharacters;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isLoverPartner() {
        if (LoveTraingleIfOthersAreMarried.LovedOneID == LoveTraingleIfOthersAreMarried.OtherPersonID)
        {

            return true;
        }
        else return false;
    }


    /// <summary>
    /// it's mainly used for flags on the postage/information on the player information book
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetStringTranslation(string key) //CHANGE THIS INTO regExp //post it note usage
    {



        switch (key)
        {
            case ("doesNotGiveToThoseInNeed"):
                return "despite being wealthy, this shape does not donate to those in need";
            case ("supportsImmigration"):
                return "this shape often supports immagrents";
            case ("likedToExperinceCulture"):
                return " this shape likes to travel and experince cultures";
            case ("RetiredYoung"):
                return "retied at a young age";
            case ("hiredByAFamilymember"):
                return "this shape was hired by a family member";
            case ("worksWithFamily"):
            return "this shape works with their family";
            case ("getsFiredAlot"):
                return "this shape gets fired alot ";
            case ("conventional"):
                return "this shape is quite conventional";
            case ("reserved"):
                return "thius shape is reserved";
            case ("TooTrustingOfEnemies"):
                return "Some might say this shape is too gulible, they are too trusting of those who mean them harm";
            case ("ButcherButRegretful"):
                return "this shape works as a butcher but they hate thier job";
            case ("isolated"):
                return "this shape prefers to stay alone, kinda dislikes social stuff";
            case ("liklyToHelpTheHomeless"):
                return "this shape seems to volenteer and help those less fourtunate";
            case ("AdventureSeeker"):
                return "they seem to have an adventureur\'s heart";
            case ("marriedForLifeStyleNotLove"):
                return "they seem to have not married for love";
            case ("marriedSomoneOlder"):
                return "they settled down with an older partner";
            case ("DevorcedManyPeople"):
                return "this shape went through so many devorces";

            case ("MovesAlot"):
                return "they sure moved alot before coming to this comunity";
            case ("SusMovments"):
                return "they have  some odd behavior....";
            case ("startedAfamilyAtAyoungAge"):
                return "rasing a family and oh so young";
            case ("departed"):
                return "apparently, they moved out of this town";
            case ("familyPerson"):
                return "looks like they have a large family";
            case ("InLovewithspouseoffriend"):
                return "apparently, this shape is in love with someone other than their spouce...";
            case ("socialLife"):
                return "someone likes to party, they have a thriving social life";
            case ("hasalotofenemies"):
                return "they apparently have alot of enemies";
            case ("graduate"):
                return "graduated from X";
            case ("friendwithabestfriendsenemy"):
                return "they are not that faithful to their friends";
            case ("widowedbutnotgrieving"):
                return "this shape was widowed, reports show they never really grived";
            case ("adultbutnotworking"):
                return "they are not part of the workforce";

            case ("notworkingandrich"):
                return "this shape is so rich and they do not  work";
            case ("DiedBeforeRetired"):
                return "died before retirment!";
            case ("loner"):
                return "this shape likes to keep to themselves";

            case ("likesToDate"):
                return "this shape apparently likes to date alot...";

            case ("leftFotLoveIntrest"):
                return "this shape left their partner for notions of love?";
            case ("hasAbestFriend"):
                return "this shape has a best friend";

            case ("InLoveWirhAnothersspouce"):
                return "this shape is in love with a taken shape...";

            case ("IsWealthy"):
                return "this shape is super wealthy";

            case ("WillActOnLove"):
                return "this shape has been known to act out publicly, esp in matters of love";

            case ("IsRichButNotGenrous"):
                return "this shape is so wealthy but too bad they are not the philanthropist kind";

            case ("flipflop"):
                return "this shape changed careers alot";

            case ("WorksInAlcohol"):
                return "this shape works in alcholo";

            case ("healerRole"):
                return "this shape works in medicine! ";

            case ("polluterRole"):
                return "this shape work is not great for the enviroment ";

            case ("riskTaker"):
                return "this shape work is high risk ";

            case ("butcherRole"):
                return "this shape works at a farm ";

            case ("Teachingrole"):
                return "this shape teaches for a living";

            case ("advancedCareer"):
                 return "this shape managed to level up their career!";

            case ("hardWorker"):
            case ("selfMadeCube"):
            case("selfMadeCubeByDedication"):
                return "this shape is such a go getter! ";

            case ("CustodianJobs"):
                return "this shape works as a custodian";

            case ("generalJobs"):
                return "this shape is in the work force";
            case ("exploteative"):
                return "this shape is knda crafty! they know how to work a room ";
            default:
                return "nill";

        }
    }
}
//ask about what adoptioneventID means ? they adopted or havber adopted 

//single mother has love intrest - married - family id - mans father loves a daughter 
//step daughter is step step mother



