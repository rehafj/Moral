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
    /// <summary>
    /// used for the first check
    /// </summary>
    /// <param name="surfaceValue"></param>
    /// <param name="subvalue"></param>
    /// <param name="exploredStrings"></param>
    /// <param name="isNPC"></param>
    /// <returns></returns>
    public string returnFatherModelFirstArgument(string surfaceValue, string subvalue,
                                           List<string> exploredStrings,
                                           bool isNPC) // 
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 

        exploredStrings.Add("");
        foreach (string s in exploredStrings)
        {
            Debug.Log(" pre enterting the loop --- the explored topics are" + s);

        }

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

        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {


            if (arg.SVkey == surfaceValue) //found the SV
            {
                int i = -1;

                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    Debug.Log("help!@!!!!!!!!!!!!!!! does the explored string list containin " + currentPatternCheck + exploredStrings.Contains(currentPatternCheck));
                    Debug.Log("what is this  statment" + !exploredStrings.Contains(currentPatternCheck));

                    string r = sobject.schema.Split('_').First();

                   

                        if (!exploredStrings.Contains(subvalue)) //always false why? no 10 true
                        {

                            Debug.Log("test"); 
                            Debug.Log("testing for the pattern" + currentPatternCheck + "currentPatternCheck with sobject.subvalue " + sobject.subvalue);


                            if (sobject.subvalue == currentPatternCheck && NPCType.ToLower() == r) //i.e. the sent in pattern matches the argument
                            {

                                Debug.Log("2--does this cond fail" + NPCType.ToLower() == r);

                                string t = NPCType.ToLower() == "high" ? "defend" : "oppose";

                                Debug.Log("found " + sobject.subvalue);
                                return "<color=red> FM used for the pattern  " + subvalue + " under the SV: " + surfaceValue + "</color> " + sobject.text + "<color=yellow> to " + t + "</color>_" + sobject.subvalue;


                            }//add it to explored?
                        }
                        else
                        {
                            i++;
                            currentPatternCheck = arg.SurfaceValueObject[i].subvalue;

                        }

                    }


                }

            }

        Debug.Log("nothing found");
        return "GenericResponceGiven";

    } 
    public string returnFatherModelArgumetnsText(string surfaceValue, string subvalue,
                                          List<string> exploredStrings,
                                          List<string> currentBNPCPatterns, bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 

        exploredStrings.Add("");
        foreach(string s in exploredStrings)
        {
            Debug.Log(" the explored topics are" + s);

        }

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

        string chosenPattern =""; 

        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            //check the subvalue first.. 

            if (arg.SVkey == surfaceValue) //found the SV
            {
                int i = -1;

                foreach (string bnpcPattern in currentBNPCPatterns)
                {
                    Debug.Log("checking for the pattern " + bnpcPattern +"under bnpc patterns for the sv " + arg.SVkey);

                    if (!exploredStrings.Contains(bnpcPattern))
                    {
                        Debug.Log("the explored strings does not contain " + bnpcPattern);


                        foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                        {
                            Debug.Log("comparing the evalues of subvalue and current pattern " +
                                bnpcPattern + "with" + sobject.subvalue);
                            Debug.Log(sobject.subvalue);

                            string r = sobject.schema.Split('_').First();

                            Debug.Log(r);
                            Debug.Log("value of noc's attack vs defend +  NPCType.ToLower() == r " + NPCType.ToLower() == r);


                            if (sobject.subvalue == bnpcPattern && NPCType.ToLower() == r)
                            {
                                Debug.Log("2--does this cond fail" + NPCType.ToLower() == r);

                                string t = NPCType.ToLower() == "high" ? "defend" : "oppose";

                                Debug.Log("found " + sobject.subvalue);
                                return "<color=red> FM used for the pattern  " + subvalue + " under the SV: " + surfaceValue + "</color> " + sobject.text + "<color=yellow> to " + t + "</color>_" + sobject.subvalue;

                            }
                            else
                            {
                                chosenPattern = sobject.subvalue; // weak 
                                /*i++;
                                currentPatternCheck = arg.SurfaceValueObject[i].subvalue;*/

                            }



                        }

                    }
                }

            }
        }
        Debug.Log("nothing found + would send in a random weak argument fopr the pattern " + chosenPattern);
        return "GenericResponceGiven"; // + random pnpc  pattern 

    }

    
    public List< KeyValuePair<string, string>> returnAllPossibleCounterArgumentsDebugging(string stance)
    {
        List<KeyValuePair<string, string>> svAndText = new List<KeyValuePair<string, string>>();
        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            foreach (SurfaceValueObject svo in arg.SurfaceValueObject)
            {
                if(svo.schema.Split('_').First().ToLower() == stance) //low or high
                {
                    svAndText.Add(new KeyValuePair<string, string> ( arg.SVkey, "with the transitional schema text \'"+svo.schemaText + " \'and the actual point" + svo.text + "for subvalue" + svo.subvalue));
                }
            }
        }
        return svAndText;
    }

    public List<KeyValuePair<string, string>> returnAllPossibleCounterArgumentsDebugging(string stance, List<string> intersectingPatterns)
    {
        List<KeyValuePair<string, string>> svAndText = new List<KeyValuePair<string, string>>();
        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            foreach (SurfaceValueObject svo in arg.SurfaceValueObject)
            {
                if (svo.schema.Split('_').First().ToLower() == stance && intersectingPatterns.Contains(svo.subvalue)) //low or high
                {
                    svAndText.Add(new KeyValuePair<string, string>(arg.SVkey, "with the transitional schema text \'" + svo.schemaText + " \'and the actual point" + svo.text + "for subvalue" + svo.subvalue));
                }
            }
        }
        return svAndText;
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
                                      List<string> exploredSterings, bool isNPC) //change this!!!! 


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

        int i = -1;
        foreach (MoralModelArguments arg in JsonLoader.Instance.listOffATHERArguments)
        {
            
            if (arg.SVkey == surfaceValue) //found the sv we wanted 
            {
                
                foreach (SurfaceValueObject sobject in arg.SurfaceValueObject)
                {
                    string r = sobject.schema.Split('_').First();
                    
                    if (sobject.subvalue == subvalue && NPCType.ToLower() == r) //low or high
                    {
                       // exploredSterings.Add(subvalue);//.change this to the npc or player list of static flags ( if cnpc flag )
                        return "<color=yellow> the schema used is:"+ sobject.schema + "</color>" + sobject.schemaText;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck))
                    {
                        //   Debug.Log(" inside if it does not cvontained explorex strings !!!!!+ ! does thios happen ?  " + r + "and npc type" + NPCType.ToLower());

                        if (NPCType.ToLower() == r) // add a check if bnpc has this flag  here 
                        {
                            return "<color=yellow> the schema used is:" + sobject.schema + "</color>" + sobject.schemaText + "<color=yellow>_" + sobject.subvalue +"</color>"; //else return the first thing that is high 
                            //debug whatever uses this!!!! AGHHHHHHHHHHHHHHHH
                        }
                      //  exploredSterings.Add(currentPatternCheck);//.change this to the npc or player list of static flags ( if cnpc flag )

                      
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
