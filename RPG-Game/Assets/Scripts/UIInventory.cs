using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> PowerUpSlots;
    public List<UISlot> PotionSlots;

    public Inventory inventory;


    void Update()
    {
       UpdatePowerUpSlot();
    }

    public void UpdatePowerUpSlot() 
    {
        if (inventory != null && inventory.Charges.Count != 0)
        {
            int count = Mathf.Min(inventory.Charges.Count, PowerUpSlots.Count); 
            for (int i = 0; i < count; i++)
            {
                Charge charge = inventory.Charges[i];
                if (PowerUpSlots[i] != null)
                {
                    PowerUpSlots[i].UpdateImage(charge.sprite);
                }
            }
        }
    }
}
