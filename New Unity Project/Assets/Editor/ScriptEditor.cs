using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// tutorial followed for unity editor stuff and modded https://www.youtube.com/watch?v=kLvZJOcTyOk
[CustomEditor(typeof(CharacterManager))]
public class ScriptEditor : Editor
{

    public override void OnInspectorGUI()
    {
        foreach(GameObject gb in ((CharacterManager)target).ourConversationalCharacters)
        
        {
            EditorGUILayout.LabelField("conversational character :\t" + gb.GetComponent<ConversationalCharacter>().ConversationalNpcName);//later add names...etc 
                                                                                                 // EditorGUILayout.LabelField("GameObject prefab:\t" + c.gameobj);//later add names...etc 

            // i++;
            EditorGUILayout.Space();
            foreach (var p in gb.GetComponent<ConversationalCharacter>().ConvCharacterMoralFactors)
            {
                EditorGUILayout.LabelField(p.Key);
                EditorGUILayout.EnumPopup(p.Value);
                /*                EditorGUILayout.LabelField(p.Key + " :  " + p.Value);
                */
            }

        }
     







        //int i = 0;
        /*   foreach(ConversationalCharacter c in ((CharacterManager)target).ourConversationalCharacters)
             {
               EditorGUILayout.LabelField("conversational character :\t" + c.ConversationalNpcName);//later add names...etc 
              // EditorGUILayout.LabelField("GameObject prefab:\t" + c.gameobj);//later add names...etc 

               // i++;
               EditorGUILayout.Space();
               foreach (var p in c.ConvCharacterMoralFactors)              
               {
                   EditorGUILayout.LabelField(p.Key);
                   EditorGUILayout.EnumPopup( p.Value);
   *//*                EditorGUILayout.LabelField(p.Key + " :  " + p.Value);
   *//*            }

           }*/

    }

}

