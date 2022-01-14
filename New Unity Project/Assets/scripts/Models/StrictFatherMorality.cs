using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StrictFatherMorality : MoralModels
{

    // public bool isCentral; maybe make this into az thing to hold values

    Dictionary<string, bool> fatherSchemas = new Dictionary<string, bool>();

    public struct Argument {

        public string pattern;
        public string matchingPattern;
        public string schema;
        public string expandedArgument;
        public bool modelCitizen;
    }
    public Argument CurrentArgument;
    public bool isPragmatic;

    List<string> NPCfmHighValues = new List<string>() //what the NPC looks for - ig its not herte NPC looks for low 
    {
       "BTrueTYourHeart","Teetotasler","ProHiringFamily",
        "Shapesarenothingifnotsocial","FriendsAreTheJoyOFlife",
        "AnimalLover","Enviromentalist","SchoolIsCool","FamilyPerson",
        "AselfMadeShapeWeAspireToBe","ImmagretsWeGetTheJobDone","SupportingComunities"
        ,"LoverOfRisks","LandISWhereThehrtIS","youthAreTheFuture","WeLiveForSpontaneity",
        "AnAdventureWeSeek","MoneyMaker","CarrerAboveAll"

    };

    List<string> NPCSVfORbOTHhIGHaNDlOW = new List<string>() { };
    // IF ITS PART THIS MIXED LIST ---> the npc looks for ohigh or low -
    //or returna  list of strings 
    // the player then only goes to generics. 
    // do this in an overloaded method 

    JsonLoader jsn;
    public override void  Start()
    {
        jsn = FindObjectOfType<JsonLoader>();

    }

    public void testFM()
    {
        Debug.Log(JsonLoader.Instance.listOffATHERArguments.Count());
    }
    //make an overloaded method -- 

    public string returnCurrentCnpcStance(string surfaceValue, string subvalue)
    {
        if (NPCfmHighValues.Contains(surfaceValue))
            {
                return  "high";
            }
            else
            {
                return "low";
            }
        
    }



    public bool isCNPCDefendingValueLikesBNPC(string surfaceValue)
    {

        if (NPCfmHighValues.Contains(surfaceValue))
        {
            return true; //high (defend /likes)
        }
        else
        {
            return false;
        }

    }


    public string returnFatherModelArgumetnsText(string surfaceValue, string subvalue,
                                          List<string> exploredSterings, bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 
        string NPCType = "High";

        if (isNPC)
        {
            if (NPCfmHighValues.Contains(surfaceValue))
            {
                NPCType = "High";
            }
            else
            {
                NPCType = "Low";
            }
        } else //bug //i.e. player type here
        {
            if (NPCfmHighValues.Contains(surfaceValue))
            {
                NPCType = "Low";
            }
            else
            {
                NPCType = "High";
            }

        }

        string currentPatternCheck = subvalue;

        int i = 0;
        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {

            if (arg.SVkey == surfaceValue) //found the sv we wanted 
            {
                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    string r = sobject.schema.Split('_').First();
                  
                    if (sobject.subvalue == subvalue && NPCType.ToLower() == r)
                    {
                        exploredSterings.Add(subvalue);
                        return sobject.text;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck))
                    {

                        if (NPCType.ToLower() == r)
                        {
                            return sobject.text + "_" + sobject.subvalue; //else return the first thing that is high 
                        }
                        exploredSterings.Add(currentPatternCheck);

                        foreach (string s in exploredSterings)
                        {
                            //for debugging purp // delete
                        }
                        i++;
                        currentPatternCheck = arg.SurfaceValueObject[i].subvalue; //slight logic bug in counter if the current checked one was in rthe middle of the list -- 
                    }

                }
            }
        }
        return "GenericResponceGiven";

    }

    public string returnFatherModelArgumetnsForAspecficString(string surfaceValue, string subvalue,
                                         List<string> exploredSterings, string stanceValue)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 



        if (stanceValue.ToLower() != "high" || stanceValue.ToLower() != "low")
        {
            stanceValue = "low";
        }

        // Debug.Log("!!!!!+ NPC WILL LOOK FOR SCHEMAS THAT HAVE" + NPCType);
        string currentPatternCheck = subvalue;

        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            // Debug.Log("arg.SVkey "+ arg.SVkey);

            if (arg.SVkey == surfaceValue) //found the sv we wanted 
            {
                // Debug.Log("INSIDE SV KEY"+ arg.SVkey);
                // Debug.Log("size of  arg.surfaceValueObjList"  + arg.SurfaceValueObject.Count);

                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    string r = sobject.schema.Split('_').First();
                  
                    if (sobject.subvalue == subvalue && stanceValue.ToLower() == r)
                    {
                        return sobject.text;
                    }
                    
                  

                }
            }
        }
        return "GenericResponceGiven";

    }


    public string returnAppendedSchemaText(string surfaceValue, string subvalue,
                                      List<string> exploredSterings, bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 
        string NPCType = "High";

        if (isNPC)
        {
            if (NPCfmHighValues.Contains(surfaceValue))
            {
                NPCType = "High";
            }
            else
            {
                NPCType = "Low";
            }
        }
        else //bug //i.e. player type here
        {
            if (NPCfmHighValues.Contains(surfaceValue))
            {
                NPCType = "Low";
            }
            else
            {
                NPCType = "High";
            }

        }

        string currentPatternCheck = subvalue;

        int i = 0;
        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            
            if (arg.SVkey == surfaceValue) //found the sv we wanted 
            {
                
                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    string r = sobject.schema.Split('_').First();
                    
                    if (sobject.subvalue == subvalue && NPCType.ToLower() == r) //low or high
                    {
                        exploredSterings.Add(subvalue);//.change this to the npc or player list of static flags ( if cnpc flag )
                        return "<color=yellow> schema:"+ sobject.schema + "</color>" + sobject.schemaText;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck))
                    {
                        //   Debug.Log(" inside if it does not cvontained explorex strings !!!!!+ ! does thios happen ?  " + r + "and npc type" + NPCType.ToLower());

                        if (NPCType.ToLower() == r) // add a check if bnpc has this flag  here 
                        {
                            return sobject.schemaText + "<color=yellow>_" + sobject.subvalue +"</color>"; //else return the first thing that is high 

                        }
                        exploredSterings.Add(currentPatternCheck);//.change this to the npc or player list of static flags ( if cnpc flag )

                        foreach (string s in exploredSterings)
                        {
                            // Debug.Log("!!!!!+ exploredSterings npw adds" + s);

                        }
                        i++;
                        currentPatternCheck = arg.SurfaceValueObject[i].subvalue; //slight logic bug in counter if the current checked one was in rthe middle of the list -- 
                    }

                }
            }
        }

        return "GenericResponceGiven";

    }
    public List<string> returnIntersectingPatternNames(List<Dialoug> currentBNPCPatterns, string CurrentSV)
    {
        List<string> temp = new List<string>();
        string sv = CurrentSV;
        sv += "_CoreValues";

        Debug.Log("what is sv:" + sv);
        try
        {

            foreach(Dialoug d in currentBNPCPatterns)
            {
                foreach (string s in base.coreValuesDic[sv])
                {
                    if (d.Pattern == s)
                    {
                        temp.Add(s);
                    }
                }

            }

            return temp;

        }
        catch (System.Exception)
        {
            Debug.LogError(sv + "does not exhisit");
            throw;
        }

    }


    public List<Dialoug> returnIntersectingPatternNodes(List<Dialoug> currentBNPCPatterns, string CurrentSV, bool returnIntersecting)
    {
        List<Dialoug> temp = new List<Dialoug>();
        string sv = CurrentSV;
        sv += "_CoreValues";

        Debug.Log("what is sv:" + sv);
        try
        {

            foreach (Dialoug d in currentBNPCPatterns)
            {
                foreach (string s in base.coreValuesDic[sv])
                {
                    if (returnIntersecting)
                    {
                        if (d.Pattern == s)
                        {
                            temp.Add(d);
                        }
                    } else if(!returnIntersecting && d.Pattern!=s)
                    {
                        temp.Add(d);
                    }
                  
                }

            }

            return temp;

        }
        catch (System.Exception)
        {
            Debug.LogError(sv + "does not exhisit");
            throw;
        }

    }
    public StrictFatherMorality()
    {
        isPragmatic = Random.Range(0, 10) >= 4 ? true : false;
    }

    
  /*  public string ReturnSchema(string pattern, Dialoug characterNode)
    {
       base.returnSchemaValue()
    }
*/

    //retutired 
 

    //add self deispline and self reliance 


    public bool resultsInSelfIntrest()
    {
        //max welth - free stuff = badf in moral 
        //greater good 
        //ca;lcuate moral book keeping here 
        return true;
    }


    public int modelcitizenCalculations()
    {
        int temp = 0;

        foreach (KeyValuePair<string,bool> kvp in fatherSchemas)
        {
            if (kvp.Value)
            {
                temp += 1;
            }
        }
        return temp;
    }


}
