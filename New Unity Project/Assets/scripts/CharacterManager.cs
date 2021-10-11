using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }



    int characterCount = 6;
    //public List<Character> characters;
    public  List<ConversationalCharacter> characters;
    private List<string> firstNamesList = new List<string> { "Acute", "Arc", "Conic", "Cy", "Vert", "Point", "Hex", "Polly" };
    private List<string> lastNamesList = new List<string> { "Segment", "Strip", "Gon", "Angle", "Metric", "Millimetre ", "Decimal" };

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<InterestingCharacters> getCurrentBackgroundCharacterList()
    {
        return DialogeManager.Instance.conversedAboutCharectersList;
    }

    string setCharacterName()
    {
        string name = firstNamesList[UnityEngine.Random.Range(0, firstNamesList.Count -1)] + " " +
              lastNamesList[UnityEngine.Random.Range(0, lastNamesList.Count -1)];

        return name;
    }
    void Start()
    {
        setUp();

    }


    //new thing here - check if it bugs itout~ 
    private void setUp() ///this is hard coded but perhaps make values random in other runs 
    {
        for(int i = 0; i <= characterCount; i++)
        {
            //ConversationalCharacter c = new ConversationalCharacter(setCharacterName());
            characters.Add(new ConversationalCharacter(setCharacterName()));
        }
    
       

    }





}







//old sysrtem 
/*  characters.Add(new Character("Alfred", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
  characters.Add(new Character("Tom", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
  characters.Add(new Character("Bruce", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));*/
/*
        characters.Add(new ConversationalCharacter(new int[] {10,7,60,20,10,80,100,75,8,0,10,14,45 }));
        characters.Add(new ConversationalCharacter(new int[] { 0, 78, 40, 45, 10, 80, 10, 75, 8, 60, 10, 14, 20 }));
    private void setUp() ///this is hard coded but perhaps make values random in other runs 
    {
      /*  characters.Add(new ConversationalCharacter("Tim", new ConversationalCharacter.RatingVlaues[] {
            ConversationalCharacter.RatingVlaues.High,  ConversationalCharacter.RatingVlaues.Low,  ConversationalCharacter.RatingVlaues.High,
            ConversationalCharacter.RatingVlaues.High, ConversationalCharacter.RatingVlaues.Low, ConversationalCharacter.RatingVlaues.High,
            ConversationalCharacter.RatingVlaues.Low, ConversationalCharacter.RatingVlaues.High, ConversationalCharacter.RatingVlaues.High,
            ConversationalCharacter.RatingVlaues.Mid, ConversationalCharacter.RatingVlaues.High, ConversationalCharacter.RatingVlaues.High,
            ConversationalCharacter.RatingVlaues.Low}, "FamilyPerson"));
         */

//characters.Add(new ConversationalCharacter("Tom"));
        

   // }
//*/
