using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PostIt : MonoBehaviour
{
    public Sprite [] sprites;

    public InterestingCharacters intChar = new InterestingCharacters(); //override this with next for when needed -

    public Viewer viwer;
    public string[] characterFlags;
    public string characterName;
    // Start is called before the first frame update
    void Start()
    {
        characterFlags = new string[5];
        viwer = FindObjectOfType<Viewer>();
        sprites = Resources.LoadAll<Sprite>("TownieImages/");
       
    }

    public void SetUp(InterestingCharacters chara)
    {
        intChar = chara;
        int i = 0;
        foreach (KeyValuePair<string, bool> k in intChar.characterFlags)
        {
            if (i <= 4)
            {
                Debug.Log(k.Key + characterFlags.Length);
                if (k.Value)
                {
                    characterFlags[i] = k.Key;
                    //Debug.Log(characterFlags[i]);
                    i++;
                }
            
            }
        
        }
        Debug.Log(intChar.fullName);

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void clickedMe()
    {
        Debug.Log("clicked on ");
    }
    void OnMouseDown()
    { 
        Debug.Log("clicked on "+gameObject.name);
        Debug.Log(intChar.fullName);
        Debug.Log(characterFlags[0]);

        viwer.updateViewer(intChar.fullName, characterFlags, sprites[Random.Range(0, 4)]);
    }


}