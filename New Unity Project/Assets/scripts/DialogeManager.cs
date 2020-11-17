using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogeManager : MonoBehaviour
{
        //this is hardcoded for now but will change this structure later 
        // maybe xml, or inkl, or fungus...? 
        public List<Dialoug> dialougNodes;


        //this is messy - move UI stuff to its own script me! 
        public Text GuicharacterName;
        public Text GuidialougText;
        public Text[] optionText;


        public void Start()
        {        //for testing purp
            dialougNodes.Add(addingNodeManually("greeting", "hello there! ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
            dialougNodes.Add(addingNodeManually("oh no", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
            dialougNodes.Add(addingNodeManually("what", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));

        //Debug.Log(dialougNodes[0].GetDialougText());

        //manual for testing purp
        dialougNodes[0].setConnectedNodes(dialougNodes[1], dialougNodes[2]);
        GuidialougText.text = dialougNodes[0].GetDialougText();
        
        for(int i=0;i < optionText.Count()-1; i++)
        {
            if(dialougNodes[0].getNodeOptiontext(i)==null)
                return;
            optionText[i].text = dialougNodes[0].getNodeOptiontext(i);
        }
     

    }

    public void Update()
        {
        }

        public void addDialougNode(Dialoug dNode)
        {
            if (dNode == null)
                return;
            dialougNodes.Add(dNode);
        }


        //ugly!! maybe just pass in a list of floats for each of the values instead two core ones? 
        Dialoug addingNodeManually(string labe, string dText, float[] values)
        {

            Dialoug d = new Dialoug(
            Dialoug.DialougType.introduction,
            labe,
            dText,
            addValues(values));//need to add connected nodes --- 
            return d;
        }

        //duplicated method - unify this into value system instead of character and dialoug one 
        private List<KeyValuePair<Dialoug.DialougValue, float>> addValues(float[] values)
        {
            List<KeyValuePair<Dialoug.DialougValue, float>> listofValues =
                new List<KeyValuePair<Dialoug.DialougValue, float>>();

            int i = 0;
            foreach (Dialoug.DialougValue Dvalue in Enum.GetValues(typeof(Dialoug.DialougValue)))
            {
                listofValues.Add((new KeyValuePair<Dialoug.DialougValue, float>
                    (Dvalue, values[i])));
                i++;
            }

            return listofValues;
        }

        void runNode(Dialoug node)
        {

        }


        void getNextNode()
        {

        }


        //remove the node, go to next node.. 
    }
    //float[] values = { 10, 0, 0,0,0,0,0,70 };

