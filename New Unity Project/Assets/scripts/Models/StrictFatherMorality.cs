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
       "BTrueTYourHeart","Teetotasler","ProHiringFamily","Shapesarenothingifnotsocial","FriendsAreTheJoyOFlife","AnimalLover","Enviromentalist","SchoolIsCool","FamilyPerson","AselfMadeShapeWeAspireToBe","ImmagretsWeGetTheJobDone","SupportingComunities","LoverOfRisks","LandISWhereThehrtIS","youthAreTheFuture","WeLiveForSpontaneity","AnAdventureWeSeek","MoneyMaker","CarrerAboveAll"

    };

    List<string> NPCSVfORbOTHhIGHaNDlOW = new List<string>() { };
    // IF ITS PART THIS MIXED LIST ---> the npc looks for ohigh or low -
    //or returna  list of strings 
    // the player then only goes to generics. 
    // do this in an overloaded method 

    JsonLoader jsn;
    public void Start()
    {
        jsn = FindObjectOfType<JsonLoader>();
        Debug.Log("did we find a json object?" + jsn);

    }

    public void  testFM()
    {
        Debug.Log(JsonLoader.Instance.listOffATHERArguments.Count());
    }
    //make an overloaded method -- 
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
                    } else if(!returnIntersecting &&d.Pattern!=s)
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
        if (Random.Range(0, 10) >= 4)
        {
            isPragmatic = true;
        }
        else
        {
            isPragmatic = false;
        }
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

