using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelArguements
{
    public string schema = "";
    public Modelelements Modelelements = new Modelelements();
}

public class Modelelements {
    public string name = "";
    public string[] matchingPatterns = { };
}
public class PatternArguments {
    public string[] arguments = { };

}
//schem

// take this schema "h" with patterns x,y and return me an argument 
// schema =="h"  and mattchingPattern.contains [x and y ] return modelElements 
//arguments of [x,y]