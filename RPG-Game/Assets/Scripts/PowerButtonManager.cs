using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> PowerBottons;
    public Inventory Backpack;
    
    void Start()
    {
        PowerBottons[0].onClick.AddListener(delegate { EquipPower(0); });
        PowerBottons[1].onClick.AddListener(delegate { EquipPower(1); });
        PowerBottons[2].onClick.AddListener(delegate { EquipPower(2); });
        PowerBottons[3].onClick.AddListener(UnEquipPower);
        UpdateBotton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EquipPower(int i)
    {
        print(PlayerControlManager.Instance.currentPlayer.name);
        print("Equip " + i);
        if(PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge != null && !Backpack.Charges[i].equipped)
            PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().Unequip(PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge);
        PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().Equip(Backpack.Charges[i]);
    }
    void UnEquipPower()
    {
        print("Unequip");
        if (PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge != null)
            PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().Unequip(PlayerControlManager.Instance.currentPlayer.GetComponent<Slot>().cur_charge);
    }
    public void UpdateBotton() 
    {
        print("Update");
        int cnt = Backpack.Charges.Count;
        //print("cnt = " + PowerBottons.Count);
        for(int i = 0; i < cnt; i++)
        {
            if (!Backpack.Charges[i].equipped)
            {
                PowerBottons[i].enabled = true;
            }
            else
            {
                PowerBottons[i].enabled = false;
            }    
        }
        if (cnt < 3)
        {
            for (int i = cnt; i < 3; i++)
            {
                PowerBottons[i].enabled = false;
            }
        }
    }
}