/*
 /// retired old code 
 /// 




   public string ReturnSchema(string tag, Dialoug characterNode) //used in combo with flags to construt arguments, this returns the schema accoring to father models 
    {
       // setArgumentStructure("","","",0);

        int ModelCounter = 0;


 
        string schemaName = "NoSchemaFound";
        string matchingPattern = "";
        switch (tag)
        {
            case ("InLovewithspouseoffriend"):
                foreach (Dialoug d in characterNode.parent.children)//send in the parent ( thing that contains sub nodes )
                {
                    if (d.Pattern != "WillActOnLove")
                    {
                        ModelCounter++;
                        schemaName = "highStrength";
                        matchingPattern = d.Pattern;
                        //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                        // add text later - - something like oh wow they have self restraint... ect 
                    }//else -not strength 
                    else if (d.Pattern == "WillActOnLove")
                    {
                        
                           matchingPattern = d.Pattern;
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
                        ModelCounter++;
                        schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                        matchingPattern = d.Pattern;

                    }
                }
                break;

            case ("hasAbestFriend"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "friendwithabestfriendsenemy")
                    {
                        ModelCounter--;
                        matchingPattern = d.Pattern;
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
                ModelCounter--; schemaName = "lowMoralboundaries";
                break;
            case ("familyPerson"):
                schemaName = "highMoralOrder";
                break;
            case ("socialLife"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "hasalotofenemies")
                    {
                        matchingPattern = d.Pattern;
                        schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.Pattern == "friendwithabestfriendsenemy")
                    {
                        matchingPattern = d.Pattern;
                        ModelCounter--; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.Pattern == "loner")
                    {
                        matchingPattern = d.Pattern;
                        schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }else
                        schemaName = "highMoralboundaries";

                }
               
                break;
            case ("hasalotofenemies"):
              
                 schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                 
                break;
            case ("graduate"):
                schemaName = "highMoralboundaries";
   
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "advancedCareer" || d.Pattern == "hardWorker")
                    {
                        schemaName = "highStrength";
                        schemaName = "highSelfIntrest";                   
                        matchingPattern = d.Pattern;
                        ModelCounter++;
                        // also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }

                    if (d.Pattern== "Teachingrole")
                    {
                        matchingPattern = d.Pattern;
                        schemaName = "highMoralOrder";
                        matchingPattern = d.Pattern; ModelCounter++;

                    }


                }

                break;
            case ("friendwithabestfriendsenemy"):
                schemaName = "lowMoralWholeness"; ModelCounter--;
                break;
            case ("adultbutnotworking"):
                schemaName = "lowMoralEssence";
                  foreach (Dialoug d in characterNode.parent.children)
                {
                        if(d.Pattern == "notworkingandrich")
                    {
                        ModelCounter++;
                        matchingPattern = d.Pattern;
                        schemaName = "highMoralOrder";
                    }
                }
                break;
            case ("notworkingandrich"):
                schemaName = "highMoralOrder";
                ModelCounter++;
                break;
            case ("loner"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "hasalotofenemies")
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.Pattern == "friendwithabestfriendsenemy")
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.Pattern == "loner")
                    {
                        matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else
                        schemaName = "lowMoralboundaries";//fix this --- 

                }

                break;
            case ("leftFotLoveIntrest"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "likesToDate")
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.Pattern == "InLoveWirhAnothersspouce")
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    if (d.Pattern == "InLovewithspouseoffriend")
                    {
                        matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    if (d.Pattern == "InLovewithspouseoffriend")
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else
                    {
                        ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowStrength";
                    }

                }

                break;
            case ("InLoveWirhAnothersspouce"):
                schemaName = "lowMoralboundaries";

                break;

                
            case ("WillActOnLove"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "InLovewithspouseoffriend")
                    {
                        schemaName = "lowStrength";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                        ModelCounter--;
                    }
                    else
                        ModelCounter++; schemaName = "highStrength";
               
                }

                break;
            case ("IsRichButNotGenrous"):
                schemaName = "highMoralOrder";

                break;
            case ("WorksInAlcohol"):
                schemaName = "highSelfIntrest";
                ModelCounter++;
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.Pattern == "hardWorker")
                    {
                        ModelCounter++;
                        schemaName = "highSelfIntrest";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                
                }

                break; //TODO 
            case ("healerRole"):
                schemaName = "highSelfIntrest"; ModelCounter++;
                break;
            case ("polluterRole"):
            case ("riskTaker"):
            case ("Teachingrole"):
            case ("advancedCareer"):
            case ("CustodianJobs"):
            case ("generalJobs"):
                schemaName = "highSelfIntrest"; ModelCounter++;

                break;
            default:
                return "noSchemasFound";
        }

        Debug.Log("the schema for the flag" + tag + "based on patterns is" + schemaName);
        Debug.Log("does this happen? inside SF------" + tag + matchingPattern+ schemaName+ ModelCounter);

        setArgumentStructure(tag, matchingPattern, schemaName, ModelCounter);

        return schemaName;
    }
    /*  public List<Argument> ReturnArguments(string tag, Dialoug characterNode) {

          int ModelCounter = 0;
          string matchingPattern = "";
          List<Argument> args = new List<Argument>();

          foreach (Dialoug dialoug in characterNode.parent.children)
          {
              if (tag != dialoug.Pattern);
              {
                  Debug.Log(" tag is " + tag + "combined with pattern" + d.Pattern + "for the character" + d.parent.Pattern);


                  string schemaName = "NoSchemaFound";
                  switch (tag)
                  {
                      case ("InLovewithspouseoffriend"):
                          foreach (Dialoug d in characterNode.parent.children)//send in the parent ( thing that contains sub nodes )
                          {
                              if (d.Pattern != "WillActOnLove")
                              {
                                  ModelCounter++;
                                  schemaName = "highStrength";
                                  matchingPattern = d.Pattern;
                                  //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                                  // add text later - - something like oh wow they have self restraint... ect 
                              }//else -not strength 
                              else if (d.Pattern == "WillActOnLove")
                              {

                                  matchingPattern = d.Pattern;
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
                                  ModelCounter++;
                                  schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                                  matchingPattern = d.Pattern;

                              }
                          }
                          break;

                      case ("hasAbestFriend"):
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "friendwithabestfriendsenemy")
                              {
                                  ModelCounter--;
                                  matchingPattern = d.Pattern;
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
                          ModelCounter--; schemaName = "lowMoralboundaries";
                          break;
                      case ("familyPerson"):
                          schemaName = "highMoralOrder";
                          break;
                      case ("socialLife"):
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "hasalotofenemies")
                              {
                                  matchingPattern = d.Pattern;
                                  schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else if (d.Pattern == "friendwithabestfriendsenemy")
                              {
                                  matchingPattern = d.Pattern;
                                  ModelCounter--; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else if (d.Pattern == "loner")
                              {
                                  matchingPattern = d.Pattern;
                                  schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else
                                  schemaName = "highMoralboundaries";

                          }

                          break;
                      case ("hasalotofenemies"):

                          schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 

                          break;
                      case ("graduate"):
                          schemaName = "highMoralboundaries";

                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "advancedCareer" || d.Pattern == "hardWorker")
                              {
                                  schemaName = "highStrength";
                                  schemaName = "highSelfIntrest";
                                  matchingPattern = d.Pattern;
                                  ModelCounter++;
                                  // also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }

                              if (d.Pattern == "Teachingrole")
                              {
                                  matchingPattern = d.Pattern;
                                  schemaName = "highMoralOrder";
                                  matchingPattern = d.Pattern; ModelCounter++;

                              }


                          }

                          break;
                      case ("friendwithabestfriendsenemy"):
                          schemaName = "lowMoralWholeness"; ModelCounter--;
                          break;
                      case ("adultbutnotworking"):
                          schemaName = "lowMoralEssence";
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "notworkingandrich")
                              {
                                  ModelCounter++;
                                  matchingPattern = d.Pattern;
                                  schemaName = "highMoralOrder";
                              }
                          }
                          break;
                      case ("notworkingandrich"):
                          schemaName = "highMoralOrder";
                          ModelCounter++;
                          break;
                      case ("loner"):
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "hasalotofenemies")
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else if (d.Pattern == "friendwithabestfriendsenemy")
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else if (d.Pattern == "loner")
                              {
                                  matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else
                                  schemaName = "lowMoralboundaries";//fix this --- 

                          }

                          break;
                      case ("leftFotLoveIntrest"):
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "likesToDate")
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else if (d.Pattern == "InLoveWirhAnothersspouce")
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              if (d.Pattern == "InLovewithspouseoffriend")
                              {
                                  matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              if (d.Pattern == "InLovewithspouseoffriend")
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }
                              else
                              {
                                  ModelCounter--; matchingPattern = d.Pattern; schemaName = "lowStrength";
                              }

                          }

                          break;
                      case ("InLoveWirhAnothersspouce"):
                          schemaName = "lowMoralboundaries";

                          break;


                      case ("WillActOnLove"):
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "InLovewithspouseoffriend")
                              {
                                  schemaName = "lowStrength";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                                  ModelCounter--;
                              }
                              else
                                  ModelCounter++; schemaName = "highStrength";

                          }

                          break;
                      case ("IsRichButNotGenrous"):
                          schemaName = "highMoralOrder";

                          break;
                      case ("WorksInAlcohol"):
                          schemaName = "highSelfIntrest";
                          ModelCounter++;
                          foreach (Dialoug d in characterNode.parent.children)
                          {
                              if (d.Pattern == "hardWorker")
                              {
                                  ModelCounter++;
                                  schemaName = "highSelfIntrest";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                              }

                          }

                          break; //TODO 
                      case ("healerRole"):
                          schemaName = "highSelfIntrest"; ModelCounter++;
                          break;
                      case ("polluterRole"):
                      case ("riskTaker"):
                      case ("Teachingrole"):
                      case ("advancedCareer"):
                      case ("CustodianJobs"):
                      case ("generalJobs"):
                          schemaName = "highSelfIntrest"; ModelCounter++;

                          break;
                      default:
                          schemaName = "highSelfIntrest"; ModelCounter++;
                          break;
                  }
              }
          }

          return args;

      }
          //used in combo with flags to construt arguments, this returns the schema accoring to father models */

