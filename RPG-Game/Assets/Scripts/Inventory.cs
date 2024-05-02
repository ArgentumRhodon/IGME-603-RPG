using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Weapon> Weapons;
    public List<Charge> Charges;
    public List<Potion> Potions;
    public int attackPotion;
    void Start()
    {
        //Weapons = new List<Weapon>();
        Charges = new List<Charge>();
        Potions = new List<Potion>();
        attackPotion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void pickup(Weapon w)
    //{
    //    w.equipped = false;
    //    Weapons.Add(w);
    //    Weapons.Sort((x, y) => x.w_type.CompareTo(y.w_type));
    //}
    
    //void equip(Weapon w)
    //{
    //    Weapons.Find(x => x.w_type == w.w_type).equipped = true;
    //    //switch slot
    //}
    
    
    
    
    
}
