using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialoug : MonoBehaviour
{


    public string playerSurfaceValue { get; set; }
    public PlayerNarrativeElements playerNarrativeElements { get; set; }
}

    public class PlayerDisAgreementOnAflag
    {
        public string flag { get; set; }
        public string textValue { get; set; }
    }

    public class PlayerNarrativeElements
    {
        public string key { get; set; }
        public string rating { get; set; }
        public string playerInAgreementText { get; set; }
        public string playerDisagreementText { get; set; }
        public List<string> playerMoralDisagreementText { get; set; }
        public List<PlayerDisAgreementOnAflag> playerDisAgreementOnAflag { get; set; }
    }



