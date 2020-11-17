using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TownEvents 
{
    public int event_id;
    public string type;
    public int subject;// subject is the person's id === who the event is about -example personID (123) gave (event type) birth!
    //or person can refer to the event 
}
