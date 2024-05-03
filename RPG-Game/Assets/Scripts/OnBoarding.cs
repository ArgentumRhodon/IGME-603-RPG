using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnBoarding : MonoBehaviour
{
    public TMP_Text displayText;
    public GameObject OnboardingPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (displayText.gameObject.activeSelf)
            {
                OnboardingPanel.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(true);
        }   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(false);
        }
    }

    public void ClosePanel() 
    {
        OnboardingPanel.SetActive(false);
    }

}
