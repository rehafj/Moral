using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurturantParentMorality:MoralModels {




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
                    if (d.ButtonText != "WillActOnLove")
                    {
                        schemaName = "highStrength"; //depicts moral strength // later will translate into something like "he applies self displine well.//// 
                        // add text later - - something like oh wow they have self restraint... ect 
                    }//else -not strength 
                    else if (d.ButtonText == "WillActOnLove")
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
                    if (d.ButtonText == "IsRichButNotGenrous")
                    {
                        schemaName = "highMoralOrder";// also true as self dispiince here  // along th elines of it's their work they shouild do with it as they see fir 
                    }
                }
                break;

            case ("hasAbestFriend"):
                foreach (Dialoug d in characterNode.parent.children)
                {
                    if (d.ButtonText == "friendwithabestfriendsenemy")
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


}