/*
private void setArgumentStructure(string mainPattern, string mattchingPattern, string schema, int moralPoints)
{
    Debug.Log("does this happen? setArgumentStructure");

    CurrentArgument.pattern = mainPattern;
    CurrentArgument.matchingPattern = mattchingPattern;
    if (moralPoints > 1) CurrentArgument.modelCitizen = true;
    CurrentArgument.expandedArgument = expandArgument(mainPattern, mattchingPattern, schema);
    Debug.Log(expandArgument(mainPattern, mattchingPattern, schema));
}

private string expandArgument(string mainPattern, string mattchingPattern, string schema)
{
    Debug.Log("hey hey hey " + CurrentArgument.pattern);
    return "read from json for " + schema + "sub_" + mainPattern + mattchingPattern + "or what the pattern matches to + main value ? one refined other easier to write? ask MM";
}


*/



/* 
 * 
 * foreach (DialougStructure op in opinions) //ex: all high opp -- splits them up here as high mid and low 
        {
            

            if (op.topic.Contains(mappedSV)) //get the translatiopn of they key but not ditect character keys.....  //TOFRICKENDO changet his to currentnode.sv
            {
               // Debug.Log(op.topic.Contains(mapToCNPCMoralFactor(key)) + "for the key " + key);
                string r  = op.topic.Split('_').First();
                selectedOpnion = op.topic.Split('_').Last(); //surface value is returned here = -  sv selected opinion 
               // Debug.Log("selectedOpnion" + selectedOpnion +" AND FLAG "+ flag +"TOPIC"+op.topic);// FLAG isd what i send over to classify as intro text pt agreement or .... 
                //selectedOpinion is the surface value

                //redo this super bad method 
                if (flag == "BiasedSVOpin")
                {
                    return op.NarrativeElements.surfaceOpinionOnTopic;
                }
                if (flag == "PlayerAgreesWithCNPC")
                {
                    return op.NarrativeElements.agreementText;
                }
                if (flag == "PlayerDisAgreesWithCNPC")
                {
                   // Debug.Log("setting disagreement text as " + op.NarrativeElements.disagreementtext);
                    return op.NarrativeElements.disagreementtext;
                }    
            }
        }
        return "NO TOPIC WAS FOUND --- need to author topic for flag " + key;

*/