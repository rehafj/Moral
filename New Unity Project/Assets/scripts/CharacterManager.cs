using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //public List<Character> characters;
    public List<ConversationalCharacter> characters;

    ///tart is called before the first frame update
    void Start()
    {
        //old sysrtem 
        /*  characters.Add(new Character("Alfred", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
          characters.Add(new Character("Tom", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
          characters.Add(new Character("Bruce", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));*/

        characters.Add(new ConversationalCharacter(new int[] {10,7,60,20,10,80,100,75,8,0,10,14,45 }));
        characters.Add(new ConversationalCharacter(new int[] { 0, 78, 40, 45, 10, 80, 10, 75, 8, 60, 10, 14, 20 }));

    }


    // Update is called once per frame
    void Update()
    {
        
    }

 
}
