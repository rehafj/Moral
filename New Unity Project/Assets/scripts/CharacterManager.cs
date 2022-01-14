using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }



    int characterCount = 3;
    //public List<Character> characters;
    //public  List<ConversationalCharacter> characters;

    public List<GameObject> ourConversationalCharacters;

    bool ActorInScene = false;
    public GameObject ActingCubes;
    public NavigationControl ActingCubeNavigation;

    int currentcharacterIndexer = 0;
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

        ActingCubes = Resources.Load("prefabs/CNPCPrefab") as GameObject;
        setUp();
    }





    public List<InterestingCharacters> getCurrentBackgroundCharacterList()
    {
        return DialogeManager.Instance.conversedAboutCharectersList;
    }

  

    //new thing here - check if it bugs itout~ 
    private void setUp() ///this is hard coded but perhaps make values random in other runs 
    {
        for (int i = 0; i <= characterCount; i++)
        {
            //ConversationalCharacter c = new ConversationalCharacter(setCharacterName());
            GameObject t = Instantiate(ActingCubes, new Vector3(23.99f, -7.08f, 0f), Quaternion.identity);

            if (i == 0)
            {
                t.SetActive(true);

            } else
            {
                t.SetActive(false);

            }
            // t.AddComponent<ConversationalCharacter>();
            // Debug.Log("value of added compnenet!" + t.GetComponent<ConversationalCharacter>().ConversationalNpcName);

            ourConversationalCharacters.Add(t);
            //characters.Add(new ConversationalCharacter(setCharacterName()));
        }



    }

    //change this to startTheMarrch! or osmething like thaT :/
    public void instantiateCube()
    {
        if (!ActorInScene)
        {
            GameObject t = Instantiate(ActingCubes, new Vector3(23.99f, -7.08f, 0f), Quaternion.identity);

            ActorInScene = true;
        }
        else
        {

            //DestroyImmediate(ActingCube,true);
            GameObject t = Resources.Load("prefabs/CNPCPrefab") as GameObject;
            ActingCubes = t;
            Instantiate(ActingCubes, new Vector3(23.99f, -7.08f, 0f), Quaternion.identity);
            ActorInScene = false;

        }


    }

    public void startTheMarch()
    {
        if (!ActorInScene)
        {
            ourConversationalCharacters[currentcharacterIndexer].SetActive(true);
            ActorInScene = true;
        }
        else //todo otter --- update this to turn it on and delay when it's called in the other script - check if this woerks! 
        { //startTheMarch needs to be cvalled before changing cnpcs... 
            currentcharacterIndexer++;
            ourConversationalCharacters[currentcharacterIndexer].SetActive(true);
            ActorInScene = false;

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
