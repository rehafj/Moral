using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostItManager : MonoBehaviour
{

    public List<InterestingCharacters> postItCharacterList = new List<InterestingCharacters>();

    BackgroundCharacter bgChar;
    public DialogeManager dm;

     public List<PostIt> postItPanels;


    void Start()
    {       
        dm = FindObjectOfType<DialogeManager>();
        Invoke("setUpPanels", 2);

    }

    void setUpPanels()
    {
        int i = 1;
        foreach( Transform g in gameObject.GetComponentInChildren<Transform>())
        {
            g.gameObject.name = dm.conversedAboutCharectersList[i].fullName + "post it";
            i++;
        }
         i = 0;
        foreach (PostIt g in postItPanels)
        {
            InterestingCharacters character = dm.conversedAboutCharectersList[i];
            g.SetUp(character);
            i++;
        }

    }

    public void UpdatePostIts()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(dm.conversedAboutCharectersList.Count);

        }
    }

    //TODO need tot move these methods into one script, both dm and this uses them in a simmilar fashion--- 
  

}
