using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    if (d.ButtonText != "WillActOnLove")
                    {
                        ModelCounter++;
                        schemaName = "highStrength";
                        matchingPattern = d.ButtonText;
                        //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                        // add text later - - something like oh wow they have self restraint... ect 
                    }//else -not strength 
                    else if (d.ButtonText == "WillActOnLove")
                    {
                        
                           matchingPattern = d.ButtonText;
                        schemaName = "highRetribution";//something along the lines of he deserves to be punished ...ect 
                    }

                }
                break;
            case ("pregnantbutnotengaged"):
                schemaName = "lowMoralBoundaries";
                break;
            case ("departed"):
                schemaName = "lowMoralboundaries";//influingg others - negative 
                break;
            case ("hardWorker"):
            case ("IsWealthy"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.ButtonText == "IsRichButNotGenrous")
                    {
                        ModelCounter++;
                        schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                        matchingPattern = d.ButtonText;

                    }
                }
                break;

            case ("hasAbestFriend"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.ButtonText == "friendwithabestfriendsenemy")
                    {
                        ModelCounter--;
                        matchingPattern = d.ButtonText;
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
                    if (d.ButtonText == "hasalotofenemies")
                    {
                        matchingPattern = d.ButtonText;
                        schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.ButtonText == "friendwithabestfriendsenemy")
                    {
                        matchingPattern = d.ButtonText;
                        ModelCounter--; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.ButtonText == "loner")
                    {
                        matchingPattern = d.ButtonText;
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
                    if (d.ButtonText == "advancedCareer" || d.ButtonText == "hardWorker")
                    {
                        schemaName = "highStrength";
                        schemaName = "highSelfIntrest";                   
                        matchingPattern = d.ButtonText;
                        ModelCounter++;
                        // also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }

                    if (d.ButtonText== "Teachingrole")
                    {
                        matchingPattern = d.ButtonText;
                        schemaName = "highMoralOrder";
                        matchingPattern = d.ButtonText; ModelCounter++;

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
                        if(d.ButtonText == "notworkingandrich")
                    {
                        ModelCounter++;
                        matchingPattern = d.ButtonText;
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
                    if (d.ButtonText == "hasalotofenemies")
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.ButtonText == "friendwithabestfriendsenemy")
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.ButtonText == "loner")
                    {
                        matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else
                        schemaName = "lowMoralboundaries";//fix this --- 

                }

                break;
            case ("pregnantbutnotfromspuceorbutloveintrest"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.ButtonText == "pregnantbutnotengaged")
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else if (d.ButtonText == "InLoveWirhAnothersspuce")
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    if (d.ButtonText == "InLovewithspouseoffriend")
                    {
                        matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    if (d.ButtonText == "InLovewithspouseoffriend")
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                    else
                    {
                        ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowStrength";
                    }

                }

                break;
            case ("InLoveWirhAnothersspuce"):
                schemaName = "lowMoralboundaries";

                break;

                
            case ("WillActOnLove"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.ButtonText == "InLovewithspouseoffriend")
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
                    if (d.ButtonText == "hardWorker")
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
            if (tag != dialoug.ButtonText);
            {
                Debug.Log(" tag is " + tag + "combined with pattern" + d.ButtonText + "for the character" + d.parent.ButtonText);


                string schemaName = "NoSchemaFound";
                switch (tag)
                {
                    case ("InLovewithspouseoffriend"):
                        foreach (Dialoug d in characterNode.parent.children)//send in the parent ( thing that contains sub nodes )
                        {
                            if (d.ButtonText != "WillActOnLove")
                            {
                                ModelCounter++;
                                schemaName = "highStrength";
                                matchingPattern = d.ButtonText;
                                //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                                // add text later - - something like oh wow they have self restraint... ect 
                            }//else -not strength 
                            else if (d.ButtonText == "WillActOnLove")
                            {

                                matchingPattern = d.ButtonText;
                                schemaName = "highRetribution";//something along the lines of he deserves to be punished ...ect 
                            }

                        }
                        break;
                    case ("pregnantbutnotengaged"):
                        schemaName = "lowMoralBoundaries";
                        break;
                    case ("departed"):
                        schemaName = "lowMoralboundaries";//influingg others - negative 
                        break;
                    case ("hardWorker"):
                    case ("IsWealthy"):
                        foreach (Dialoug d in characterNode.parent.children)
                        {
                            if (d.ButtonText == "IsRichButNotGenrous")
                            {
                                ModelCounter++;
                                schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                                matchingPattern = d.ButtonText;

                            }
                        }
                        break;

                    case ("hasAbestFriend"):
                        foreach (Dialoug d in characterNode.parent.children)
                        {
                            if (d.ButtonText == "friendwithabestfriendsenemy")
                            {
                                ModelCounter--;
                                matchingPattern = d.ButtonText;
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
                            if (d.ButtonText == "hasalotofenemies")
                            {
                                matchingPattern = d.ButtonText;
                                schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else if (d.ButtonText == "friendwithabestfriendsenemy")
                            {
                                matchingPattern = d.ButtonText;
                                ModelCounter--; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else if (d.ButtonText == "loner")
                            {
                                matchingPattern = d.ButtonText;
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
                            if (d.ButtonText == "advancedCareer" || d.ButtonText == "hardWorker")
                            {
                                schemaName = "highStrength";
                                schemaName = "highSelfIntrest";
                                matchingPattern = d.ButtonText;
                                ModelCounter++;
                                // also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }

                            if (d.ButtonText == "Teachingrole")
                            {
                                matchingPattern = d.ButtonText;
                                schemaName = "highMoralOrder";
                                matchingPattern = d.ButtonText; ModelCounter++;

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
                            if (d.ButtonText == "notworkingandrich")
                            {
                                ModelCounter++;
                                matchingPattern = d.ButtonText;
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
                            if (d.ButtonText == "hasalotofenemies")
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else if (d.ButtonText == "friendwithabestfriendsenemy")
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else if (d.ButtonText == "loner")
                            {
                                matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else
                                schemaName = "lowMoralboundaries";//fix this --- 

                        }

                        break;
                    case ("pregnantbutnotfromspuceorbutloveintrest"):
                        foreach (Dialoug d in characterNode.parent.children)
                        {
                            if (d.ButtonText == "pregnantbutnotengaged")
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralWholeness";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else if (d.ButtonText == "InLoveWirhAnothersspuce")
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            if (d.ButtonText == "InLovewithspouseoffriend")
                            {
                                matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            if (d.ButtonText == "InLovewithspouseoffriend")
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowMoralboundaries";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                            }
                            else
                            {
                                ModelCounter--; matchingPattern = d.ButtonText; schemaName = "lowStrength";
                            }

                        }

                        break;
                    case ("InLoveWirhAnothersspuce"):
                        schemaName = "lowMoralboundaries";

                        break;


                    case ("WillActOnLove"):
                        foreach (Dialoug d in characterNode.parent.children)
                        {
                            if (d.ButtonText == "InLovewithspouseoffriend")
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
                            if (d.ButtonText == "hardWorker")
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
    private void setArgumentStructure(string mainPattern, string mattchingPattern,string schema, int moralPoints)
    {
        Debug.Log("does this happen? setArgumentStructure" );

        CurrentArgument.pattern = mainPattern;
        CurrentArgument.matchingPattern = mattchingPattern;
        if (moralPoints > 1) CurrentArgument.modelCitizen = true;
        CurrentArgument.expandedArgument = expandArgument(mainPattern, mattchingPattern, schema);
        Debug.Log(expandArgument(mainPattern, mattchingPattern, schema));
    }

    private string expandArgument(string mainPattern, string mattchingPattern, string schema)
    {
        Debug.Log("hey hey hey "+CurrentArgument.pattern);
        return "read from json for " + schema + "sub_" + mainPattern + mattchingPattern +"or what the pattern matches to + main value ? one refined other easier to write? ask MM"; 
    }

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