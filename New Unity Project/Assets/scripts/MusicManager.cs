using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioClip ElavatorDing;
    AudioSource audioSourse;
    // Start is called before the first frame update
    void Start()
    {
        audioSourse = GetComponent<AudioSource>();
        instance = this;
        if(instance != null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playElavatorDing()
    {
        audioSourse.PlayOneShot(ElavatorDing);


    }
}
