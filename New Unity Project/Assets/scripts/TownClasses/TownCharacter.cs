using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TownCharacter 
{
    public int id;
    public string type;
    public string town;
    public int biologicalMotherID;
    public int motherID;//-1 if null 
    public int biologicalFatherID;
    public int fatherID;
    public int[] parentsID;//id of parents 
    public int age;
    public bool adult;
    public bool inWorkForce;
    public bool isMale;
    public bool isFemale;
    public bool isAlive;
    public bool iSattractedToMen;
    public bool iSattractedToWomen;
    //Personality personality; load it in alone 
/*    public struct routine
    {
        int personID;
        bool working;
        string occasion;
    }*/
    public string firstName;
    public string lastName;
    public int[] immediateFamilyId;
    public int[] grandparentsId;
    public int[] auntsId;
    public int[] unclesId;
    public int[] siblingsId;
    public int[] kids;
    public int[] bioParents;
    public int[] bioImmediateFamily;
    public int spouse;
    public bool widowed;
    public int[] sexualPartnersid;
    public int[] acquaintancesId;
    public int[] friendsId;
    public int[] enemiesId;
    public int[] neighborsID;
    public int[] coworkersId;
    public int[] formerCoworkersID;
    public int bestFriendiD;
    public int worstEnemyID;
    public int loveInterest;
    public int significantOther;
    public double chargeOfBestFriend;
    public double chargeOfWorstEnemy;
    public double sparkOfLoveInterest;
    public bool isPregnant;
    public int impregnatedById;
    public int adoptionEventId;
    public int marriageEventId;
    public int[] marriagesEventId;
    public int[] divorcesEventId;
    public int[] adoptionsEventId;
    public int[] movesEventId;
    public int[] layOffsEventId;
    public int[] nameChangesEventId;
    public int[] buildingCommissionsEventId;
    public int[] homePurchasesEventId;
    public int retirementEventId;//-1 = null 
    public int departureEventID;
    public int death;
    public double money;
    // Occuptions[] occuptions; TODO load it in sepretluy 
    // int[] occuptions;//rework this --- 
    public bool isRetired;
    public bool isCollegeGraduate;
    public bool grieving;
    public bool weddingRingOnFinger;

    //remove routine from json --- 
}
