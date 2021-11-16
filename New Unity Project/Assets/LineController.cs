using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update

    public LineRenderer lr;
    Transform[] points;

    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
    }

    public void lineSetup(Transform[] _points)
    {
        lr.positionCount = _points.Length;
        points = _points;
        DrawLine();
    }

    public void DrawLine()
    {
        for(int i = 0; i< points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
