using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge != null)
        {
            image.enabled = true;
            image.sprite = PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge.sprite;
        }
        else
        {
            image.enabled = false;
        }
    }
}
