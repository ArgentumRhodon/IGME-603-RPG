using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnBoarding : MonoBehaviour
{
    public TMP_Text displayText;
    public int panelIndex; 
    private bool overlap;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (overlap)
            {
                ActivePanel.Instance.OpenPanel(panelIndex);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(true);
            overlap = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayText.gameObject.SetActive(false);
            overlap = false;
        }
    }

    public void CloseCurrentPanel()
    {
        ActivePanel.Instance.ClosePanel(panelIndex);
    }
}