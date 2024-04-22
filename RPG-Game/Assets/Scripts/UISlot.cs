using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField]
    public Image image;

    public void UpdateImage(Sprite newsprite) 
    {
        if (!image.gameObject.activeSelf)
        {
            image.gameObject.SetActive(true);
        }
        image.sprite = newsprite;
    }
    public void Reset()
    {
        image.sprite = null;
        image.gameObject.SetActive(false);
    }
}