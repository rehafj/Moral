using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<Character> characters;

    ///tart is called before the first frame update
    void Start()
    {
        characters.Add(new Character("Alfred", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        characters.Add(new Character("Tom", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        characters.Add(new Character("Bruce", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
