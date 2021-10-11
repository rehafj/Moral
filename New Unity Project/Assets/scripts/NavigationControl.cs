using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavigationControl : MonoBehaviour
{

    public GameObject goal1;
    public GameObject goal2;
    NavMeshAgent agent;
    Material mt;
    Renderer rd;
    // Start is called before the first frame update

    private void Awake()
    {
       
        string s = "";
        int r = UnityEngine.Random.Range(0, 2);
        s = setColorBase(r);
        mt = gameObject.GetComponent<Material>();

        rd = GetComponent<Renderer>();

        goal1 = GameObject.FindGameObjectWithTag("D");//disguisepoint
        goal2= GameObject.FindGameObjectWithTag("C");//chair 
        mt = Resources.Load<Material>(s);
        rd.material = mt;

    }

    private string setColorBase(int r) //redo this and get a random one from resources folder -- temp for tests
    {
        if (r == 0)
        {
            return "ShapeDesigns/cubeColors/greenCube";
        }
        else if (r == 1)
        {
            return "ShapeDesigns/cubeColors/RedCube";

        }
        else
        {
            return "ShapeDesigns/cubeColors/BlueCube";

        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal1.transform.position;
        Invoke("updateDestenation", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDestenation()
    {
        agent.destination = goal2.transform.position;
        

    }

    public void updateDestenation(Transform t)
    {
        agent.destination = t.position;

    }
}
