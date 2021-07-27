using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralModelArguments //this is the new one for testing x OLD IS CALLED MODELARGUMENTS 
{
    public string SVkey { get; set; }
    public List<SurfaceValueObject> SurfaceValueObject { get; set; }
}
public class SurfaceValueObject
{
    public string subvalue { get; set; }
    public string text { get; set; }
    public string schema { get; set; }
    public string NPCTypeSchemaValue { get; set; }
}

