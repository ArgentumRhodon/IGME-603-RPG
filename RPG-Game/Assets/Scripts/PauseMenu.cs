using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject pausebutton;
    public GameObject controlmenu;
    public bool pause = false;
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
        Application.Quit();
    }
}
