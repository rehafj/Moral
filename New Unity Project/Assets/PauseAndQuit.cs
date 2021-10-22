using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndQuit : MonoBehaviour
{
    public GameObject PauseMenuPannel;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuPannel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            enablePauseCanvase();
        }
    }

    private void enablePauseCanvase()
    {
        PauseMenuPannel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PauseMenuPannel.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {//add saving stuff
        Application.Quit();
    }
}
