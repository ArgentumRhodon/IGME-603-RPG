using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePanel : MonoBehaviour
{
    public static ActivePanel Instance { get; private set; }

    public List<GameObject> onBoardingPanels = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOnBoardingPanel(GameObject panel)
    {
        onBoardingPanels.Add(panel);
    }

    public void OpenPanel(int index)
    {
        if (index >= 0 && index < onBoardingPanels.Count)
        {
            foreach (var panel in onBoardingPanels)
            {
                panel.SetActive(false);
            }
            onBoardingPanels[index].SetActive(true);
        }
    }

    public void ClosePanel(int index)
    {
        if (index >= 0 && index < onBoardingPanels.Count)
        {
            onBoardingPanels[index].SetActive(false);
        }
    }
}
