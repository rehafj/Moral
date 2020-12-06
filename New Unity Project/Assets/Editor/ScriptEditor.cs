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
        int i = 0;
        foreach(ConversationalCharacter c in ((CharacterManager)target).characters)
        {
            EditorGUILayout.LabelField("conversational character" + i);//later add names...etc 
            i++;
            EditorGUILayout.Space();
            foreach (var p in c.ConvCharacterMoralFactors)              
            {
                EditorGUILayout.IntField(p.Key, p.Value);
/*                EditorGUILayout.LabelField(p.Key + " :  " + p.Value);
*/            }

        }
     
    }

}


/*
 
 
 
 foreach (var p in ((ConversationalCharacter)target).ConvCharacterMoralFactors)
            {
                EditorGUILayout.LabelField(p.Key + ":" + p.Value);
            }
 
 */