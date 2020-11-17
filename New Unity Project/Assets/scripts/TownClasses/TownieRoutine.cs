using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TownieRoutine
{
         public string  type;//if type and vocation matches then they do what they like --- 
         public int personID;
         public int companyID;
         public string shift;  
         public int hiring;//-1 for false
         public int precededByID;//-1 for nill -- I THINK THIS IS FOR PERSON IOD UNSURE 
         public int succeededByID;//-1 for nill
         public bool hiredAsFavor;
         public string vocation;//vocation - ask michael if it is current or wants to be?
         public int Careerlevel; //i think ? IT JUST SAYS LEVEL IN CODE 
}
