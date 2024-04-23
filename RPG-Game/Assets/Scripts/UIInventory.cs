using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<GameObject> PowerUpSlots;
    public List<GameObject> PotionSlots;

    List<UISlot> powerupslots = new List<UISlot>();
    List<UISlot> potionslots = new List<UISlot>();

    public Inventory inventory;

    void Start()
    {
        initializedPowerSlot();
    }

    void Update()
    {
       UpdatePowerUpSlot();
    }

    public void initializedPowerSlot() 
    {
        foreach (var slotGameObject in PowerUpSlots)
        {
            UISlot slot = slotGameObject.GetComponent<UISlot>();
            if (slot != null)
            {
                powerupslots.Add(slot);
            }
        }
    }
    
    public void UpdatePowerUpSlot() 
    {
        if (inventory != null && inventory.Charges.Count != 0)
        {
            int count = Mathf.Min(inventory.Charges.Count, PowerUpSlots.Count); 
            for (int i = 0; i < count; i++)
            {
                Charge charge = inventory.Charges[i];
                Debug.Log("get sprite");
                if (powerupslots[i] != null)
                {
                    Debug.Log(" have Sprite");
                    powerupslots[i].UpdateImage(charge.sprite);
                    Debug.Log("Change Sprite");
                }
                else
                    Debug.Log(PowerUpSlots[i] != null);
            }
        }
    }
}
