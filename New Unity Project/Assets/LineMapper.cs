using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMapper : MonoBehaviour
{

    public Transform[] points;
    public LineController line;

    // Start is called before the first frame update
    void Start()
    {
        line.lineSetup(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
