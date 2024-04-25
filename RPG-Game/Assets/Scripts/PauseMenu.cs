using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject pausebutton;
    public GameObject controlmenu;
    public GameObject statesmenu;
    public bool pause = false;
    public bool Showstates = false;
    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);
        pausebutton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowPlayerStates();
        }
    }

    public void TogglePause() 
    {
        if (pause) 
        {
            Time.timeScale = 1;
            pause = false;
            pausemenu.SetActive(false);
            pausebutton.SetActive(true);
        }
        else if (!pause) 
        {
            Time.timeScale = 0;
            pause = true;
            pausemenu.SetActive(true);
            pausebutton.SetActive(false);
        }
    }
    public void ControlMenu()
    {
        pausemenu.SetActive(false);
        controlmenu.SetActive(true);
    }
    public void BackToPause()
    {
        pausemenu.SetActive(true);
        controlmenu.SetActive(false);
    }

    public void Restart() 
    {
        Time.timeScale = 1;
        pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ShowPlayerStates()
    {
        if (Showstates)
        {
            statesmenu.SetActive(false);
            Showstates = false;
            Time.timeScale = 1;
        }
        else 
        {
            statesmenu.SetActive(true);
            Showstates = true;
            Time.timeScale = 0;
        }
    }
}
