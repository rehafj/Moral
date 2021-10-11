using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchOpener : MonoBehaviour
{
    public Canvas searchCanvas;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            searchCanvas.gameObject.SetActive(true);
            isOpen = true;

        } else
        {
            searchCanvas.gameObject.SetActive(false);
            isOpen = false;

        }
    }
}
