using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialougGraphViewer : GraphView
{

    private readonly Vector2 defaultnodeSize = new Vector2(x:150, y:200);
  public DialougGraphViewer()
    {

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());


       AddElement( gernerateEntryPointNode());
    }
     Port generatePort(DialougNode node, Direction portDirection ,Port.Capacity capacity = Port.Capacity.Single )//direction ingot is poinput output - capacity is single or multiple nodes ?
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));//typig can be anything but not used for dialoug ( as it iis not a shader...etc) 
    }


    private DialougNode gernerateEntryPointNode()
    {
        //get new instance of the dialoug node 
        var node = new DialougNode //creatinhg a new instance of a dialoue node 
        {
            title = "start",
            nodeID = Guid.NewGuid().ToString(),
            text = "Entry point",
            startPoint = true
        };
        var generatedport = generatePort(node, Direction.Output);
        generatedport.portName = "Next"; //why didn't thia chaneg the name?
        node.outputContainer.Add(generatedport);

        
        //refresh the window 
        node.RefreshExpandedState();
        node.RefreshPorts();

        //set the node's postion 
        node.SetPosition(new Rect(x: 100, y: 100, width: 100, height: 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialougNode(nodeName));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        //base class ports 
        ports.ForEach(funcCall: (port)=>
            {

                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            //var portView = port;
        });

        return compatiblePorts;


    }

    public  DialougNode  CreateDialougNode( string nodeNmae)
    {
        var dialougNode = new DialougNode
        {
            title = nodeNmae,
            text = nodeNmae, //change this to the actual dialoug once I fgiigure uit out 
            nodeID = Guid.NewGuid().ToString()
        };
        //ok so this node is the inpt
        var inputport = generatePort(dialougNode, Direction.Input, Port.Capacity.Multi);
        inputport.portName = "input";
        dialougNode.inputContainer.Add(inputport);

        var button = new Button(clickEvent: () => {

            AddChoicePort(dialougNode);
            });
        button.text = "new option ";
        dialougNode.titleContainer.Add(button);

        //update visual  info 
        dialougNode.RefreshExpandedState();
        dialougNode.RefreshPorts();
        dialougNode.SetPosition(new Rect(position: Vector2.zero, defaultnodeSize));

        return dialougNode;
    }

    private void AddChoicePort(DialougNode dialougNode)
    {
        var generatedPort = generatePort(dialougNode, Direction.Output, Port.Capacity.Single);
        var outportCount = dialougNode.outputContainer.Query(name: "connector").ToList().Count;
        var outportName = $"Choice{outportCount}"; //look up this expression, I seem to recall things like this in python... 
        dialougNode.outputContainer.Add(generatedPort);
        dialougNode.RefreshExpandedState();
        dialougNode.RefreshPorts();
    }
}
