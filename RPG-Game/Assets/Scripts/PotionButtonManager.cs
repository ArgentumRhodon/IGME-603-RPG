using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> PotionButtons;
    public Inventory Inventory_Potions;

    void Start()
    {
        PotionButtons[0].onClick.AddListener(delegate { EquipPotion(0); });
        PotionButtons[1].onClick.AddListener(delegate { EquipPotion(1); });
        PotionButtons[2].onClick.AddListener(delegate { EquipPotion(2); });
        PotionButtons[3].onClick.AddListener(delegate { EquipPotion(3); });
        UpdateButton();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void EquipPotion(int i)
    {
        print(PlayerControlManager.Instance.currentPlayer.name);
        print("Equip " + i);
        if (PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_potion != null && !Inventory_Potions.Potions[i].Potion_equipped)
            PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().Unequip(PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_potion);
        PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().Equip(Inventory_Potions.Potions[i]);
    }
    public void UpdateButton()
    {
        print("Update");
        int cnt = Inventory_Potions.Potions.Count;
        print("cnt = " + PotionButtons.Count);
        for (int i = 0; i < cnt; i++)
        {
            if (!Inventory_Potions.Potions[i].Potion_equipped)
            {
                PotionButtons[i].enabled = true;
            }
            else
            {
                PotionButtons[i].enabled = false;
            }
        }
        if (cnt < 3)
        {
            for (int i = cnt; i < 4; i++)
            {
                PotionButtons[i].enabled = false;
            }
        }
    }
}
