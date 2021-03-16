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
        characterFlags.Add("pregnantbutnotengaged", false);//found 1
        characterFlags.Add("pregnantbutnotfromspuceorbutloveintrest", false); //TODO not in this run --- check if this ever gets simulated ? 
        characterFlags.Add("hasAbestFriend", false);//found
        characterFlags.Add("InLoveWirhAnothersspuce", false);//found
        characterFlags.Add("IsWealthy", false);//found 
    //    characterFlags.Add("IsYoungAndPregnant", false);//<16 and preg
   /*     characterFlags.Add("worksWithFamily", false);


        characterFlags.Add("hiredByAFamilymember", false);//as a favor 
        characterFlags.Add("MovesAlot", false);
        characterFlags.Add("getsFiredAlot", false);
        characterFlags.Add("SusMovments", false);//loner and moves alot and
        characterFlags.Add("RetiredYoung", false);
        characterFlags.Add("DiedBeforeRetired", false);
        characterFlags.Add("DevorcedManyPeople", false);
        characterFlags.Add("marriedSomoneOlder", false); //what can i infer if a person is both conventional and reserved, kinda worried about topics...?
        characterFlags.Add("marriedForLifeStyleNotLove", false); //what can i infer if a person is both conventional and reserved, kinda worried about topics...?

*/
        //personalityrelated 
        characterFlags.Add("WillActOnLove", false);
        characterFlags.Add("IsRichButNotGenrous", false);
  /*      characterFlags.Add("AdventureSeeker", false);
        characterFlags.Add("liklyToHelpTheHomeless", false);
        characterFlags.Add("isolated", false);
        characterFlags.Add("WantsArtAsJob", false);
        characterFlags.Add("ButcherButRegretful", false);
        characterFlags.Add("TooTrustingOfEnemies", false);
        characterFlags.Add("ArtSeller", false);
        characterFlags.Add("selfMadeCubeByDedication", false);
        characterFlags.Add("likedToExperinceCulture", false);
        characterFlags.Add("doesNotGiveToThoseInNeed", false);//.i.e does not support comun
        characterFlags.Add("supportsImmigration", false);
        characterFlags.Add("conventional", false);
        characterFlags.Add("reserved", false);

*/






        //job based
        // characterFlags.Add("nepotism", false);//TODO hired as favor and by family (check company former owner) 
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
      /*  characterFlags.Add("selfMadeCube", false);

*/




        //add more things...



        //pregnancy low age 
        // downer - prone to negative emotions 
        //art ? 
        // 



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

        if (characterFlags["InLovewithspouseoffriend"] || characterFlags["pregnantbutnotfromspuceorbutloveintrest"]
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


}
//ask about what adoptioneventID means ? they adopted or havber adopted 

//single mother has love intrest - married - family id - mans father loves a daughter 
//step daughter is step step mother




/*
 
 
 things found as true so far: 
social life
has a best friend
flipflop 
teacherrole
familyperson
has alot of enemies
butcher role
advanced careers
works in alchaol 
pulluter role 
departed
general jobs 
widdowed but not greiving 
custodian 
healer
risk taker
pregnant but not engaged 

 



 */