using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadScene", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void loadScene()
    {
        Debug.Log("loading the scene");
        SceneManager.LoadScene("gossip2", LoadSceneMode.Single);

    }
}
