using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        OpenDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        doorAnimator.Play("OpenDoor");
        MusicManager.instance.playElavatorDing();
       // Invoke("donotcodethislate", 5f);

    
    }
}
