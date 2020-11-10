using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CharacterTrigger : MonoBehaviour
{
    [SerializeField]
    public Flowchart TomFlowChar;
    public Character character;
    public Dialoug dialog;

    public void Start()
    {
        Block block = new Block();
        //block.CommandList.Add(sayco);
        TomFlowChar.AddSelectedBlock(block);

    }

    public void OnClick()
    {
        //start dialoug 

    }



   

}
