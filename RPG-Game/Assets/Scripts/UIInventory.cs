using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> PowerUpSlots;
    public List<UISlot> PotionSlots;

    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePowerUpSlot() 
    {
        if (inventory.Charges.Count != 0) 
        {
            for (int i = 0; i< inventory.Charges.Count; i++) 
            {
                Charge charge = inventory.Charges[i];
                PowerUpSlots[i].UpdateImage(charge.sprite);
            }
        }
    }
}
