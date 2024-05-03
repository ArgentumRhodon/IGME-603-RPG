using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject StartScene;
    public GameObject ControlScene;
    public void OnPlayButton()
    {
        SceneManager.LoadScene("CutScene");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void OnControlButton()
    {
        StartScene.SetActive(false);
        ControlScene.SetActive(true);
    }
    public void OnBackButton()
    {
        StartScene.SetActive(true);
        ControlScene.SetActive(false);
    }
}
