using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Linq;


public class NurturantParentMorality:MoralModels {


    Dictionary<string, bool> motherSchemas = new Dictionary<string, bool>();

    List<string> NPCNPHighValues = new List<string>() //what the NPC looks for - ig its not herte NPC looks for low 
    {
       "BTrueTYourHeart","Teetotasler","ProHiringFamily",
        "Shapesarenothingifnotsocial","FriendsAreTheJoyOFlife",
        "AnimalLover","Enviromentalist","SchoolIsCool","FamilyPerson",
        "AselfMadeShapeWeAspireToBe","ImmagretsWeGetTheJobDone","SupportingComunities",
        "LoverOfRisks","LandISWhereThehrtIS","youthAreTheFuture","WeLiveForSpontaneity",
        "AnAdventureWeSeek","MoneyMaker","CarrerAboveAll"

    };

    protected bool isPragmatic;

    public string ReturnSchema(string tag, Dialoug characterNode) //used in combo with flags to construt arguments, this returns the schema accoring to father models 
    {
        Debug.Log(" tag is " + tag + "charanode" + characterNode);
        string schemaName = "NoSchemaFound";
        switch (tag)
        {
            case ("InLovewithspouseoffriend"):
                foreach (Dialoug d in characterNode.parent.children)//send in the parent ( thing that contains sub nodes )
                {
                    if (d.Pattern != "WillActOnLove")
                    {
                        schemaName = "highStrength"; //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                        // add text later - - something like oh wow they have self restraint... ect 
                    }//else -not strength 
                    else if (d.Pattern == "WillActOnLove")
                    {
                        schemaName = "highRetribution";//something along the lines of he deserves to be punished ...ect 
                    }

                }
                break;
            case ("likesToDate"):
                schemaName = "lowMoralBoundaries";
                break;
            case ("departed"):
                schemaName = "lowMoralboundaries";//influingg others - negative 
                break;
            case ("hardWorker"):
            case ("IsWealthy"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "IsRichButNotGenrous")
                    {
                        schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                }
                break;

            case ("hasAbestFriend"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "friendwithabestfriendsenemy")
                    {
                        schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                }
                break;

            case ("butcherRole"):
                schemaName = "highMoralOrder";//what fits naturally 
                break;
            case ("flipflop"):
                schemaName = "lowMoralboundaries";//devaiting from the norm - bad 
                break;
            case ("widowedbutnotgrieving"):
                schemaName = "lowMoralboundaries";
                break;
            default:
                return "noSchemasFound";
        }
        //add new values and resturcture this 
        return schemaName;
    }
    //add self deispline and self reliance 

    public bool resultsInSelfIntrest()
    {
        return true;
    }

    public string returnNurturantModelArgumetnsText(string surfaceValue, string subvalue,
                                       List<string> exploredSterings, bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 
        string NPCType = "High";

        if (isNPC)
        {
            if (NPCNPHighValues.Contains(surfaceValue))
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
            if (NPCNPHighValues.Contains(surfaceValue))
            {
                NPCType = "Low";
            }
            else
            {
                NPCType = "High";
            }

        }

        // Debug.Log("!!!!!+ NPC WILL LOOK FOR SCHEMAS THAT HAVE" + NPCType);
        string currentPatternCheck = subvalue;

        int i = 0;
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
                    //Debug.Log("!!!!!+ r value" + r);

                    /*      Debug.Log("!!!!!+ !exploredSterings.Contains(currentPatternCheck) value" +
                         !exploredSterings.Contains(currentPatternCheck)); *///after the furst time it becomes false 

                    if (sobject.subvalue == subvalue && NPCType.ToLower() == r)
                    {
                        exploredSterings.Add(subvalue);
                        return sobject.text;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck))
                    {
                        //   Debug.Log(" inside if it does not cvontained explorex strings !!!!!+ ! does thios happen ?  " + r + "and npc type" + NPCType.ToLower());

                        if (NPCType.ToLower() == r)
                        {
                            return sobject.text + "_" + sobject.subvalue; //else return the first thing that is high 
                        }
                        exploredSterings.Add(currentPatternCheck);

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

    public string returnAppendedSchemaText(string surfaceValue, string subvalue,
                                      List<string> exploredSterings, bool isNPC)
    {//update this ti include updates high low lists / player or npc --- update to reflect player bool - update for sening in list of explored or some list 
        string NPCType = "High";

        if (isNPC)
        {
            if (NPCNPHighValues.Contains(surfaceValue))
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
            if (NPCNPHighValues.Contains(surfaceValue))
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
                        return sobject.schemaText;
                    }
                    else if (!exploredSterings.Contains(currentPatternCheck))
                    {
                        //   Debug.Log(" inside if it does not cvontained explorex strings !!!!!+ ! does thios happen ?  " + r + "and npc type" + NPCType.ToLower());

                        if (NPCType.ToLower() == r) // add a check if bnpc has this flag  here 
                        {
                            return sobject.schemaText + "_" + sobject.subvalue; //else return the first thing that is high 

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

            foreach (Dialoug d in currentBNPCPatterns)
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
                    }
                    else if (!returnIntersecting && d.Pattern != s)
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
    public NurturantParentMorality()
    {
        if (Random.Range(0, 10) >= 4)
        {
            isPragmatic = true;
        }
        else
        {
            isPragmatic = false;
        }
    }
}
