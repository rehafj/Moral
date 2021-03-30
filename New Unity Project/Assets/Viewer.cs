using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Viewer : MonoBehaviour
{

    public GameObject viewer;
    public  Text characterName;
    public Text[] flags;
    public Image spriteViewer;
    // Start is called before the first frame update

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void updateViewer(string name, string [] flag, Sprite img)
    {
        characterName.text = name ;
        int i = 0;
        foreach (Text t in flags)
        {
            t.text = flag[i];
            i++;
        }
        spriteViewer.sprite = img;
    }

    public void CloseViewer()
    {
        gameObject.SetActive(false);
    }
}
