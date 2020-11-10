using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialougeGraph : EditorWindow
{
    private DialougGraphViewer graphViewer;
  [MenuItem("Graph/ Dialoge Grap")]//cool did not know this was a thing - can call a method this way! 
    public static void OpenDiaWindow() // only y possibkle opn menu if this is static 
    {
        var window = GetWindow<DialougeGraph>();
        window.titleContent = new GUIContent(text: "dialoge graph"); ;

    }

    private void OnEnable()
    {
        constructGraph();
        generateToolBar();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphViewer);
    }//help

    void constructGraph()
    {
        graphViewer = new DialougGraphViewer
        {
            name = "Dialoug graph"
        };
        //stretch it to fgill window size 
        graphViewer.StretchToParentSize();
        //add to the editor with root visual element 
        rootVisualElement.Add(graphViewer); //ads to the editor window 
    }

    private void  generateToolBar()
    {
        var toolBar = new Toolbar();
        var nodeCreateButton = new UnityEngine.UIElements.Button(clickEvent:() => {

            graphViewer.CreateNode("dialouge node"); //add element in this method adds the node from the creation menu on graphviewer 
            

        });

        nodeCreateButton.text = "create node";
        toolBar.Add(nodeCreateButton);

        rootVisualElement.Add(toolBar);

    }
}
