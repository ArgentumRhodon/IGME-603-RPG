using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField]
    public Image image;

    public void UpdateImage(Sprite newsprite) 
    {
        image.sprite = newsprite;
        if (!image.gameObject.activeSelf) 
        {
            image.gameObject.SetActive(true);
        }
    }
}