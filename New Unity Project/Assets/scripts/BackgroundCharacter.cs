using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BackgroundCharacter : MonoBehaviour
{//org lists later and move them to their own objects 
    public JsonLoader jsn;
    // Start is called before the first frame update
    void Start()
    {
        JsonLoader jsn = gameObject.GetComponent<JsonLoader>();
        Debug.Log(jsn.listOfPersonalities.Count);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int x = returnHighestOpenValueID();
            Debug.Log("!!!" + returnAcharacterName(x));
        }
    }

    int  returnHighestOpenValueID() //make this genertic for all people ---
    {
        List<double> opennesVlues = new List<double>() ;
        int id=0;
        double maxValue=0;
        // Debug.Log("does this print 1");



        foreach (TwoniePersonalities townie in jsn.listOfPersonalities)
        {
            opennesVlues.Add(townie.opennessToExperience);

            if (townie.opennessToExperience >= opennesVlues.Last())
            {
                maxValue = townie.opennessToExperience;
                id = townie.personID;
            }
        }
        Debug.Log("the person with the max value is; " + id + "with a valye of " + maxValue);
        return id;

    }//end of return highOpennes

    string returnAcharacterName(int id)
    {
        foreach(TownCharacter townie in jsn.backgroundcharacters)
        {
            if(id== townie.id)
            {
                return (townie.firstName + " " + townie.lastName);
            }
        }
        return "no id found ";

    }
}
