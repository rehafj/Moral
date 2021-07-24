using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BackgroundCharacter  :  MonoBehaviour
{//org lists later and move them to their own objects 
    public JsonLoader jsn;

    public List<InterestingCharacters> characters = new List<InterestingCharacters>();
    public static List<InterestingCharacters> filtredCharacters = new List<InterestingCharacters>();

    Dictionary<int, int> bestfrinedList;
    Dictionary<int, List<int>> LoveIntrests;

    // Start is called before the first frame update
    void Start()
    {
        JsonLoader jsn = gameObject.GetComponent<JsonLoader>();
        setInitialIntrestingCharacters();
        flagInterestingCharactersWithOccupations();
        flagInterestingCharactersWithNestedFlags();
        flagCharactersWithPersonalityPossibilites();
        filterCharacters();
        printAllCharacterValues();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            /* int x = returnHighestOpenValueID();
             Debug.Log("!!!" + returnAcharacterName(x));*/  //debug checks! 
                                                            //findBestFriends();
                                                            // findEnemiesOFBestFriends();
                                                            // loveIntrest();
                                                            //returnIntoList(jsn.backgroundcharacters);
        


        }
    }

    public  List<InterestingCharacters> GetFiltredCharerList()
    {
        return filtredCharacters;
    }

    public void filterCharacters() {
        
        foreach( InterestingCharacters c in characters)
        {
            int trueCounter = 0;
            foreach (bool entry in c.characterFlags.Values)
            {
                if(entry == true)
                {
                    trueCounter++;
                }
            
            }
            if (trueCounter > 4) //if 4 gives us a total of 54 characters... 
            {
                filtredCharacters.Add(c);

            }
        }
    }


    private void printAllCharacterValues()
    {
        foreach(InterestingCharacters c in filtredCharacters)
        { //in story streucture bring outr flags and check if a and b are true about a character how does my cnpc react ? 
            /*  if(c.characterFlags["InLoveWithAnothersspuce"] ==true && c.characterFlags["WillActOnLove"] == true)
              {
                  Debug.Log("will act on love! even if it hurts ppl :0 "); //perhaps the character should have their own field called true to your heart 
              }*///wow 45 ppl will love another's spuce out of them 7 will act on it :0 

           // Debug.Log("the characte +r" + c.fullName);
                foreach (KeyValuePair<string, bool> kvp in c.characterFlags)
            {
                if(kvp.Value == true)
                {
                   // Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));

                }

            }
        }
    }

    void setInitialIntrestingCharacters()//i is the main one 11111
    {
        InterestingCharacters character = new InterestingCharacters();
        for (int i = 0; i < jsn.backgroundcharacters.Count; i++)
        {
            character.personID = jsn.backgroundcharacters[i].id;
            character.fullName = jsn.backgroundcharacters[i].firstName + " " + jsn.backgroundcharacters[i].lastName;
            character = new InterestingCharacters();

            //checks for a single character flags
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].departureEventID, "departed", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].kids.Length, "familyPerson", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].friendsId.Length, "socialLife", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].enemiesId.Length > 1, "hasalotofenemies", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].isCollegeGraduate, "graduate", character); 
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].widowed && (!jsn.backgroundcharacters[i].grieving), "widowedbutnotgrieving", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].adult && (!jsn.backgroundcharacters[i].inWorkForce), "adultbutnotworking", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].neighborsID.Length > 0 && (jsn.backgroundcharacters[i].friendsId.Length >2), "loner", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].money > 5, "IsWealthy", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].movesEventId.Length > 1 && !jsn.backgroundcharacters[i].movesEventId.Contains(-1), "MovesAlot", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].coworkersId.Intersect(jsn.backgroundcharacters[i].immediateFamilyId).Any(), "worksWithFamily", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].layOffsEventId.Length > 1 && !jsn.backgroundcharacters[i].movesEventId.Contains(-1), "getsFiredAlot", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].age < 45 && jsn.backgroundcharacters[i].isRetired, "RetiredYoung", character);
            checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].divorcesEventId.Length >= 3, "DevorcedManyPeople", character);


            /*  
             


               checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].death != 1 && !jsn.backgroundcharacters[i].isRetired, "DiedBeforeRetired", character);
   */
            for (int j = 0; j < jsn.backgroundcharacters.Count - 1; j++)
            {

                if (i != j)
                {

                    checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].bestFriendiD,
                        jsn.backgroundcharacters[j].id, "hasAbestFriend", character);


                    checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].age < 
                     (jsn.backgroundcharacters[j].age - 7 ), "marriedSomoneOlder", character);

                    checkIfAnIntconditionIsMet(jsn.backgroundcharacters[i].spouse == jsn.backgroundcharacters[j].id &&
                      jsn.backgroundcharacters[i].loveInterest != jsn.backgroundcharacters[i].spouse
                        && jsn.backgroundcharacters[i].money <= 10 &&
                            (jsn.backgroundcharacters[j].age - jsn.backgroundcharacters[i].age) >= 10 && (jsn.backgroundcharacters[j].age - jsn.backgroundcharacters[i].age) > 0, "marriedForLifeStyleNotLove", character);


            

                    checkLoveTraingle(jsn.backgroundcharacters[i].loveInterest, jsn.backgroundcharacters[j].spouse,
                                           jsn.backgroundcharacters[j].significantOther, jsn.backgroundcharacters[j].id,
                                            "InLoveWithAnothersspuce", character);

                    checkLoveTraingle(jsn.backgroundcharacters[i].id,
                                           jsn.backgroundcharacters[j].id, jsn.backgroundcharacters[i].spouse, "InLovewithspouseoffriend", character);

                    
                    checkifbestfriendwithanenmey(jsn.backgroundcharacters[i].bestFriendiD, jsn.backgroundcharacters[j].id,
                        jsn.backgroundcharacters[i].friendsId, jsn.backgroundcharacters[j].enemiesId,
                        "friendwithabestfriendsenemy", character);

                  


                    
                }
            }
            characters.Add(character);
        }
    }

    void flagInterestingCharactersWithOccupations() ////22222222
    {
        foreach (InterestingCharacters character in characters)
        {
            int i = 0;
            foreach (Occupations oc in jsn.listofOccupations)
                {
                
                if (character.personID == oc.personID)
                {  // to check if a character has multiplejobs in diffrient fields 
                    if (oc.type == "Farmhand" || oc.type == "Farmer")
                    { character.characterFlags["butcherRole"] = true; i++; }
                    if (oc.type == "Brewer" || oc.type == "Distiller")
                    { character.characterFlags["WorksInAlcohol"] = true; i++; }
                    if (oc.type == "Teacher" || oc.type == "Principal")
                    { character.characterFlags["Teachingrole"] = true; i++; }
                    if (oc.type == "Cooper" || oc.type == "Miner")
                    { character.characterFlags["polluterRole"] = true; i++; }
                    if (oc.type == "Secretary" || oc.type == "Cashier")
                    { character.characterFlags["generalJobs"] = true; i++; }
                    if (oc.type == "Manager" || oc.type == "Engineer")
                    { character.characterFlags["advancedCareer"] = true; i++; }
                    if (oc.type == "HotelMaid" || oc.type == "Janitor" || oc.type == "Groundskeeper")
                    { character.characterFlags["CustodianJobs"] = true; i++; }
                    if (oc.type == "Firefighter")
                    { character.characterFlags["riskTaker"] = true; i++; }
                    if (oc.type == "Nurse")
                    { character.characterFlags["healerRole"] = true; i++; }
                    if (character.characterFlags["worksWithFamily"] == true && oc.hiredAsFavor)
                    {
                        character.characterFlags["hiredByAFamilymember"] = true;
                    }
                    if (oc.level >= 3) 
                    { character.characterFlags["hardWorker"] = true; }

               
                    if (i > 2) { character.characterFlags["flipflop"] = true; //character has 3 jobs! 
                    }
                    character.Lastoccupation = oc.type;
                }
            }
        }
    }
    void flagInterestingCharactersWithNestedFlags() /////3
    {
        foreach (InterestingCharacters character in characters)
        {
            if (character.characterFlags["socialLife"] == false && character.characterFlags["MovesAlot"])
            {
                character.characterFlags["SusMovments"] = true;
            }

            if (character.characterFlags["hardWorker"] && character.characterFlags["IsWealthy"])
            {
                character.characterFlags["selfMadeCube"] = true;

            }
            if (character.characterFlags["adultbutnotworking"] && character.characterFlags["IsWealthy"])
            {
                character.characterFlags["notworkingandrich"] = true;

            }
            if(character.characterFlags["InLoveWithAnothersspuce"] && character.characterFlags["WillActOnLove"] && !character.isLoverPartner())
            {
                character.characterFlags["leftFotLoveIntrest"] = true;

            }


        }

    }

    void flagCharactersWithPersonalityPossibilites()//handles things like will act on love so later we can compare if ---4 
    {
        foreach (InterestingCharacters character in characters)
        {
            foreach (TwoniePersonalities personalityOnFive in jsn.listOfPersonalities)
            {
                


                if (character.personID == personalityOnFive.personID)//if its the same person i am looking at
                {


                     checkIfAnIntconditionIsMet(character.characterFlags["InLoveWithAnothersspuce"]  && (personalityOnFive.lowNeuroticism 
                            || personalityOnFive.highAgreeableness), "WillActOnLove", character);
                    /// did this instead of charge 

                    checkIfAnIntconditionIsMet(character.characterFlags["IsWealthy"] &&
                        personalityOnFive.lowAgreeableness, "IsRichButNotGenrous", character);

                    checkIfAnIntconditionIsMet(character.characterFlags["selfMadeCube"] && character.characterFlags["IsWealthy"] &&
                 personalityOnFive.highConscientiousness, "selfMadeCubeByDedication", character);

                    checkIfAnIntconditionIsMet(personalityOnFive.lowExtroversion, "reserved", character); //this needs to stay here cz order of execution - need to refactor most of this :/



                    checkIfAnIntconditionIsMet(character.characterFlags["socialLife"] &&
                        !character.characterFlags["reserved"] &&
                personalityOnFive.highOpennes, "likesToDate", character); //need to update this with perhaps a love chrghe or will act on love...
                    checkIfAnIntconditionIsMet(character.characterFlags["departed"] &&
                                       personalityOnFive.highEextroversion, "AdventureSeeker", character);
                    checkIfAnIntconditionIsMet(personalityOnFive.lowNeuroticism &&
                                     personalityOnFive.highAgreeableness, "liklyToHelpTheHomeless", character);

                    checkIfAnIntconditionIsMet(character.characterFlags["loner"] &&
                     personalityOnFive.lowExtroversion, "isolated", character);
                    checkIfAnIntconditionIsMet(character.characterFlags["butcherRole"] &&
                                  personalityOnFive.highAgreeableness, "ButcherButRegretful", character);
                    checkIfAnIntconditionIsMet(personalityOnFive.lowOpenness, "conventional", character); // need to refine this 

                    checkIfAnIntconditionIsMet(personalityOnFive.highOpennes && character.characterFlags["MovesAlot"]
                               , "likedToExperinceCulture", character); //this needs to stay here cz order of execution - need to refactor most of this :/


                    checkIfAnIntconditionIsMet(personalityOnFive.highOpennes && personalityOnFive.highEextroversion &&
                                        personalityOnFive.highAgreeableness || character.characterFlags["likedToExperinceCulture"] && personalityOnFive.highAgreeableness,
                                        "supportsImmigration", character); //this needs to stay here cz order of execution - need to refactor most of this :/
                    checkIfAnIntconditionIsMet(character.characterFlags["selfMadeCube"] && character.characterFlags["IsRichButNotGenrous"] &&
                                         personalityOnFive.highConscientiousness, "doesNotGiveToThoseInNeed", character);
                    checkIfAnIntconditionIsMet(character.characterFlags["hasalotofenemies"] && personalityOnFive.highAgreeableness, "TooTrustingOfEnemies", character);

                    checkIfAnIntconditionIsMet(character.characterFlags["hiredByAFamilymember"] &&
                                                   personalityOnFive.highAgreeableness, "exploteative", character);



                }
            }

        }
    }
       
    private void checkifbestfriendwithanenmey(int id, int bestFriendiD, int[] personFriend,  int[] enemiesOfFriend ,  string flag, InterestingCharacters character)
     {       //i is best friends with j and 
        
            
        foreach( int enemyID in enemiesOfFriend)
         {
            if (id == bestFriendiD && personFriend.Contains(enemyID))
            {
                character.characterFlags[flag] = true;
                break;
            }

         }
    }
    

    //    void checkIfAnIntconditionIsMet<T>(T Item1, T Item2, string flag, InterestingCharacters character) where T : class


    void checkIfAnIntconditionIsMet(int Item1, string flag, InterestingCharacters character)
    {
        if (Item1!=-1 && Item1 !=0)//do all of the checks 
        {
            character.characterFlags[flag] = true;
            //Debug.Log("somone has this flag true "+ flag); //SOCIAL LIFE, FAMILY PERSON AND DEPARTED ALL ARE TURE 

        }
    }
    void checkIfAnIntconditionIsMet(bool item1, string flag, InterestingCharacters character)
    {
        if (item1)//do all of the checks 
        {
            character.characterFlags[flag] = true;
            //Debug.Log("somone has this flag true " + flag); //ALOT OF ENEMIES, WIDDOWED BUT NOT GRIEVING AND PREGNANT BUT NOT ENGAGED ALL TRUE 
        }
    }

    void checkIfAnIntconditionIsMet(int Item1, int Item2, string flag, InterestingCharacters character) 
    {
        if (Item1 == Item2)//do all of the checks 
        {
            character.characterFlags[flag] = true;
            //Debug.Log("somone has this flag true " + flag);
        }
    }
    void checkLoveTraingle(int person, int bestfriend, int spuceofperson, string flag, InterestingCharacters character)
    {
        if (checkForNullValues(person, spuceofperson, bestfriend))
        {
            if (person == bestfriend && bestfriend == spuceofperson)//do all of the checks 
            { //check charge! 
                character.characterFlags[flag] = true;
                Debug.Log("somone has this flag true " + flag); //NO ONE HAS A LOve traingle - check this logic out 
            }
            character.SetLoveTraingleValues(spuceofperson, bestfriend);
        } 
    }

    /// <summary>
    /// check if the main person's love intrest is another person's (J)  spouce and 
    /// J's siignificantOther is their spouce
    /// </summary>
    void checkLoveTraingle(int IMainLoveInrestID, int JSpouce, int JsiginifcantOther, int jID , string flag, InterestingCharacters character)
    {
        if (checkForNullValues(IMainLoveInrestID, JSpouce, JsiginifcantOther))
        {
            
            if (IMainLoveInrestID == JSpouce && JsiginifcantOther == JSpouce)
            { 
                character.characterFlags[flag] = true; 
                //Debug.Log("DOES THIS HAPPEN?");
                character.SetLoveTraingleValues(IMainLoveInrestID, jID);

            }
            
        }
    }
    //DO A METHOD THAT CHECKS TEH CHARGE OF AN ITEM --- 
    bool checkForNullValues(int i, int j, int k)
    {
        return (i != -1 && j != -1 && k != -1);
    }
    //perhaps make this generic 
    private KeyValuePair<int,int> returnComparedItemsPairID(int selection, int i, int j) {


        switch (selection)
        {
            case (1):
                
                return returnBestFriendsRecuprical(i, j);// add all comparrison arguments here - kibnda janky, but saves for now some repetion --- or use func<> 
                
            default: return returnBestFriendsRecuprical(i, j);
              

        }
    }

    private Dictionary<int, int> returnIntoList<T>(List<TownCharacter> aList, int comparrisonID)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        dic.Add(1, 1);
        for (int i = 0; i < aList.Count; i++)
        {
            for (int j = 0; j < aList.Count - 1; j++)
            {
                if (i != j && checkForNullValues(i, j))
                {

                    //dic.Add(returnBestFriendsRecuprical(i, j)); //(returnComparedItemsPairID(comparrisonID, i, j));// if the selected comparrison is true 


                }
                Debug.Log(aList[i].spouse);
            }
        }
        return dic;

    }
    private void findEnemiesOFBestFriends() { }


    int returnHighestOpenValueID() //make this genertic for all people ---
    {
        List<double> opennesVlues = new List<double>() ;
        int id=0;
        double maxValue=0;

        foreach (TwoniePersonalities townie in jsn.listOfPersonalities)
        {
            opennesVlues.Add(townie.opennessToExperience);

            if (townie.opennessToExperience >= opennesVlues.Last())
            {
                maxValue = townie.opennessToExperience;
                id = townie.personID;
            }
        }
        Debug.Log("the person with the max value is; " + id + "with a valye of " + maxValue);
        return id;

    }//end of return highOpennes

    string returnAcharacterName(int id)
    {
        foreach(TownCharacter townie in jsn.backgroundcharacters)
        {
            if(id== townie.id)
            {
                return (townie.firstName + " " + townie.lastName);
            }
        }
        return "no id found ";

    }
    KeyValuePair<int,int> returnBestFriendsRecuprical(int i, int j)
    {
        if ((jsn.backgroundcharacters[i].id == jsn.backgroundcharacters[j].bestFriendiD) &&
            (jsn.backgroundcharacters[i].bestFriendiD == jsn.backgroundcharacters[j].id))
        {             
            return new KeyValuePair<int, int>(+jsn.backgroundcharacters[i].id, jsn.backgroundcharacters[j].id);
        }
        return new KeyValuePair<int, int>(-1, -1);

    }//end of method 

    void printBestFriendsRecuprical()
    {
        Debug.Log("any best friends?");
        for (int i = 0; i < jsn.backgroundcharacters.Count; i++)
        {
            for (int j = 0; j < jsn.backgroundcharacters.Count - 1; j++)
            {
                if ((jsn.backgroundcharacters[i].id == jsn.backgroundcharacters[j].bestFriendiD) &&
                     (jsn.backgroundcharacters[i].bestFriendiD == jsn.backgroundcharacters[j].id))
                {
                    Debug.Log("this person:  " + jsn.backgroundcharacters[i].id
                        + " and person :" + jsn.backgroundcharacters[j].id +
                        "are best friends~ ---- recepriocal relationships :d");
                }
   
                if (jsn.backgroundcharacters[i].id == jsn.backgroundcharacters[j].bestFriendiD)
                {
                    Debug.Log("this person:  " + jsn.backgroundcharacters[i].id
                        + "IS THE BEST FRIEND OF " + jsn.backgroundcharacters[j].id +
                        "may not be recepriocal ");
                }
             
             }
        }
     }//end of method 

    void loveIntrest()
    {
        for (int i = 0; i < jsn.backgroundcharacters.Count; i++)
        {
            if (jsn.backgroundcharacters[i].loveInterest != jsn.backgroundcharacters[i].spouse)
            {
                Debug.Log(jsn.backgroundcharacters[i].firstName + "married to the person that is not their love intrest ");
            }
            for (int j = 0; j < jsn.backgroundcharacters.Count - 1; j++)
            {
                // Debug.Log("checking elemnt [" + i + "]with [" + j+"]");
                if (i != j)// stop comparing the same elements 
                {
                    if (checkForNullValues(i, j))
                    { 
                        if ((jsn.backgroundcharacters[i].loveInterest == jsn.backgroundcharacters[j].spouse) &&
                            (jsn.backgroundcharacters[j].significantOther == jsn.backgroundcharacters[j].spouse))
                        {
                            Debug.Log(jsn.backgroundcharacters[i].firstName + "has a thing for" +
                                 jsn.backgroundcharacters[j].firstName + "s spouce !!!!!");
                        }
                    }
                }
               
            }
        }
    }
    //checkForNullValues(i, j)
    private bool checkForNullValues(int i, int j)
    {//perhaps do this with find or contains 

        bool x = (jsn.backgroundcharacters[i].loveInterest != -1 &&
        jsn.backgroundcharacters[i].significantOther != -1 &&
        jsn.backgroundcharacters[j].significantOther != -1 &&
        jsn.backgroundcharacters[j].spouse != -1);

        return x;
    }
}



/*
 * 
 * 
 * 
 * 
 * 
 * 
 *  character.SetLoveTraingleValues(jsn.backgroundcharacters.
                           Find(x=>x.id == jsn.backgroundcharacters[j]), ))
 */