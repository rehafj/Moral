using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PostIt : MonoBehaviour
{
    public Sprite [] sprites;

    public InterestingCharacters intChar; //override this with next for when needed -

    public Viewer viwer;
    public string[] characterFlags;
    public string characterName;
    // Start is called before the first frame update
    void Start()
    {
        intChar = new InterestingCharacters();
        characterFlags = new string[5];
        //viwer = FindObjectOfType<Viewer>();
        sprites = Resources.LoadAll<Sprite>("TownieImages/");
       
    }

    public void SetUp(InterestingCharacters chara)
    {
        this.intChar = chara;
/*        Debug.Log(intChar.fullName + "hey hey hey");
*/        int i = 0;
        foreach (KeyValuePair<string, bool> k in intChar.characterFlags)
        {
            if (i <= 4)
            {
                //Debug.Log(k.Key + characterFlags.Length);
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
        viwer.gameObject.SetActive(true);
        Debug.Log("clicked on "+gameObject.name);
        Debug.Log(characterFlags[0]);
        string[] postItStrings = new string[characterFlags.Length];
        Debug.Log(intChar.fullName + "hey hey hey");

        int i = 0;
        foreach (string s in characterFlags)
        {
            postItStrings[i] =  intChar.GetStringTranslation(s); i++;
        }
        viwer.updateViewer(intChar.fullName, postItStrings, sprites[Random.Range(0, 4)]);
    }

    
    

}